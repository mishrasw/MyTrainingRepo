using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace UnleasedBDD.StepDefinitions
{
    [Binding]
    class APISteps
    {

        private IWebDriver driver;
        private readonly ScenarioContext context;

        public APISteps(ScenarioContext context)
        {
            this.context = context;
            driver = context.Get<IWebDriver>("driver");
        }

        [Given(@"I send a GET request")]
        public async void GivenISendAGETRequestAsync()
        {
            
            var client = new WebClient();
            string signature = GetSignature("format=json", "nbSJg6rHEOZFHhErmPQRTLOJ3oDrQkYZ8tZUlsrvLpfpU2qcg0zxU9yAjGD7auz4gViiB2QNYCdkw==");
            client.Headers.Add("api-auth-id", "21b8f644-b5a3-4398-b97b-636048d7811c");
            client.Headers.Add("api-auth-signature", signature);
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("Content-Type", "application/json; charset=" + client.Encoding.WebName);

            string response = Get(client, "https://apidocs.unleashedsoftware.com/Products?ProductCode=123456?format=json");


        }


        private string GetSignature(string args, string privatekey)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] key = encoding.GetBytes(privatekey);
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(args));
            string hmac64 = Convert.ToBase64String(hashValue);
            myhmacsha256.Clear();
            return hmac64;
        }

        private string Get(WebClient client, string uri)
        {
            string response = string.Empty;
            try
            {
                response = client.DownloadString(uri);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var stream = ex.Response.GetResponseStream();
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
            }
            return response;
        }

    }
}
