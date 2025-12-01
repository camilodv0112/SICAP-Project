using Newtonsoft.Json;
using Presentacion.Repositories.IRepositories;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public AuthRepository(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<string> LoginPersonalAsync(string usuario, string password)
        {
            var loginDto = new { Usuario = usuario, Contraseña = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"{_endpoint}/login/personal", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseData);
                string token = result?.token;
                
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("El servidor no devolvió un token válido.");
                }
                
                return token;
            }
            else
            {
                var errorData = await response.Content.ReadAsStringAsync();
                string errorMessage = "Credenciales inválidas";
                
                try
                {
                    dynamic errorResult = JsonConvert.DeserializeObject(errorData);
                    if (errorResult?.Message != null)
                    {
                        errorMessage = errorResult.Message.ToString();
                    }
                }
                catch
                {
                }
                
                throw new Exception(errorMessage);
            }
        }

        public async Task<string> LoginEstudianteAsync(string usuario, string password)
        {
            var loginDto = new { Usuario = usuario, Contraseña = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"{_endpoint}/login/estudiante", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseData);
                string token = result?.token;
                
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("El servidor no devolvió un token válido.");
                }
                
                return token;
            }
            else
            {
                var errorData = await response.Content.ReadAsStringAsync();
                string errorMessage = "Credenciales inválidas";
                
                try
                {
                    dynamic errorResult = JsonConvert.DeserializeObject(errorData);
                    if (errorResult?.Message != null)
                    {
                        errorMessage = errorResult.Message.ToString();
                    }
                }
                catch
                {
                    // Si no se puede parsear el error, usar el mensaje por defecto
                }
                
                throw new Exception(errorMessage);
            }
        }
    }
}
