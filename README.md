# Systemtestning Labb1

### Utvalda delar att testa:
Jag har valt ut 3st delar ur koden som jag finner är dels viktiga för programmets säkerhet och funktion men också rimliga att testa utifrån den applikation som jag jobbar med. 
 Det är väldigt tydligt att vi ej jobbade testdrivet när vi gjorde bank-projektet, detta ser man då det är långa metoder med lite för mycket innehåll och mycket utskrifter till konsollen blandat med logik. Det är inte heller många metoder som returnerar något utan många metoder som sköter hela "kedjan" i applikationen från användarens interagering till data som lagras. 
 Jag har har därför valt ut följande delar:
 <ul>
  <li>VerifyCostumer<p>Denna metod verifierar att ett lösenord som skrivs in stämmer överrens med en användares och returnerar en bool. Den här delen av programmet är viktig då det inte får komma in fel användare i systemet och att lösenorden måste funka.</p></li>
  <li>CheckPassword<p>I den här metoden skall först och främst en användare hittas i en lista av användare. Om den inte hittas skall metoden returnera null. Metoden skall sedan kontrollera det inmatade lösenordet mot användarens lösenord, är det en match så skickas ett användar-objekt tillbaka som sedan används för att knyta an till konton etc i systemet. Är det inte en match så skickas null tillbaka. Denna metoden är likt den förra viktig för programmets säkerhet och det ställs krav på att det både skall finnas en användare i systemet och att lösenordet skall matcha vilket skall säkerställa att endast användare med tillgång till systemet kan komma in.</p></li>
 <li>EnoughBalance<p>Den här metoden tillhör basklassen för konton och säkerställer att det finns tillräckligt med pengar på kontot. Detta genom att ta in en kontrollsumma som skall vara lägre än saldot på kontot, då returneras en bool som är true. Är saldot lägre så returneras en bool som är false. Denna metod är viktig för att användaren inte skall kunna föra över eller ta ut pengar som den inte har.</p></li>
 </ul>
 
## Vad Kan gå fel?
 <ul>
  <li>VerifyCostumer
   <p>
   <ul>
    <li>Att fel lösenord har lagrats i användarobjektet</li>
    <li>Att metoden skickar tillbaka en bool som är true även fast lösenordet är fel, eller tvärt om.</li>
    <li>Att programmet inte ser skillnad på stora och små bokstäver</li>
   </ul>
   </p>
   </li>

  <li>CheckPassword 
	  <p>
    <ul>
     <li>Att metoden skickar tillbaka null trots att det finns en användare och lösenord som matchar i listan. </li>
     <li>Att metoden kan returnera en true bool trots att användaren finns men lösenordet är fel.</li>
     <li>Att metoden inte returnerar någonting och genererar fel som krachar programmet.</li>
      <p> *(Hittade ett fel i koden när den skulle testas. Då användaren inte hittades i listan och metoden gick in i en if-sats där lösenordet hos användaren skulle
        jämföras med det medskickade lösenordet, då krashade programmet då användar-objektet var null. Jag lade till ett villkor i if-satsen för att först kolla
        att objektet inte är null. Felet uppstod aldrig i applikationen då det i programmet redan var kollat att en användare finns.)* </p>
     <li>Att programmet inte ser skillnad på stora och små bokstäver</li>
     </ul>
    </p>
   </li>


  <li>EnoughBalance
   <p>
    <ul>
      <li>Att metoden skickar tillbaka en false bool trots att det finns tillräckligt med pengar på kontot</li>
      <li>Eller tvärt om ovanstående.</li>
      <li>Att metoden inte fungerar med decimaler</li>
      <li>Att metoden inte fungerar med minus siffror</li>
      <li>Att metoden returnerar false om sifforna är likadana</li>
     </ul>
    </p>
   </li>

</ul>


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
### Admin
This is a subclass from User that handles the functions that only the admin does.
### Customer
This is a subclass from User. It sorts out data from the StoreAndLoad class for customers that are already registered in the system. Each customer object contains all the customers accounts. 
### Account
This class contains account functions such as transations and bank loan. This class contains all the available currencies in the system. 
### SavingsAccount, CreditAccount, MainAccount, FutureAccount, InvestmentAccount
These are subclasses from Account and contains funtions related to these subclasses. 
### Bank
This is a static class that contains the login functions and the user objects. After login it checks the user type and directs the user to the correct menu. Contains the different menues and handles all the menu options in the system. Handles the API calls to update the registered currency rates. 
HamsterArt
Handles the visual effects and graphical content in the application.

##  UML-CHART
![Team Hamster](https://user-images.githubusercontent.com/77905783/146100171-83416663-8888-4031-a30c-28158657033b.jpeg)
##  SCRUM BOARD
[Scrum Board](https://acidic-mat-b11.notion.site/Team-Hamster-2ebd3b4450514aad9722e9e1fedb90a5)
