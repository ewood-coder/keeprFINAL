namespace keeprFINAL.Services;

public class VaultsService
{
	private readonly VaultsRepository _vaultsRepository;

	public VaultsService(VaultsRepository vaultsRepository)
	{
		_vaultsRepository = vaultsRepository;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE VAULT
	internal Vault CreateVault(Vault vaultData)
	{
		Vault vault = _vaultsRepository.CreateVault(vaultData);
		return vault;
	}



	// STUB: GET ALL VAULTS
	// internal List<Vault> GetAllVaults()
	// {
	// 	List<Vault> vaults = _vaultsRepository.GetAllVaults();
	// 	return vaults;
	// }



	// STUB: GET VAULT BY ID -------------

	// NOTE overload: if you call GetVaultById and only supply an integer, this method runs
	// this is a private method, we are dealing with sensitive information, so this only callable by members of the service
	private Vault GetVaultById(int vaultId)
	{
		Vault vault = _vaultsRepository.GetVaultById(vaultId);

		if (vault == null)
		{
			throw new Exception($"Invalid id: {vaultId}");
		}

		return vault;
	}

	// NOTE overload: if you call GetVaultById and supply an integer and a string, this method runs
	internal Vault GetVaultById(int vaultId, string userId)
	{
		Vault vault = GetVaultById(vaultId); // private method only accessible by members of this class

		if (vault.CreatorId != userId)
		{
			throw new Exception($"Invalid id: {vaultId} 😉");
		}

		return vault;
	}



	// STUB: UPDATE VAULT
	internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
	{
		Vault vaultToUpdate = GetVaultById(vaultId);

		if (vaultToUpdate.CreatorId != userId)
		{
			throw new Exception("NOT YOUR VAULT! ❌");
		}

		vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
		vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
		vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
		if (vaultData.IsPrivate == null)
		{
			vaultData.IsPrivate = vaultToUpdate.IsPrivate;
		}
		vaultToUpdate.IsPrivate = vaultData.IsPrivate;

		Vault updatedVault = _vaultsRepository.UpdateVault(vaultToUpdate);

		return updatedVault;
	}



	// STUB: DELETE VAULT
	internal string DestroyVault(int vaultId, string userId)
	{
		Vault vaultToDestroy = GetVaultById(vaultId);

		if (vaultToDestroy.CreatorId != userId)
		{
			throw new Exception("YOU CAN'T DELETE OTHER PEOPLE'S VAULTS BRO");
		}

		_vaultsRepository.DestroyVault(vaultId);

		return $"The vault with the ID: '{vaultToDestroy.Id}' and NAME: '{vaultToDestroy.Name}' has been deleted!";
	}
}
