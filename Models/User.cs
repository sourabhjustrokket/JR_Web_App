using System.ComponentModel.DataAnnotations.Schema;

namespace JR_Web_App.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}