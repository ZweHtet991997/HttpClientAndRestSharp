using Newtonsoft.Json;
using ShareEntities;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text.Json.Serialization;

namespace Consume_Backend_Api_Project.ApiConsumeServices.HttpClientService
{
    //consume api using HttpClient
    public class EmployeeApiService_1
    {
        #region Constructor
        private IConfiguration _configuration;
        //use HttpClientHandler to prevent the SSL connection could not be established error
        //when calling to the target endpoint
        HttpClientHandler clientHandler = new HttpClientHandler();
        public EmployeeApiService_1(IConfiguration configuration)
        {
            _configuration = configuration;
            clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, SslPolicyErrors) => { return true; };
        }
        #endregion

        //Get Method
        public async Task<List<EmployeeEntities>> GetEmployee()
        {
            using (var client = new HttpClient(clientHandler))
            {
                List<EmployeeEntities> lstEmployee = new List<EmployeeEntities>();
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BackendApiUrl"));
                var response = client.GetAsync("api/employee");

                var dataResult = response.Result;
                if (dataResult.IsSuccessStatusCode)
                {
                    lstEmployee = await dataResult.Content.ReadFromJsonAsync<List<EmployeeEntities>>();
                    goto Results;
                }
            Results:
                return lstEmployee;
            }
        }

        //Post Method
        public async Task<bool> CreateEmployee(EmployeeEntities model)
        {
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BackendApiUrl"));
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync("api/employee", model);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        //Put Method
        public async Task<bool> UpdateEmployee(EmployeeEntities model)
        {
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BackendApiUrl"));
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync("api/employee", model);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        //Delete Method
        public async Task<bool> DeleteEmployee(int Id)
        {
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BackendApiUrl"));
                HttpResponseMessage responseMessage = await client.DeleteAsync("api/employee/" + Id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        //Get Method with parameter pass
        public async Task<EmployeeEntities> GetEmployeeById(int Id)
        {
            using (var client = new HttpClient(clientHandler))
            {
                EmployeeEntities employee = new EmployeeEntities();
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BackendApiUrl"));
                HttpResponseMessage responseMessage = await client.GetAsync("api/getemployee/" + Id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    employee = await responseMessage.Content.ReadFromJsonAsync<EmployeeEntities>();
                    goto Result;
                }
            Result:
                return employee;
            }
        }

        //Pass Bearer token in HttpClient request
        public async Task<List<EmployeeEntities>> GetEmployeeListByAuthentication(string accessToken)
        {
            using (var client = new HttpClient(clientHandler))
            {
                List<EmployeeEntities> lstEmployee = new List<EmployeeEntities>();
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BackendApiUrl"));
                //pass authorization token from header
                client.DefaultRequestHeaders.Add("Authorization", "" + accessToken);
                var response = client.GetAsync("api/employee");

                var dataResult = response.Result;
                if (dataResult.IsSuccessStatusCode)
                {
                    lstEmployee = await dataResult.Content.ReadFromJsonAsync<List<EmployeeEntities>>();
                    goto Results;
                }
                else if (dataResult.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return null;
                }
            Results:
                return lstEmployee;
            }
        }
    }
}

