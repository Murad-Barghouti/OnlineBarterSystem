::create a file named generateModels.local.bat and put your DB connection string 
dotnet ef dbcontext scaffold "Server=.;Database=YOURDATABASENAME;Trusted_Connection=True;MultipleActiveResultSets=true" ^
Microsoft.EntityFrameworkCore.SqlServer ^
-o Model ^
-c OnlineBarterSystemContext ^
-d ^
--use-database-names ^
-f
pause