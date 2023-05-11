using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;

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
            string stringToReturn = "AanyaKiara";

            try
            {

                DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential();

                Uri keyVaultUri = new Uri("https://testkeyvalultak1.vault.azure.net/");
                SecretClient secretClient = new SecretClient(keyVaultUri, defaultAzureCredential);

                Response<KeyVaultSecret> response = secretClient.GetSecret("SqlServerConnectionString");

                stringToReturn = response.Value.Value;
            }
            catch (Exception ex)
            {
                stringToReturn = ex.ToString();
            }

            return stringToReturn;
        }
    }
}
