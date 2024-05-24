using Nancy.Json;
using Newtonsoft.Json;
using System.Text;
using TaskMWeb.Models;
using TaskMWeb.Services.Interface;

namespace TaskMWeb.Services7
{
    public class LoginService : ILogin
    {
        string BaseURL = "https://localhost:7032/api/Login/Login";
        private IConfiguration _configuration;
        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public LoginM Login(LoginM loginModel)
        {
            LoginM data; 
            using (var client = new HttpClient())
                {
                    var loginModel1 = JsonConvert.SerializeObject(loginModel);
                    client.BaseAddress = new Uri(BaseURL);
                    HttpContent inputContent = new StringContent(loginModel1, Encoding.UTF8, "application/json");
                    var logintask = client.PostAsync("Login", inputContent).Result;
                   
                    data = (new JavaScriptSerializer()).Deserialize<LoginM>(logintask.Content.ReadAsStringAsync().Result);
                } 
            return data;
        }
    }
}
