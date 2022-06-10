using System.ComponentModel.DataAnnotations;

namespace assetManagementBackend.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_surname { get; set; }
        public string user_email { get; set; }
        public bool user_role { get; set; } //true: admin / false: user
        public bool isActive { get; set; }
        public string user_adress { get; set; }
        public string user_password { get; set; }   
      
    }
}