Budgetbase is an open-source, safe and simple tool that allows tracking spending, budgeting and savings.  

## Feature Highlights
- **Tracking**: Checkbook register for all your personal accounts, including savings and checking accounts.
- **Imports**: Import your bank statements with no effort from over 50 supported banks.
- **Categories**: Categorize transaction types manually or automatically with rules.
- **Budgets**: Set budgets per category and monitor your spending so you don't get of track.
- **Calculators**: Estimate how much should you spend, on what, when could you retire and more.
- **Dashboards**: Monitor your activity with a variety of rich interface dashboards.

## Developer Quick Start
A priority of Budgetbase is to keep the technology footprint small for getting up and running as quickly as possible. However, you do need the minimum requirements listed below:
- .NET 7
- npm for compiling javascript
- PostgreSQL
- SMTP
- Visual Studio 2022 or VS Code. All tutorials will assume Visual Studio.

The steps to run Budgetbase locally are that of any typical .NET application.
1. Clone this repository into your local directory.
git clone https://github.com/budgetbase/budgetbase.git
2. Ensure your appsettings.config has a valid database connection string and SMTP credentials. If you do not have access to an SMTP server for local development, [check out Papercut-SMTP](https://github.com/ChangemakerStudios/Papercut-SMTP). Super convenient.
3. Make sure Budgetbase.Web.Razor is set as the Default Project. Compile and run. Budgetbase will apply the database migrations to your database on first run.

#### Optional
To create migrations run one of the following commands, depending on the DbContext affected:
```
dotnet ef migrations add MIGRATION_NAME --context AppDbContext --project BudgetBase.Infrastructure.Persistence --startup-project BudgetBase.Web.Razor --output-dir Migrations
```
```
dotnet ef migrations add MIGRATION_NAME --context AppIdentityDbContext --project BudgetBase.Infrastructure.Identity --startup-project BudgetBase.Web.Razor --output-dir Migrations
```

## Community support
For general help using Budgetbase, please refer to official documentation on budgetbase.app, the Budgetbase Youtube channel, or post your questions and feedback in Github Discussions.

## Contributing to Budgetbase
Budgetbase is open-source software, freely distributable under the terms of an MIT license.

We welcome contributions in the form of feature requests, bug reports, pull requests, or thoughtful discussions in the GitHub discussions and issue tracker. Please see the CONTRIBUTING document for more information.
