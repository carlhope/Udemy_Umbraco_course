using Lucene.Net.Analysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Org.BouncyCastle.Tls;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Udemy_Umbraco_course.Controllers
{
	[ApiController]
	[Route("connect/{action}")]
	public class AuthTokenController : ControllerBase
	{
		private readonly IOpenIddictScopeManager _scopeManager;
		private readonly IOpenIddictApplicationManager _applicationManager;
		public AuthTokenController(IOpenIddictApplicationManager applicationManager, IOpenIddictScopeManager scopeManager)
		{
			_scopeManager = scopeManager;
			_applicationManager = applicationManager;
		}
		[HttpPost]
		public async Task<IActionResult> Token()
		{
			var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

			if (request.IsClientCredentialsGrantType())
			{
				var application = await _applicationManager.FindByClientIdAsync(request.ClientId);
				if (application == null)
				{
					throw new InvalidOperationException("Client was not found in the database.");
				}


				var identity = new ClaimsIdentity(
					authenticationType: TokenValidationParameters.DefaultAuthenticationType,
					nameType: Claims.Name, 
					roleType: Claims.Role
					);

				identity
					.SetClaim(Claims.Subject, request.ClientId)
					.SetClaim(Claims.Name, await _applicationManager.GetDisplayNameAsync(application))
					.SetClaims(Claims.Role, new[] { "ClientApplication" }.ToImmutableArray());

				identity.SetScopes(request.GetScopes());
				identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
				identity.SetDestinations(GetDestinations);

				return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

			}

			throw new InvalidOperationException("The specified grant type is not supported.");

		}
		private IEnumerable<string> GetDestinations(Claim claim)
		{
			return claim.Type switch
			{
				Claims.Name or
				Claims.Subject
						=> new[] { Destinations.AccessToken, Destinations.IdentityToken },
				_ => new[] { Destinations.AccessToken }
			};
		}
	}
}
