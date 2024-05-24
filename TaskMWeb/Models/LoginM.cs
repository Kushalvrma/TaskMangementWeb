using System.ComponentModel.DataAnnotations;

namespace TaskMWeb.Models
{
    public class LoginM
    {
        public int id { get; set; }
        public string? email { get; set; }        
        public string? password { get; set; }        
        public string? name { get; set; }
        public int roleId { get; set; }
        public string? phonenumber { get; set; }
    }
}
