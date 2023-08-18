using System.ComponentModel.DataAnnotations;

namespace News.WebUI.Entities.Concrete
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
