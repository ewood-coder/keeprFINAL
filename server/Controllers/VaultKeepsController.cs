namespace keeprFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
	private readonly VaultKeepsService _vaultKeepsService;
	private readonly Auth0Provider _auth0Provider;



	public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
	{
		_vaultKeepsService = vaultKeepsService;
		_auth0Provider = auth0Provider;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE VAULTKEEP
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			vaultKeepData.CreatorId = userInfo.Id;

			VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
			return Ok(vaultKeep);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET VAULTKEEP BY ID
	[HttpGet("{vaultKeepId}")]
	public ActionResult<VaultKeep> GetVaultKeepById(int vaultKeepId)
	{
		try
		{
			VaultKeep vaultKeep = _vaultKeepsService.GetVaultKeepById(vaultKeepId);
			return Ok(vaultKeep);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: DELETE VAULTKEEP
	[Authorize]
	[HttpDelete("{vaultKeepId}")]
	public async Task<ActionResult<string>> DestroyVaultKeep(int vaultKeepId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			string message = _vaultKeepsService.DestroyVaultKeep(vaultKeepId, userInfo.Id);
			return Ok(message);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}
}