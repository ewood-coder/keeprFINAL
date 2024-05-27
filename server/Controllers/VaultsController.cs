namespace keeprFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
	private readonly VaultsService _vaultsService;
	private readonly Auth0Provider _auth0Provider;
	private readonly VaultKeepsService _vaultKeepsService;



	public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
	{
		_vaultsService = vaultsService;
		_auth0Provider = auth0Provider;
		_vaultKeepsService = vaultKeepsService;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE VAULT
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			vaultData.CreatorId = userInfo.Id;
			Vault vault = _vaultsService.CreateVault(vaultData);
			return Ok(vault);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET ALL VAULTS
	// [HttpGet]
	// public async Task<ActionResult<List<Vault>>> GetAllVaults()
	// {
	// 	try
	// 	{
	// 		// We can still see who is logged in even if the route is not authorized
	// 		Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

	// 		// NOTE userInfo?.Id does not drill into userInfo if null (user is not logged in)
	// 		List<Vault> vaults = _vaultsService.GetAllVaults(userInfo?.Id);
	// 		return Ok(vaults);
	// 	}
	// 	catch (Exception exception)
	// 	{
	// 		return BadRequest(exception.Message);
	// 	}
	// }



	// STUB: GET VAULT BY ID
	// NOTE not an authorized route
	[HttpGet("{vaultId}")]
	public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
	{
		try
		{
			// We can still see who is logged in even if the route is not authorized
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
			return Ok(vault);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: UPDATE VAULT
	[Authorize]
	[HttpPut("{vaultId}")]
	public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			Vault vault = _vaultsService.UpdateVault(vaultId, userInfo.Id, vaultData);
			return Ok(vault);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: DELETE VAULT
	[Authorize]
	[HttpDelete("{vaultId}")]
	public async Task<ActionResult<string>> DestroyVault(int vaultId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			string message = _vaultsService.DestroyVault(vaultId, userInfo.Id);
			return Ok(message);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET KEEPS BY VAULT ID
	[Authorize]
	[HttpGet("{vaultId}/keeps")]
	public async Task<ActionResult<List<VaultKeep>>> GetKeepsByVaultId(int vaultId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			List<K> keeps = _vaultKeepsService.GetKeepsByVaultId(vaultId, userInfo.Id);
			return Ok(keeps);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}
}
