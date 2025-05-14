using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SPL_Diplom_Winki_Trippi_Sabi.Models;
using System.Linq;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

        var model = new List<UserWithRolesViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            model.Add(new UserWithRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles.ToList(),
                SelectedRole = roles.FirstOrDefault() ?? ""
            });
        }

        ViewBag.AllRoles = allRoles;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var model = new EditUserRolesViewModel
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = new List<RoleSelection>()
        };

        foreach (var role in _roleManager.Roles)
        {
            model.Roles.Add(new RoleSelection
            {
                RoleName = role.Name,
                Selected = await _userManager.IsInRoleAsync(user, role.Name)
            });
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditRoles(EditUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);
        var selectedRoles = model.Roles.Where(r => r.Selected).Select(r => r.RoleName);

        await _userManager.RemoveFromRolesAsync(user, userRoles);
        await _userManager.AddToRolesAsync(user, selectedRoles);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeRole(string id, string newRole)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, newRole);

        return RedirectToAction("Index");
    }
}
