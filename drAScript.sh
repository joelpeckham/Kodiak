dotnet add package Microsoft.EntityFrameworkCore.Tools 
dotnet add package Microsoft.EntityFrameworkcore.SqlServer 
dotnet add package Microsoft.EntityFrameworkcore.SqlServer.Design 
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design 

mkdir Models

dotnet ef dbcontext scaffold "Server=localhost; database=kodiakDB; user id=sa;Password=B3lated#iSh" Microsoft.EntityFrameworkcore.SqlServer -c kodiakDBContext -o Models -f

dotnet aspnet-codegenerator razorpage -m kodiak.Models.Course -dc kodiak.Models.kodiakDBContext -udl -outDir Pages/kodiakDB --referenceScriptLibraries