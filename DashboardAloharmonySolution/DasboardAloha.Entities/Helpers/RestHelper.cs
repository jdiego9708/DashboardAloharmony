using DasboardAloha.Entities.Configuration.ModelsConfiguration;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Amazon.Runtime;
using DasboardAloha.Entities.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DasboardAloha.Entities.Helpers
{
    public class RestHelper : IRestHelper
    {
        private readonly ConfigurationDataBox ConfigurationDataBox;
        public RestHelper(IConfiguration IConfiguration)
        {
            var settings = IConfiguration.GetSection("ConfigurationDataBox");
            this.ConfigurationDataBox = settings.Get<ConfigurationDataBox>();
        }
        public async Task<RestResponseModel> CallMethodPostDataBoxAsync(string data)
        {
            try
            {
                string apiUrl;

                bool isProduccion = false;

                if (isProduccion)
                    apiUrl = ConfigurationDataBox.ApiURLProd;
                else
                    apiUrl = ConfigurationDataBox.ApiURLProd;

                var requestUri = $"{apiUrl}/data";
             
                JObject jsonObject = JObject.Parse(data);
                JArray dataArray = (JArray)jsonObject["data"];

                var parameters = new Dictionary<string, object>();

                List<string> querys = new();

                foreach (JObject dataObject in dataArray)
                {
                    foreach (JProperty property in dataObject.Properties())
                    {
                        string propertyName = property.Name;
                        JToken propertyValue = property.Value;

                        string query = "{\"data\":[{\"$V0\":V1}]}";
                        string queryFull = query.Replace("V0", propertyName);
                        queryFull = queryFull.Replace("V1", propertyValue.ToString());
                        querys.Add(queryFull);
                    }
                }


                //RestResponse result = new();
                //foreach (string query in querys)
                //{
                //    await Task.Delay(1000);

                //    RestClient client = new(apiUrl);

                //    RestRequest request = new()
                //    {
                //        Method = Method.Post
                //    };

                //    request.AddHeader("Content-Type", "application/json");
                //    request.AddHeader("Accept", "application/vnd.databox.v2+json");
                //    request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes(ConfigurationDataBox.TokenAccess))}");

                //    request.AddJsonBody(query);
                //    result = await client.ExecuteAsync(request);
                //    if (result.IsSuccessful)
                //    {

                //    }
                //}

                RestClient client = new(apiUrl);

                RestRequest request = new()
                {
                    Method = Method.Post
                };

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/vnd.databox.v2+json");
                request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes(ConfigurationDataBox.TokenAccess))}");

                request.AddJsonBody("{\"data\":[{\"$Count_users_registers\":2400}]}");
                RestResponse result = await client.ExecuteAsync(request);

                //var data1 = new { data = new[] { new { parameters } } };

                //string rep = JsonConvert.SerializeObject(data1);

                //var data2 = new
                //{
                //    data = new[] { new {
                //        Percent_users_registers_active = 45,
                //        Count_users_registers_premium_plus = 800
                //    } }
                //};

                //string rep = JsonConvert.SerializeObject(data2);

                //request.AddJsonBody(rep);


                //request.AddJsonBody();



                //string modifiedJsonString = jsonObject.ToString();





                if (result == null)
                    throw new Exception("Error llamando al servidor");

                string content = result.Content.ToString();

                if (string.IsNullOrEmpty(content))
                    throw new Exception("Error con el contenido de la respuesta");

                if (!result.IsSuccessful)
                    throw new Exception(content);

                if (result.IsSuccessful)
                {
                    return new RestResponseModel
                    {
                        Success = true,
                        Message = result.Content,
                    };
                }
                else
                {
                    return new RestResponseModel
                    {
                        Success = false,
                        Message = result.Content,
                    };
                }
            }
            catch (Exception ex)
            {
                return new RestResponseModel
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RestResponseModel> CallMethodPostDataBoxRestAsync(string controller, string data, string token = "")
        {
            try
            {
                // Replace with your API key
                string apiKey = "blh4n57osvkg0gl15jqam8";

                // Replace with your metric ID
                string metricId = "Users_actives_key";

                // Replace with your metric value
                int metricValue = 2350;

                // Create a new HttpClient instance
                using var client = new HttpClient();

                // Set the base URL for the Databox API
                //client.BaseAddress = new Uri("https://api.databox.com/");

                // Set the content type to JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Set the API key in the Authorization header
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

                // Create a JSON payload with the metric value
                var payload = new
                {
                    data = new[]
                    {
                        new
                        {
                            key = "Users_actives_key",
                            value = metricValue
                        }
                    }
                };

                // Serialize the payload to JSON
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                // Create a new StringContent instance with the JSON payload
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a PUT request to update the metric
                var response = await client.PutAsync($"https://push.databox.com/metrics/{metricId}/data", content);

                if (response == null)
                    throw new Exception("Error llamando al servidor");

                //string content = result.Content.ToString();

                //if (string.IsNullOrEmpty(content))
                //    throw new Exception("Error con el contenido de la respuesta");

                //if (!result.IsSuccessful)
                //    throw new Exception(content);

                if (response.IsSuccessStatusCode)
                {
                    return new RestResponseModel
                    {
                        Success = true,
                        Message = "OK",
                    };
                }
                else
                {
                    return new RestResponseModel
                    {
                        Success = false,
                        Message = "ERROR",
                    };
                }
            }
            catch (Exception ex)
            {
                return new RestResponseModel
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public class Data
        {
            public int my_metric_key { get; set; }
        }
    }
}
