# DataBase Setup.
Copy Database "MDF" and "LDF" files and attached them to local SQL server.
---OR---
Copy DBscript.sql file from src/Database Folder and run it SQL Server Management Studio

There are 2 table "Users" and "UserPermission" created.
if you attached direct MDF file to SQL server then data will be there and then use

User Id: admin@usersdemo.com ---or--- henry.peterson@userdemo.com
Password: Test@123

You can check it from data tabel stored in plain text as it is just for demo did not use any data encryption.

Users (Table)
=================
UserId  FirstName   LastName    Phone       Email                           Password    Status
1	    Henry	    Peterson	223388771	henry.peterson@userdemo.com	    Test@123	True
2	    Mark	    Joe	        772341166	admin@usersdemo.com	            Test@123	True

UserPermissions (Table)
=======================
Id  ModuleName  ViewPermission      AddPermission   EditPermission  DeletePermission    UserId
4	Users	    True	            True	        True	        True	            2
5	Customer	True	            True	        True	        True	            2
6	Operations	True	            True	        True	        True	            2
7	Users	    True	            False	        True	        False	            1
8	Customer	True	            True	        True	        False	            1
9	Operations	True	            True	        True	        False	            1

