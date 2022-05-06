# BankApp
Current project in school. 
- ASP.NET Core Web App
- Code first
- Razor Pages
- Unit test
- ASP.NET Core Identity, roles through Seed().

This is a webb app which handles bank customers and their accounts. Thera are two roles; "admin" and "cashier". 

As an admin you can:
- Withdraw/deposit money from a customer
- Transfer money between accounts of a customer
- Add a new customer
- Edit a customer
- Add a new user (cashier/admin)

As a cashier you can:
- Withdraw/deposit money from a customer
- Transfer money between accounts of a customer
- Add a new customer
- Edit a customer

Unit tests:
- When making deposit,withdrawal or transfer with negative amount - return AmountIsNegative()
- When withdrawal or transfering a bigger amount than there is - - return BalanceTooLow()
- When making any transaction - a new transacton is added to the List<Transactions>


