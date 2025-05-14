using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SPL_Diplom_Winki_Trippi_Sabi.Models
{
    public class UserWithRolesViewModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

        public string SelectedRole { get; set; }
    }
}
