using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class LoginViewModel_past_ver
    {
        [Required(ErrorMessage ="Введите имя пользователя")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { set; get; }        
    }
}