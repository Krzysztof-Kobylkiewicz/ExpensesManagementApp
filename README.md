# Financial Management App - Under Development
Application created using .NET, C#, Blazor and MS SQL and others - works in InteractiveAuto Render Mode

# Introduction
The aim of this project is to make financial management easier. The application allows you to import csv files from banks, perform CRUD operations on files, transactions and categories. 
Currently, the user interface for financial management is being developed - charts, forecasts, statistics. In the next phase, tests are planned to be added.

# Technologies
- .NET 8.0
- C# 12
- EntityFramework 8.0.10
- Microsoft SQL Server 2022 - 16.0.1000.6

## Features
- It is possible to import csv files generated from bank (currently only Santander);
- Supports CRUD operations;
- Allows to 
- Easy management assured by charts, predictions and statistics - under development;
- Ensuring stability through testing - planned for the next phase of development;
- It is possible to group transactions;

# Project status
- Both server side and client side services are implemented and ready to be extended if needen;
- File, transaction & transaction group dialogs are implemented;
- Reusable file, tansactions, transaction group & categories components are also implemented;
- It is possible to import .csv files with transactions - currently only Santander mapping is supported;
- Management - charts, predictions & statistics - under develompent;
- Models - both DTO and DB models;
- Categories, transactions, file & statistics repositories are implemented and ready to be extended if needed;
- HttpResult model & custom exceptions were created in order to increase control over returned data & messages;
