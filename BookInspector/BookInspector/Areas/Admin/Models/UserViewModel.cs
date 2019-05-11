using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookInspector.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "user name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "e-mail is required")]
        public string Email { get; set; }
        
        //public Dictionary<string, bool> Roles { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }

    public class RoleViewModel
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
