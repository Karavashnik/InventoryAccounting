using System.Reflection;
using System.Threading.Tasks;
using InventoryAccounting.Models;
using InventoryAccounting.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace InventoryAccounting.Areas.Identity.Pages.Account.Manage
{
    public class ResetAuthenticatorModel : PageModel
    {
        UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        ILogger<ResetAuthenticatorModel> _logger;
        private readonly IStringLocalizer _identityLocalizer;

        public ResetAuthenticatorModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<ResetAuthenticatorModel> logger,
            IStringLocalizerFactory factory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

            var type = typeof(IdentityResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _identityLocalizer = factory.Create("IdentityResource", assemblyName.Name);
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_identityLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_identityLocalizer["USER_NOTFOUND", _userManager.GetUserId(User)]);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = _identityLocalizer["RESET_AUTHENTICATOR_STATUS"];

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}