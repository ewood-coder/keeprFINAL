namespace keeprFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
	private readonly ProfilesService _profilesService;
	private readonly Auth0Provider _auth0Provider;

	public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider)
	{
		_profilesService = profilesService;
		_auth0Provider = auth0Provider;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: GET USERS PROFILE BY ID
	[HttpGet("{profileId}")]
	public ActionResult<Profile> GetProfileById(string profileId)
	{
		try
		{
			Profile profile = _profilesService.GetProfileById(profileId);
			return Ok(profile);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET USERS KEEPS BY ID
	[HttpGet("{profileId}/keeps")]
	public ActionResult<List<Keep>> GetKeepsByUserId(string profileId)
	{
		try
		{
			List<Keep> userKeeps = _profilesService.GetKeepsByUserId(profileId);
			return Ok(userKeeps);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET USERS VAULTS BY ID
	[HttpGet("{profileId}/vaults")]
	public async Task<ActionResult<List<Vault>>> GetVaultsByUserId(string profileId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			List<Vault> userVaults = _profilesService.GetVaultsByUserId(profileId, userInfo?.Id);
			return Ok(userVaults);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}
}
