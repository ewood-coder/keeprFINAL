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

		// VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new {vaultKeepData} =>
		// {
		// 	return vaultKeep;
		// }, new { vaultKeepData }).FirstOrDefault();

		// VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepData }).FirstOrDefault();

		return vaultKeep;
	}



	// STUB: GET KEEPS IN PUBLIC✅ VAULT
	internal List<VaultKeep> GetKeepsByPublicVaultId(int vaultId)
	{
		string sql = @"
			SELECT 
			vaultKeeps.*,
			vaults.* 
			FROM vaultKeeps 
			JOIN vaults ON vaultKeeps.vaultId = vaults.id
			WHERE vaultKeeps.vaultId = @vaultId;";


		List<VaultKeep> vaultKeeps = _db.Query<VaultKeep, Vault, VaultKeep>(sql, (vaultKeep, vault) =>
		{
			vaultKeep.CreatorId = vault.CreatorId;
			vaultKeep.VaultId = vault.Id;
			return vaultKeep;
		}, new { vaultId }).ToList();
		return vaultKeeps;
	}


	// STUB: GET KEEPS IN PRIVATE❌ VAULT



	// STUB: CREATE VAULTKEEP
}