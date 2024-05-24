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



	// STUB: GET KEEPS IN PUBLIC ✅ VAULT



	// STUB: GET KEEPS IN PRIVATE ❌ VAULT



	// STUB: CREATE VAULTKEEP
}