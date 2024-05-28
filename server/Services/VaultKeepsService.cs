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



	// // STUB: GET KEEPS BY VAULT ID
	internal List<Keep> GetKeepsByVaultId(int vaultId, string? userId)
	{
		List<Keep> keeps = _vaultKeepsRepository.GetKeepsByVaultId(vaultId, userId);
		return keeps;
	}



	// STUB: GET VAULTKEEP BY ID
	internal VaultKeep GetVaultKeepById(int vaultKeepId)
	{
		VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);

		if (vaultKeep == null)
		{
			throw new Exception("NO VAULTKEEP FOUND WITH THAT ID 🙎‍♂️❌");
		}

		return vaultKeep;
	}



	// STUB: DELETE VAULTKEEP
	internal string DestroyVaultKeep(int vaultKeepId, string userId)
	{
		VaultKeep vaultKeepToDestroy = GetVaultKeepById(vaultKeepId);

		if (vaultKeepToDestroy.CreatorId != userId)
		{
			throw new Exception("CAN'T DELETE SOMEONE ELSE'S VAULTKEEP, FRIEND-O 🙎‍♂️❌");
		}

		_vaultKeepsRepository.DestroyVaultKeep(vaultKeepId);

		return $"You've successfully deleted the vaultKeep with the ID: '{vaultKeepId}'";
	}
}