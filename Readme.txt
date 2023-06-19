** ATTENTION **
* This readme file belongs to the entire project, including the data access layer (DAL), webservices (WS) and client app (CA)

** For Database and web services
* Install the developer version of the SQL server from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
* Install microsoft sql server studio (MSSQL) from: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16
* Install microsoft visual studio
* Upon installing, open MSSQL, connect to the server and create a database named "OnlineBarterSystem"
* Open the project in microsoft visual studio VS. Right click on the OnlineBarterSystemWS and choose the "Manage user secrets" option
* Add the following to the user secret:
    {
        "ConnectionStrings": {
            "OnlineBarterSystemConnection": "Server=.;Database=OnlineBarterSystem;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
        }
    }
* In the microsoft VS click on tools in the upper middle part of the screen. Select NuGet package manager and then packet manager console.
* When the console opens, make sure the default project selected is OnlineBarterSystemDal
* In the console, type the command "Add-Migration" and then give it a name of your choice. Run the migration.
* A folder called migrations should appear in the DAL project.
* In the same console run the command "update-database" and the name of the migration you chose before. Check the database in MSSQL, new tables
    should be created.
* Open the seed file in the seed folder in the DAL project. Copy the content of the file.
* Go to the MSSQL and right click on your database, choose the new query option.
* Paste the content into the query window and execute it.
* Now the database and the webservices are ready.

** For the client Application
* Install node JS
* open the command prompt while in the client app folder
* run npm install

*****
** To Run the entire project
* Run the webservices from microsoft VS and open a command prompt from inside the client app folder and run npm start