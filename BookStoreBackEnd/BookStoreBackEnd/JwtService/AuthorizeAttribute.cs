using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _privilege;

    public AuthorizeAttribute(string privilege = "")
    {
        _privilege = privilege;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous) return;

        var roleId = context.HttpContext.Items["RoleId"];
        foreach (var key in context.HttpContext.Items.Keys)
        {
            // Get the value associated with the key
            var value = context.HttpContext.Items[key];

            // Print the key and value
            Console.WriteLine($"Key: {key}, Value: {value}");
        }
        if (roleId == null)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Unauthorized" });
            return;
        }

        var db = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

        if (db == null)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return;
        }

        if (!ValidatePrivilege(Convert.ToInt32(roleId), db))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }
    }

    private bool ValidatePrivilege(int roleId, ApplicationDbContext db)
    {
        var roleExist = db.Roles.Any(x => x.RoleId == roleId);
        if (!roleExist) return false;

        if (string.IsNullOrWhiteSpace(_privilege)) return true;

        return db.RolePrivilege
            .Any(x => x.RoleId == roleId && x.Privilege == _privilege);
    }
}
