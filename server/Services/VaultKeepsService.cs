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



	// STUB: GET KEEPS IN PUBLIC✅ VAULT
	internal List<VaultKeep> GetKeepsByPublicVaultId(int vaultId)
	{
		List<VaultKeep> vaultKeeps = _vaultKeepsRepository.GetKeepsByPublicVaultId(vaultId);
		return vaultKeeps;
	}


	// STUB: GET KEEPS IN PRIVATE❌ VAULT



	// STUB: CREATE VAULTKEEP
}