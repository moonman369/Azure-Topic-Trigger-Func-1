
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Moonman.KeyVault
{
    public class KeyVaultSecretAggregtor
    {

        private static readonly string? _azureKeyVaultConnStr = System.Environment.GetEnvironmentVariable("AKV_URL");

        public static string GetSecretValue(string key)
        {
            try
            {
                if (_azureKeyVaultConnStr == null)
                {
                    return "";
                }
                var client = new SecretClient(new Uri(_azureKeyVaultConnStr), new DefaultAzureCredential());
                KeyVaultSecret secret = client.GetSecret(key);

                return secret.Value;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}