# TeamGrape_BankApp
A simple Bank application made with C# and .Net Core 3.1 done by a group of students as a school project based on some stories from the bank user, the administrator, the bank owner and the system owner which you can find [here](https://qlok.notion.site/Initial-backlog-03eb10ead8f047b3aff215f8c9b47d0e).  
  
This project was part of our course "Agile system development in teams", we choose to work in a Scrum style work method with daily stand-ups and weekly retrospectives and planning meetings to ensure continious delivery of working software in short iterations. Our scrum board can be viewed <a href="https://trello.com/b/Kql7IvAp/team-grape" target="_blank">here</a>.  
## Object Oriented Design
The basic structure of our system can be visualized with this UML  
  
<img width="400" alt="bild1" src="https://raw.githubusercontent.com/qvarnstr0m/TeamGrapeBankApp/3c3ba73d4e04b707976a87cca859ff16320183b5/UML%20TeamGrapeBankApp.png">  
  
To get a structure of different types of users in the system we started with an abstract class User as a base class which sub classes Customer and Admin inherits from, this leaves open possibilities to add new types of users in the future. The BankAccount class is for creating objects of bank account types, it also works a base class in which sub classes LoanAccount and SavingsAccount inherits. The Transaction class is used for handeling transactions between bank accounts so that they are processed at an interval of 15 minutes, also for the ability to list transactions for the logged in customer.

## How to Install and Run the Project
First you should have a Visual Studio installed in you computer and open it. Click on `Clone a repository` which you will find on the right side of the screen and add the link of the github repository in the `Repository location` box. In the `Path` box write a specific empty folder on your computer and then click `Clone`. In the right side of the screen you can se the name of the project. Click on it and you can see all the classes and code we wrote. Now you can run the the project by clicking on the :arrow_forward:`TeamGrapeBankApp`.
## How to Use the Project
After running the project this will be shown up:

<img width="400" alt="bild1" src="https://user-images.githubusercontent.com/114058034/207589420-ba5f5b35-c1f7-488d-b516-0a701e831821.png">

Press any key to continue and than you will see this:

<img width="400" alt="bild2" src="https://user-images.githubusercontent.com/114058034/207590289-2c981161-274f-4e17-82a4-9c3e8e2d4442.png">

In the project you can log in two ways:
One of them is by choosing one of the five bank users you can log in to the bank with and then the menu of choices will be shown:

<img width="290" alt="bild3" src="https://user-images.githubusercontent.com/114058034/207592195-67e0ebad-3c55-49b9-83df-ddea702d0e0a.png">

You can choose one by enter the corresponding number and follow the steps that you will find in each of the choices.

The other way to log in is to log in as the admin and the other menu will be shown: 

<img width="277" alt="bild4" src="https://user-images.githubusercontent.com/114058034/207592323-4eb7e2a2-970d-4e30-8fa6-fe9c0c1a35a6.png">

You can choose one by entering on the corresponding number and follow the steps that you will find in each of the choices.

By clicking on option 8 (as user) or 3 (as admin) you will be logged out.

<img width="208" alt="bild5" src="https://user-images.githubusercontent.com/114058034/207592724-cf6f016d-cdde-44ba-81d2-982c86d76d50.png">

## Particpants
* [Alfred Larsson](https://github.com/Fredihi)
* [Kenny Lindblom](https://github.com/KennyLindblom)
* [Martin Qvarnström](https://github.com/qvarnstr0m)
* [Nour Uqla](https://github.com/NourUq02)
* [Zanefina Qmega](https://github.com/Zanefina)
