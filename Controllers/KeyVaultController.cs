using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AccessKeyVaultThroughHttpsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public KeyVaultController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetKeyVaultSecret")]
        public string Get()
        {
            string stringToReturn = "AanyaKiara ";

            try
            {

                DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential();

                Uri keyVaultUri = new Uri("https://testkeyvalultak1.vault.azure.net/");
                SecretClient secretClient = new SecretClient(keyVaultUri, defaultAzureCredential);

                Response<KeyVaultSecret> response = secretClient.GetSecret("SqlServerConnectionString");

                // stringToReturn = response.Value.Value;

                // string connectionString = @"Server=tcp:testdbserverak1.database.windows.net,1433;Initial Catalog=TestDatabase;Persist Security Info=False;User ID=SqlAdmin;Password=Aanya3kiara14!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                string connectionString = response.Value.Value;

                DataSet dataSet = new DataSet();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    using(SqlDataAdapter dataAdapter = new SqlDataAdapter())
                    {
                        dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM Persons", sqlConnection);

                        sqlConnection.Open();

                        dataAdapter.Fill(dataSet);
                    }
                }

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(stringToReturn);
                if(dataSet.Tables.Count > 0)
                {
                    foreach(DataRow row in dataSet.Tables[0].Rows) 
                    {
                        stringBuilder.Append($" ID: {row["ID"]}, First Name: {row["FirstName"]}, Last Name: {row["LastName"]}, Age: {row["Age"]}.  ");
                    }
                }

                stringToReturn = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                stringToReturn = ex.ToString();
            }

            return stringToReturn;
        }
    }
}
