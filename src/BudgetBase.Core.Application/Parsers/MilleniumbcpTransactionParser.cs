using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces;
using BudgetBase.Core.Domain.Entities;
using System.Globalization;
using System.Text;

namespace BudgetBase.Core.Application.Parsers
{
    public class MilleniumbcpTransactionParser : IBankTransactionParser
    {
        public ParserResult ParseTransactions(MemoryStream stream)
        {
            int transactionsCount = 0;
            var transactions = new List<TransactionDto>();
            string csvContent = Encoding.Unicode.GetString(stream.ToArray());

            using (var reader = new StringReader(csvContent))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    // Skip the first 12 lines and the footer
                    if (lineNumber < 12 || line.StartsWith("O millenniumbcp.pt"))
                    {
                        lineNumber++;
                        continue;
                    }

                    if (lineNumber > 12) // Start parsing from line 13
                    {
                        var fields = line.Split(';');
                        if (fields.Length >= 3) // Ensure there are at least 3 columns
                        {
                            try
                            {
                                transactionsCount++;

                                DateTime date = DateTime.ParseExact(fields[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                                var transaction = new TransactionDto
                                {
                                    Date = date,
                                    Description = fields[2],
                                    Amount = Convert.ToDecimal(fields[3], CultureInfo.InvariantCulture)
                                };
                                transactions.Add(transaction);
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
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
