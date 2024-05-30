// using keeprFINAL.Interfaces;
using System.Runtime.CompilerServices;


namespace keeprFINAL.Repositories;

// NOTE: ONLY USE THIS LINE IF YOU PLAN ON USING LINE 1
// public class VaultKeepsRepository : IRepository<VaultKeep>
public class VaultKeepsRepository
{
	private readonly IDbConnection _db;
	private readonly KeepsRepository _keepsRepository;

	public VaultKeepsRepository(IDbConnection db, KeepsRepository keepsRepository)
	{
		_db = db;
		_keepsRepository = keepsRepository;
	}



	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE VAULTKEEP
	internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
	{
		string vaultSQL = @"
			SELECT * FROM vaults WHERE id = @VaultId;";

		Vault vault = _db.Query<Vault>(vaultSQL, new { vaultId = vaultKeepData.VaultId }).FirstOrDefault();
		if (vault == null)
		{
			throw new Exception("Invalid Id");
		}

		if (vault.CreatorId != vaultKeepData.CreatorId)
		{
			throw new Exception("You don't have access");
		}


		string sql = @"
			INSERT INTO
			vaultKeeps(creatorId, keepId, vaultId)
			VALUES(@CreatorId, @KeepId, @VaultId);
			
			SELECT * FROM vaultKeeps WHERE id = LAST_INSERT_ID();";

		VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
		int Kept = _keepsRepository.GetKeptCount(vaultKeep.KeepId);

		return vaultKeep;

	}



	// STUB: GET KEEPS BY VAULT ID
	internal List<Keep> GetKeepsByVaultId(int vaultId, string? userId)
	{
		if (userId == null)
		{
			// it messes up sql if its null and not a string
			userId = "0";
		}
		string vaultSQL = @"
			SELECT * FROM vaults WHERE id = @vaultId;";

		Vault vault = _db.Query<Vault>(vaultSQL, new { vaultId }).FirstOrDefault();

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



	// STUB: GET VAULTKEEP BY ID
	internal VaultKeep GetVaultKeepById(int vaultKeepId)
	{
		string sql = "SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";

		VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();

		return vaultKeep;
	}



	// STUB: DELETE VAULTKEEP
	internal void DestroyVaultKeep(int vaultKeepId)
	{
		string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";

		int rowsAffected = _db.Execute(sql, new { vaultKeepId });

		if (rowsAffected == 0)
		{
			throw new Exception("DELETE failed!");
		}

		if (rowsAffected > 1)
		{
			throw new Exception("UH OH, MORE THAN ONE ROW WAS AFFECTED. GET HELP!");
		}
	}
}