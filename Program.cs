using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;


namespace az204_auth
{
    class Program
    {
        private const string _clientId = "e1fae282-ed9d-4af3-b588-3e3dab6f2d82";
        private const string _tenantId = "0203b644-9c4a-42de-af6e-5b93af31924f";

        public static async Task Main(string[] args)
        {
            var app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build(); 
            string[] scopes = { "user.read" };
            // AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

            // Console.WriteLine($"Token:\t{result.AccessToken}");

            var provider = new InteractiveAuthenticationProvider(app, scopes);

            var client = new GraphServiceClient(provider);

            User me = await client
                .Me
                .Request()
                .GetAsync();
            
            Console.WriteLine($"Display Name:\t{me.DisplayName}");


        }
    }
}