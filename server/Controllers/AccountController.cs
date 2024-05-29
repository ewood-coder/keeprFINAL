namespace keeprFINAL.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
	private readonly AccountService _accountService;
	private readonly Auth0Provider _auth0Provider;
	private readonly ProfilesService _profilesService;


	public AccountController(AccountService accountService, Auth0Provider auth0Provider, ProfilesService profilesService)
	{
		_accountService = accountService;
		_auth0Provider = auth0Provider;
		_profilesService = profilesService;
	}




	[HttpGet]
	[Authorize]
	public async Task<ActionResult<Account>> Get()
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			return Ok(_accountService.GetOrCreateProfile(userInfo));
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}



	// STUB: UPDATE ACCOUNT
	[HttpPut]
	[Authorize]
	public async Task<ActionResult<Account>> Update([FromBody] Account updatedAccount)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			updatedAccount.Id = userInfo.Id;
			return Ok(_accountService.Edit(updatedAccount, userInfo.Email));
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}



	// STUB: GET LOGGED IN USERS VAULTS
	[HttpGet("vaults")]
	[Authorize]
	public async Task<ActionResult<List<Vault>>> GetVaultsByAccountId()
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			return Ok(_profilesService.GetVaultsByUserId(userInfo.Id, userInfo.Id));
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}
