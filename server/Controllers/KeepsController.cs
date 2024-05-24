namespace keeprFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
	private readonly KeepsService _keepsService;
	private readonly Auth0Provider _auth0Provider;

	public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
	{
		_keepsService = keepsService;
		_auth0Provider = auth0Provider;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE KEEP
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			keepData.CreatorId = userInfo.Id;
			Keep keep = _keepsService.CreateKeep(keepData);
			return Ok(keep);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET ALL KEEPS
	[HttpGet]
	public async Task<ActionResult<List<Keep>>> GetAllKeeps()
	{
		try
		{
			// We can still see who is logged in even if the route is not authorized
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			// NOTE userInfo?.Id does not drill into userInfo if null (user is not logged in)
			List<Keep> keeps = _keepsService.GetAllKeeps(userInfo?.Id);
			return Ok(keeps);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: GET KEEP BY ID
	// NOTE not an authorized route
	[HttpGet("{keepId}")]
	public async Task<ActionResult<Keep>> GetKeepById(int keepId)
	{
		try
		{
			// We can still see who is logged in even if the route is not authorized
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

			// NOTE userInfo?.Id does not drill into userInfo if null (user is not logged in)
			Keep keep = _keepsService.IncrementViews(keepId, userInfo?.Id);
			return Ok(keep);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: UPDATE KEEP
	[Authorize]
	[HttpPut("{keepId}")]
	public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep keepData)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			Keep keep = _keepsService.UpdateKeep(keepId, userInfo.Id, keepData);
			return Ok(keep);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}



	// STUB: DELETE KEEP
	[Authorize]
	[HttpDelete("{keepId}")]
	public async Task<ActionResult<string>> DestroyKeep(int keepId)
	{
		try
		{
			Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
			string message = _keepsService.DestroyKeep(keepId, userInfo.Id);
			return Ok(message);
		}
		catch (Exception exception)
		{
			return BadRequest(exception.Message);
		}
	}
}
