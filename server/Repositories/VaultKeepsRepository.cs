// using keeprFINAL.Interfaces;
using System.Runtime.CompilerServices;


namespace keeprFINAL.Repositories;

// NOTE: ONLY USE THIS LINE IF YOU PLAN ON USING LINE 1
// public class VaultKeepsRepository : IRepository<VaultKeep>
public class VaultKeepsRepository
{
	private readonly IDbConnection _db;

	public VaultKeepsRepository(IDbConnection db)
	{
		_db = db;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE VAULTKEEP
	internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
	{
		string sql = @"
			INSERT INTO
			vaultKeeps(creatorId, keepId, vaultId)
			VALUES(@CreatorId, @KeepId, @VaultId);

			SELECT 
			vaultKeeps.*,
			vaults.*,
			accounts.* 
			FROM vaultKeeps
			JOIN vaults ON vaultKeeps.vaultId = vaults.id
    		JOIN accounts ON vaults.creatorId = accounts.id
			WHERE vaultKeeps.id = LAST_INSERT_ID();";


		VaultKeep vaultKeep = _db.Query<VaultKeep, Vault, Profile, VaultKeep>
		(sql, (vaultKeep, vault, account) =>
		{
			vaultKeep.CreatorId = vault.CreatorId;
			vaultKeep.VaultId = vault.Id;
			vaultKeep.CreatorId = account.Id;
			return vaultKeep;
		}, vaultKeepData).FirstOrDefault();

		return vaultKeep;
	}



	// STUB: GET KEEPS BY VAULT ID
	internal List<Keep> GetKeepsByVaultId(int vaultId, string userId)
	{
		string vaultSQL = @"
			SELECT * FROM vaults WHERE id = @vaultId;";

		Vault vault = _db.Query<Vault>(vaultSQL, new { vaultId }).First();

		if (vault == null)
		{
			throw new Exception($"Invalid Vault Id: {vaultId}");
		}

		if (vault.IsPrivate && vault.CreatorId != userId)
		{
			throw new Exception("This vault is private and you are not the creator");
		}

		string sql = @"
			SELECT
			keeps.*, vaultKeeps.id as vaultKeepId, accounts.*
			FROM keeps
			JOIN vaultKeeps ON keeps.id = vaultKeeps.keepId 
			JOIN vaults ON vaultKeeps.vaultId = vaults.id
			JOIN accounts ON vaults.creatorId = accounts.id
			WHERE vaultKeeps.vaultId = @vaultId AND (vaults.IsPrivate = false OR vaults.CreatorId = @userId);";

		List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
		{
			keep.Creator = account;
			return keep;
		}, new { vaultId, userId }).ToList();

		return keeps;
	}



	// STUB: DELETE VAULTKEEP
}