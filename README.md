# TeamHamsterBank
***Version 1.0.0***

## INDTRODUCTION
This is a group project for our school work. We have created a bank program that provides basic banking services with forex trading. 

## Table of contents
* [TECHNOLOGY AND LANGUAGE](#TECHNOLOGY-AND-LANGUAGE)
* [CONTRIBUTERS](#CONTRIBUTERS)
* [INSTALLATION](#INSTALLATION)
* [USING THE PROGRAM](#USING-THE-PROGRAM)
* [ABOUT THE PROGRAM - Classes and objects](#ABOUT-THE-PROGRAM---Classes-and-objects)
* [UML-CHART](#UML-CHART)
* [SCRUM BOARD](#SCRUM-BOARD)

## TECHNOLOGY AND LANGUAGE
- Type of application: Console application
- Programming language: C#
- Framework: .NET 3.1
- Language used in program: Swedish

## CONTRIBUTORS (Students from Campus Varberg, class SUT21)
- Nael Sharabi, https://github.com/NAYEL4SLR
- Robin Svensson, https://github.com/hoprob
- Elin Ericstam, https://github.com/elineri

## INSTALLATION
The project was build in .NET Core 3.1 so that it can be run on both Windows and Mac devices. Simply run the application from the executable file.  

## USING THE PROGRAM
The user logs in and the program determines if the user is an admin or customer. Depending on which type of user it is they will get different options in the menu.
If a user fails to log in three times the program will be blocked for 1 min. 

 Admin
 - Username: 111111
 - Password: password
 
 Customer
 - Username: 333333
 - Password: password
 
 Customer
 - Username: 555555
 - password: password

For other registered users and login details check the textfiles (Users.txt).

Admin menu
1. Register a new customer
2. Update exchange rates (API calls)
3. Set value for specific exchange rates
4. Change password for customer
5. Change password
6. Remove a user (admin/customer)
7. Log out

Customer menu
1. View accounts and balance (option to view history/transaction logs)
2. Transfer between accounts
3. Deposit
4. Withdraw
5. Open a new account
6. Change password
7. Bank loan
8. Log out

## ABOUT THE PROGRAM - Classes and objects
### StoreAndLoad
This class reads data from the text-files (Users, Users - Backup, Accounts, Accounts - Backup, Transactions, Transactions - Backup). If these files are lost, the program will print 
“System maintenance is ongoing” and then it will shut down. 

The imported data will be used to declare and load all interactive objects, such as: admins, customers, accounts and their history transactions. With that’s all being done, and on each 
return from a menu choice, the class will overwrite the text-files with any possible changes. However, this will only affect the files which don’t have ‘Backup’ in their names.

### User
This is an abstract base class for admin and customer classes. Contains logic which is implemented in both admin and customer class. 
Admin
This is a subclass from User that handles the functions that only the admin does.
### Customer
This is a subclass from User. It sorts out data from the StoreAndLoad class for customers that are already registered in the system. Each customer object contains all the customers accounts. 
### Account
This class contains account functions such as transations and bank loan. This class contains all the available currencies in the system. 
### SavingsAccount, CreditAccount, MainAccount, FutureAccount, InvestmentAccount
These are subclasses from Account and contains funtions related to this class. 
### Bank
This is a static class that contains the login functions and the user objects. After login it checks the user type and directs the user to the correct menu. Contains the different menues and handles all the menu options in the system. Handles the API calls to update the registered currency rates. 
HamsterArt
Handles the visual effects and graphical content in the application.

##  UML-CHART

##  SCRUM BOARD
