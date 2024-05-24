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



	// STUB: GET KEEPS IN PUBLIC ✅ VAULT



	// STUB: GET KEEPS IN PRIVATE ❌ VAULT



	// STUB: CREATE VAULTKEEP
}