# BankSolution

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Description](#description)
- [Project Structure](#project-structure)
- [Notes](#notes)
- [Website](#url)

## Introduction
This project is a Bank application using a database with over 5000 simulated customers from Sweden, Finland, Norway and Denmark (database-first, the db will not be created locally when running the app). A bootstrap template was used from StartBootstrap, custom styling and scripts were added. The project was made with Razor Pages using ASP.NET Core Identity which allows for a dynamic user experiance using authentication and authorization.

## Features
- statistics based on each country
- User login
- password hashing
- Account management
- Fund transfers between accounts
- deposits & withdrawals
- Transaction history
- seeded user roles (admin, cashier)
- Admin panel for managing users and accounts (currently only styling implemented)
- Secure authentication and authorization with ASP.NET Core Identity
- Processes transactions nightly to look for suspicious activity

## Technologies Used
- **Framework**: ASP.NET Core 6
- **UI**: Razor Pages
- **Server-side Language**: C#
- **Database**: Microsoft SQL Server Management Studio (MSSMS)
- **Authentication**: ASP.NET Core Identity
- **Client-side**: JavaScript, HTML, CSS
- **Styling**: Bootstrap
- **Tools**: Entity Framework Core, Visual Studio
- **Deployment**: Azure
- **Cloud Services**: Azure SQL Database, Azure App Service


## Descritpion
On the Home page (Index) the unauthenticated user is shown statistics of every country's statistics composed of the name, flag, the amount of cutomers, accounts and total balance. The nav bar is collapsable and inludes a "Login" link, where the user enters an email and password to get authenticated. The roles seeded are admins and cashiers that both can manage customers but admins can also manage users.

The authorized user can search for customers on the "Customers" page using the search-bar by entering a "CustomerNumber", Name or City. The customers are paged by displaying 50 customers at a time. At the bottom of the page the pages are displayed (1, 2, 3, 4, etc) in addition to a "Next" button and a "Prevous" button if the user isn't on page 1. By pressing the "view" button on the right on the customer row, the user is redirected to the CustomerDetails page. On the "CustomerDetails" page the chosen customers details are displayed, full name, address, bithday and national id to name a few. 

In the 'Account' section all the customers accounts and the total balance of them is displayed. Here it is also possible for the user to get redirected to the "AccountDetails" page where information such as the transaction history is displayed and paged by 20. The paging on this page is done using an AJAX script which allows for 20 more transactions to be listed without needing to load the whole page. There are three buttons for creating new transactions; deposit, withdraw and transfer. I have used individual modules for displaying each action and JavaScript for the handling of the modules and form submissions.

## Notes
The app is deployed using Azure App Service, with Azure SQL Database for the database and an Azure SQL Database Server. The BatchProcessor is scheduled to run at 2:00 AM every night, using Azure WebJobs. All customers are simulated and not real.

Thank you for reading and feel free to take a look at the BankApp!

## Url
https://bankapp-ih.azurewebsites.net/

**Login**

Email: richard.chalk@systementor.se
Password: Hejsan123# 
role: Admin

Email: richard.erdos.chalk@gmail.se
Password: Hejsan123#
role: Cashier
