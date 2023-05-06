using DasboardAloha.Entities.Configuration.ModelsConfiguration;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasboardAloha.Entities.Helpers
{
    public class RestHelper
    {
        public async Task<RestResponseModel> CallMethodPostDataBoxAsync(string controller, string data, string token = "")
        {
            try
            {
                string apiUrl;

                bool isProduccion = false;

                if (isProduccion)
                    apiUrl = "https://push.databox.com";
                else
                    apiUrl = "https://push.databox.com";

                RestClient client = new(apiUrl);

                if (!string.IsNullOrEmpty(token))
                    client.Authenticator = new JwtAuthenticator(token);

                RestRequest request = new()
                {
                    Method = Method.Post
                };

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/vnd.databox.v2+json");
                request.AddHeader("Authorization", "5075s9d4qz4ss0s0kc8gsccw4csgs0sk");

                request.AddJsonBody("{\"data\":[{\"$<Total_users_actives>\":2550}]}");

                RestResponse result = await client.ExecuteAsync(request);

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

        public class Data
        {
            public int my_metric_key { get; set; }
        }
    }
}
