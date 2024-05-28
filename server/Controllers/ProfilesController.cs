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
}
