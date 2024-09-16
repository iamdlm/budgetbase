using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces;
using ClosedXML.Excel;
using System.Globalization;

namespace BudgetBase.Core.Application.Parsers
{
    public class ActivoBankTransactionParser : IBankTransactionParser
    {
        public ParserResult ParseTransactions(MemoryStream stream)
        {
            int transactionsCount = 0;
            var transactions = new List<TransactionDto>();

            // Load the stream into an Excel workbook
            using (var workbook = new XLWorkbook(stream))
            {
                // Assuming the transactions are in the first worksheet
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed(); // Skip header

                int lineNumber = 0;

                foreach (var row in rows)
                {
                    // Skip the first 8 lines and the footer
                    if (lineNumber < 5)
                    {
                        lineNumber++;
                        continue;
                    }

                    if (lineNumber > 5) // Start parsing from line 9
                    {
                        try
                        {
                            DateTime date = DateTime.ParseExact(row.Cell(2).Value.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                            var transaction = new TransactionDto
                            {
                                Date = date, // Assuming 'date' has been correctly parsed as shown in previous advice
                                Description = row.Cell(3).GetString(), // Use GetString() for textual data
                                Amount = 0 // Initialize with a default value
                            };

                            // Attempt to convert the cell value directly if it's numeric
                            if (row.Cell(4).TryGetValue(out double numericValue))
                            {
                                transaction.Amount = Convert.ToDecimal(numericValue, CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                // Fallback: Attempt to parse the cell's string value as a decimal
                                string amountString = row.Cell(4).GetString(); // Convert to string for parsing
                                if (decimal.TryParse(amountString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedAmount))
                                {
                                    transaction.Amount = parsedAmount;
                                }
                                else
                                {
                                    // Handle error: The cell's value is neither directly numeric nor parsable as decimal
                                    // This could involve logging an error, throwing an exception, or setting a default value
                                    throw new InvalidOperationException($"Unable to parse amount from cell: {amountString}");
                                }
                            }
                            transactions.Add(transaction);
                            transactionsCount++;
                        }
                        catch (Exception ex)
                        {
                            // Log or handle the exception as needed
                            continue;
                        }
                    }
                    lineNumber++;
                }
            }

            return new ParserResult()
            {
                Transactions = transactions,
                ProcessedCount = transactionsCount
            };
        }
    }
}
