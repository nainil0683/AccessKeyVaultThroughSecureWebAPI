# AccessKeyVaultThroughSecureWebAPI
This is a Https Web API that would access KeyVault secret

Note: The entire ARM template of the Resource group is checked in with file "ExportedTemplate-TestResourceGroup.zip"

End goal:
To query Azure SQL Datasbase by first fetching the connection string from a KeyVault secret and then using the connection string to fetch data from Azure SQL Database and display it on screen using an App Service instance that has ASP.NET Web API deployed through Git.
To fetch KeyVault secret, we first creat a "Managed Identity" calld "userassignedmanagedidentity" (better read as UserAssignedManagedIdentity).
This managed identity will be assumed by App Service ASP.NET Web API to fetch the Azure KeyVault secret

Steps:

1. In Azure, search for "Managed Identities"
    a. Create a new managed identity called "userassignedmanagedidentity"
    
2. Create a new Azure KeyVault account called "TestKeyValultAK1"
    a. Add a new secret called SqlServerConnectionString
    b. Open the secret and assign the value for connection string
    c. Go back to the overview page
    
    Assign Access control (IAM)
    a. Click on "Access control (IAM) from left blade
    b. Click on "Add" and select "Add role assignment"
    c. Search for "Key Vault Secrets User" role and seelct it and click on "Next"
    d. Select on "Managed identity" radio button in "Assign access to" section under "Members" tab
    e. Click on "Select members"
    f. In the "Select managed identities" section on the right side, for the drop down "Managed identity", select "User-assigned managed identity (1)"
    g. Select "userassignedmanagedidentity" and hit "Select"
    h. Click on "Review + Assign"
    
    Assign Access Policies
    a. On the left blade, click on "Access Policies" 
    b. Click on "Create"
    c. In the "Secret Permissions", select "Get" and "List" and click on "Next"
    d. In "Principal" tab, type "userassignedmanagedidentity"
    e. Eventually, create it
    
3. Create a new instance of "App Service"
    a. Name the app service "AccessKeyVaultThroughWebAPIWithUserManagedIdentity"
    b. Set up the Git username, repository and other Git realted information
    c. Go to Identity from the Settings section from left blade
    d. Click on "User assigned" and click on "Add"
    e. From the section on the left start typing "userassignedmanagedidentity"
    f. Select the "userassignedmanagedidentity" and add
    g. Make sure to save it
    
4. Create a new instance of "Azure SQL"
    a. Create a server and add a database
    b. Use Sql server authentication
    c. Click on "Networking" item in the left blade under "Security" section
    d. To allow connecting local computer, under the "Firewall rules", click on "Add your client IPv4 address (xxx.xxx.xxx.xxx)"
    e. In the "Exceptions" section, click on check box for "Allow Azure servcies and resources to access the server"
    f. Click on Save
