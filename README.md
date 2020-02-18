# ContactManagement
This project is about contact management. It does basic operations such as insert, delete, update and displays list of contacts.

Folder structure of the application :
------------------------------------
1) ProCompany.Contact.UnitTests: This project contains tests cases of Contact manangement web application.

2) Company.Contact.WebAPI: This project contains all apis, that we call to accomplish insert, update, delete and select operations. 

3) Company.Contact.WebApp: This project internally consumes apis implemented in WebAPI project. Contains different tools such as automapper, dependancy injection.

4) Company.Contacts.DomainEntities: This project has business entities required for this web application. This project referenced in other projects to maintain consistancy over all projects.

5) Company.Contacts.RepositoryServices : This project has services that we are using for all CRUD operations. Basically, I have used code first approach while creating these services.

Instructions on how to run the application :
-------------------------------------------
1) Open this project in Visual Studio 2017/2019. Please make sure you have dot net core sdk 2.2.0 installed on your machine. you can use below link to download 
https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-2.2.101-windows-x64-installer

2) You need to make sure you multiple project startup is enabled on your machine. You can do this by changing startup project options and set it to multiple start up projects. select dropdown of Company.Contact.WebAPI and Company.Contact.WebApp project and set it to Start.

3) Database creation: you need to create database using SQL server. Name it "ContactServiceDB". I have added script to this to create table on database. 

4) Script is available at AppData folder of Company.Contact.WebAPI project. Script name is - InitalScriptToRunThisProject.txt

5) That's it, project is ready to run on your machine.
