ShopBridge is an e-commerce shopping application. Currently, a product admin of ShopBridge can add, edit, delete and get items from the inventory.

Following are the steps to run the application:

A. Configure and seed database
1. Run command 'dotnet tool install --global dotnet-ef' so that Entity Framework is installed globally and we can create the database.
2. In the appsettings.json file, under 'ConnectionStrings' update the connection string for the 'ShopBridgeDB' SQL database.
3. The "Migrations" folder contains the database tables. To apply/create them, run command 'dotnet ef update database'. After this command has ran, you should be able to see tables in the database provided in the ConnectionStrings.

B. Run the application
1. Check the url under 'Urls' in appsettings.json and make sure the port to be used is free. At this url, the appplication will be hosted.
2.i	Run command 'dotnet build' to build the application. This command will build the binaries at path '~\bin\Debug\net5.0\'.
2.ii Navigate to the folder created in the earlier step and run 'dotnet ShopBridge.dll'. This will run/host the application at the url mentioned in step B.1

Note: Step B.1 can be avoided and the Url can then be provided in step 2.ii with additional parameter '--urls=http://localhost:<port>/' with <port> replaced with the port number that is to be used/free.
