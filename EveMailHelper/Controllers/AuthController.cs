﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EVEStandard;
using EVEStandard.Models.SSO;
using EVEStandard.Enumerations;

using EveMailHelper.DataAccessLayer;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLibrary.Managers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace EveMailHelper.Controllers
{
    [Route("/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IAuthenticationManager _authManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            AuthenticationStateProvider AuthenticationStateProvider,
            IAuthenticationManager authenticationManager,
            ILogger<AuthController> logger)
        {
            _authenticationStateProvider = AuthenticationStateProvider;
            _authManager = authenticationManager;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult Login(string? returnUrl = null)
        {
            // Scopes are required for API calls,
            // but not for authentication,
            //
            // mm> is that still necessary?
            // dummy scope is inserted to workaround an issue in the library
            var scopes = new List<string>()
            {
                Scopes.ESI_CALENDAR_READ_CALENDAR_EVENTS_1,
                Scopes.ESI_CALENDAR_RESPOND_CALENDAR_EVENTS_1,
                Scopes.ESI_MAIL_READ_MAIL_1,
                Scopes.ESI_MAIL_SEND_MAIL_1,
                Scopes.ESI_MAIL_ORGANIZE_MAIL_1,
            };

            _logger.LogInformation("login caught, redirecting to external auth");

            return Redirect(_authManager.GetEveAuthorizationUrl(scopes));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            // TODO: really log out here
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("logout caught");

            return Redirect("/");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            var result = Guid.TryParse(state, out Guid stateGuid);
            if (!result)
            {
                _logger.LogError("failed to convert state to Guid");
            }
            else if (!_authManager.IsValidCharacterAuthInfoId(stateGuid))
            {
                _logger.LogError("no character for Guid:{stateGuid}", stateGuid);
                return Redirect("/error");
            }

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var character = await _authManager.FinalizeEveAuthentication(user, code, state);
            await SignInAsync(user, character);

            return Redirect("/characterList");
        }

        private async Task SignInAsync(ClaimsPrincipal principal, DataModels.Character character)
        {
            var claimsIdentity = AuthenticationManager.GetClaimsPrincipal(principal, character);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddHours(24) });
        }
    }
}