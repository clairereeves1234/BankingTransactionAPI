Instructions for setting up environment:
Install VSCode
Install .NET 8 Sdk (https://dotnet.microsoft.com/en-us/download)
Install .NET Install Tool Extension, C# Extension, and C# dev kit (all from microsoft)
For more information on setting up C# and .NET in VSCode use this link: https://code.visualstudio.com/docs/csharp/get-started

Instructions for running: 
In the terminal navigate to the BankingTransactionAPI directory
Enter "dotnet run" to run the application
I have included a postman collection with example requests called "Banking Transaction Collection.postman_collection.json"

Assumptions: 
- Many users can have the same first name and last name as it is assumed that there may be two different users with the same name. 
Their userId created upon user creation is used to uniquely identify them. 