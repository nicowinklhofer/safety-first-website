using Microsoft.AspNetCore.Mvc;

namespace SPL_Diplom_Winki_Trippi_Sabi.Models
{
   
        public class EditUserRolesViewModel
        {
            public string UserId { get; set; }
            public string Email { get; set; }
            public List<RoleSelection> Roles { get; set; }
        }

        public class RoleSelection
        {
            public string RoleName { get; set; }
            public bool Selected { get; set; }
        }

    }

