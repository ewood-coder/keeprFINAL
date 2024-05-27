namespace keeprFINAL.Services;

public class VaultKeepsService
{
	private readonly VaultKeepsRepository _vaultKeepsRepository;
	private readonly VaultsService _vaultsService;


	public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
	{
		_vaultKeepsRepository = vaultKeepsRepository;
		_vaultsService = vaultsService;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE VAULTKEEP
	internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
	{
		VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
		return vaultKeep;
	}



	// STUB: GET KEEPS BY VAULT ID
	internal List<Keep> GetKeepsByVaultId(int vaultId, string userId)
	{
		List<Keep> keeps = _vaultKeepsRepository.GetKeepsByVaultId(vaultId, userId);
		return keeps;
	}



	// STUB: DELETE VAULTKEEP
}