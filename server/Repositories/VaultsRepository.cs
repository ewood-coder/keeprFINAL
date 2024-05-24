// using keeprFINAL.Interfaces;
using System.Runtime.CompilerServices;


namespace keeprFINAL.Repositories;

// NOTE: ONLY USE THIS LINE IF YOU PLAN ON USING LINE 1
// public class VaultsRepository : IRepository<Vault>
public class VaultsRepository
{
	private readonly IDbConnection _db;

	public VaultsRepository(IDbConnection db)
	{
		_db = db;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: POPULATE CREATOR
	private Vault PopulateCreator(Vault vault, Profile creator)
	{
		vault.Creator = creator;
		return vault;
	}



	// STUB: CREATE VAULT
	internal Vault CreateVault(Vault vaultData)
	{
		string sql = @"
			INSERT INTO
			vaults(creatorId, name, description, img, isPrivate)
			VALUES(@CreatorId, @Name, @Description, @Img, @IsPrivate);

			SELECT
			vaults.*,
			accounts.*
			FROM vaults
			JOIN accounts ON accounts.id = vaults.creatorId
			WHERE vaults.id = LAST_INSERT_ID();";

		Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, vaultData).FirstOrDefault();

		return vault;
	}



	// STUB: GET ALL VAULTS
	// internal List<Vault> GetAllVaults()
	// {
	// 	string sql = @"
	// 		SELECT
	// 		vaults.*,
	// 		accounts.*
	// 		FROM vaults
	// 		JOIN accounts ON accounts.id = vaults.creatorId
	// 		ORDER BY vaults.createdAt;";

	// 	List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator).ToList();

	// 	return vaults;
	// }



	// STUB: GET VAULT BY ID
	internal Vault GetVaultById(int vaultId)
	{
		string sql = @"
			SELECT
			vaults.*,
			accounts.*
			FROM vaults
			JOIN accounts ON accounts.id = vaults.creatorId
			WHERE vaults.id = @vaultId;";

		Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, new { vaultId }).FirstOrDefault();

		return vault;
	}



	// STUB: UPDATE VAULT
	internal Vault UpdateVault(Vault vaultToUpdate)
	{
		string sql = @"
			UPDATE vaults
			SET
			name = @Name,
			description = @Description,
			img = @Img,
			isPrivate = @IsPrivate
			WHERE id = @Id;

			SELECT
			vaults.*,
			accounts.*
			FROM vaults
			JOIN accounts ON accounts.id = vaults.creatorId
			WHERE vaults.id = @Id;";


		Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, account) =>
		{
			vault.Creator = account;
			return vault;
		}, vaultToUpdate).FirstOrDefault();

		return vault;
	}



	// STUB: DELETE VAULT
	internal void DestroyVault(int vaultId)
	{
		string sql = "DELETE FROM vaults WHERE id = @vaultId;";

		_db.Execute(sql, new { vaultId });
	}
}