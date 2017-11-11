using System;
using System.Collections.Generic;
using System.Text;

namespace WordReminder.Data.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
    }
}
