using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebProject.Extensions
{
    public static class ClaimGetExtension
    {
        public static string GetUserId(this ClaimsPrincipal userClaimsPrincipal)
        {
            var userId = userClaimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
