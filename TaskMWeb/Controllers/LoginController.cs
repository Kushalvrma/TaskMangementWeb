using Microsoft.AspNetCore.Mvc;
using TaskMWeb.Models;
using TaskMWeb.Services.Interface;
using TaskMWeb.Services7;


namespace TaskMWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
    
        private readonly ILogin loginServices;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            loginServices = new LoginService(configuration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginM loginModel)
        {
            LoginM userData = new LoginM();            
            userData = loginServices.Login(loginModel);
            
            LoginM data = new LoginM()
            {
                email = userData.email,               
                password = userData.password,
                id = userData.id,
                phonenumber = userData.phonenumber,
                name = userData.name,
                roleId = userData.roleId
            };
            HttpContext.Session.SetInt32("userID", userData.id);
            return Json(data);
        }
       
    }
}
