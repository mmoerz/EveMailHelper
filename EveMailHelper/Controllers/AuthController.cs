using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EVEStandard;
using EVEStandard.Models.SSO;

using EveMailHelper.DataAccessLayer;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLibrary.Managers;

namespace EveMailHelper.Controllers
{
    [Route("/[controller]")]
    public class AuthController : Controller
    {
        //private readonly EVEStandardAPI esiClient;
        //private readonly EveNatDbContext eveNatDbContext;

        private readonly IAuthenticationManager _authManager;

        public AuthController(IAuthenticationManager authenticationManager)
        {
            _authManager = authenticationManager;
        }

        [HttpGet("[action]")]
        public IActionResult Login(string? returnUrl = null)
        {
            // Scopes are required for API calls but not for authentication, dummy scope is inserted to workaround an issue in the library
            var scopes = new List<string>()
            {
                "esi-location.read_location.v1",
                "publicData",
                "esi-skills.read_skills.v1",
                "esi-planets.manage_planets.v1",
                "esi-location.read_online.v1",
                "esi-planets.read_customs_offices.v1",
                "esi-characters.read_loyalty.v1",
            };
            
            return Redirect(_authManager.GetEveAuthorizationUrl(scopes));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            Guid.TryParse(state, out Guid stateGuid);
            
            if (!_authManager.IsValidCharacterAuthInfoId(stateGuid))
            {
                // log not found here
                return Redirect("/error");
            }

            await _authManager.FinalizeEveAuthentication(code, state);

            return Redirect("/characterList");
        }
    }
}