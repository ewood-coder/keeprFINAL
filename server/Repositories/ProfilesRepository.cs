namespace keeprFINAL.Repositories;

public class ProfilesRepository
{
	private readonly IDbConnection _profileRepository;

	public ProfilesRepository(IDbConnection profileRepository)
	{
		_profileRepository = profileRepository;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: GET USERS PROFILE BY ID
	internal Profile GetProfileById(string profileId)
	{
		string sql = "SELECT * FROM accounts WHERE id = @profileId;";

		Profile profile = _profileRepository.Query<Profile>(sql, new { profileId }).FirstOrDefault();

		return profile;
	}


	// STUB: GET USERS KEEPS BY ID
	internal List<Keep> GetKeepsByUserId(string profileId)
	{
		string sql = @"
			SELECT 
			keeps.*,
			accounts.* 
			FROM keeps 
			JOIN accounts ON accounts.id = keeps.creatorId
			WHERE keeps.creatorId = @profileId;";


		List<Keep> userKeeps = _profileRepository.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
		{
			keep.CreatorId = profile.Id;
			return keep;
		}, new { profileId }).ToList();
		return userKeeps;
	}



	// STUB: GET USERS VAULTS BY ID
	internal List<Vault> GetVaultsByUserId(string profileId, string accountId)
	{
		string sql = @"
			SELECT 
			vaults.*,
			accounts.* 
			FROM vaults 
			JOIN accounts ON accounts.id = vaults.creatorId
			WHERE vaults.creatorId = @profileId AND (vaults.creatorId = @accountId OR vaults.isPrivate = false);";

		List<Vault> userVaults = _profileRepository.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
		{
			vault.CreatorId = profile.Id;
			return vault;
		}, new { profileId, accountId }).ToList();

		return userVaults;
	}
}

