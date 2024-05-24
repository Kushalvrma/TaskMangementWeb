using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using TaskMWeb.Models;

namespace TaskMWeb.Controllers
{
    public class HomeController : Controller
    {
        readonly string baseURL = "https://localhost:7032/api/AddUser/";
        readonly string BaseURL = "https://localhost:7032/api/TaskM/";


        [HttpGet]   
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.GetInt32("userID");
            List<AddUser> adduser = new List<AddUser>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("GetData");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    adduser = JsonConvert.DeserializeObject<List<AddUser>>(result);
                }
                else
                {
                    Console.WriteLine("Error database not found");
                }              
            }
            return View("Index", adduser);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUser registerModel)
        {
           
            using (var client = new HttpClient())
            {
                var registermodel1 = JsonConvert.SerializeObject(registerModel);
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.PostAsJsonAsync("Postuser/", registerModel);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddUser addUser = new AddUser();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("EditGetDatabyid?id=" + id);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    addUser = JsonConvert.DeserializeObject<AddUser>(result);
                }
                else
                {
                    Console.WriteLine("Error database not found");
                }
            }
            ViewData["userDetails"] = addUser;
            ViewBag.userDetail = addUser;
            return Json(addUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddUser registerModel)
        {
            using (var client = new HttpClient())
            {
                var registerModel1 = JsonConvert.SerializeObject(registerModel);
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpContent inputDetails = new StringContent(registerModel1,System.Text.Encoding.UTF8,"application/json");
                HttpResponseMessage response = client.PostAsync("EdituserById?id=" + registerModel.Id, inputDetails).Result;
                var edit = response.Content.ReadAsStringAsync().Result;



                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");   

                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                               
                var delete = client.GetAsync("DeleteuserById?id=" + id);
                delete.Wait();

                var result = delete.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }





        //Task

        [HttpGet]
        public async Task<IActionResult> TaskGet()
        {
            List<TaskM> dt = new List<TaskM>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("TaskGetData");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    dt = JsonConvert.DeserializeObject<List<TaskM>>(result);

                }
                else
                {
                    Console.WriteLine("Error database not found");
                }
            }
            return View("TaskGet", dt);
        }

        [HttpGet]
        public  async Task<IActionResult> AddTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(string task_name, bool task_Status, int task_id, int user_id)
        {
            TaskM TaskModel = new TaskM();
            TaskModel.Task_Name = task_name;
            TaskModel.Task_Status = task_Status;
            TaskModel.Task_id = task_id;
            TaskModel.User_id = user_id;
            

            using (var client = new HttpClient())
            {
                var registermodel1 = JsonConvert.SerializeObject(TaskModel);
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.PostAsJsonAsync("TaskPostData/", TaskModel);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("TaskGet");
                }

            }
            return View();
        }
















        //Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }









        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}