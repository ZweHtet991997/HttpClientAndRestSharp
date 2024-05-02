using Newtonsoft.Json;
using RestSharp;
using ShareEntities;

namespace Consume_Backend_Api_Project.ApiConsumeServices.RestSharpService
{
    public class EmployeeApiService_2
    {
        #region Constructor
        private IConfiguration _configuration;
        private RestClient _restClient;
        public EmployeeApiService_2(IConfiguration configuration)
        {
            _configuration = configuration;
            _restClient = new RestClient(_configuration.GetValue<string>("BackendApiUrl"));
        }
        #endregion

        //Get Method
        public async Task<List<EmployeeEntities>> GetEmployee()
        {
            List<EmployeeEntities> lstEmployee = new List<EmployeeEntities>();
            RestRequest request = new RestRequest("api/employee", Method.Get);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                lstEmployee = JsonConvert.DeserializeObject<List<EmployeeEntities>>(response.Content);
            }
            return lstEmployee;
        }

        //Post Method
        public async Task<bool> CreateEmployee(EmployeeEntities model)
        {
            RestRequest request = new RestRequest("api/employee").AddJsonBody(model);
            RestResponse response = await _restClient.ExecutePostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //Put Method
        public async Task<bool> UpdateEmployee(EmployeeEntities model)
        {
            RestRequest request = new RestRequest("api/employee").AddJsonBody(model);
            RestResponse response = await _restClient.ExecutePutAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //Delete Method
        public async Task<bool> DeleteEmployee(int Id)
        {
            RestRequest request = new RestRequest($"api/employee/{Id}", Method.Delete);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //Get Method with parameter pass
        public async Task<EmployeeEntities> GetEmployeeById(int Id)
        {
            EmployeeEntities employee = new EmployeeEntities();
            RestRequest request = new RestRequest($"api/getemployee/{Id}", Method.Get);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                employee = JsonConvert.DeserializeObject<EmployeeEntities>(response.Content);
            }
            return employee;
        }

        //Pass Bearer token in HttpClient request
        public async Task<List<EmployeeEntities>> GetEmployeeListByAuthentication(string accessToken)
        {
            List<EmployeeEntities> lstEmployee = new List<EmployeeEntities>();
            RestRequest request = new RestRequest("api/employee");
            request.AddHeader("Authorization", accessToken);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                lstEmployee = JsonConvert.DeserializeObject<List<EmployeeEntities>>(response.Content);
            }
            return lstEmployee;
        }
    }
}
