namespace keeprFINAL.Services;

public class ProfilesService
{
	private readonly ProfilesRepository _profilesRepository;

	public ProfilesService(ProfilesRepository profilesRepository)
	{
		_profilesRepository = profilesRepository;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: GET USERS PROFILE BY ID
	internal Profile GetProfileById(string profileId)
	{
		Profile profile = _profilesRepository.GetProfileById(profileId);

		if (profile == null)
		{
			throw new Exception($"Invalid id: {profileId}");
		}

		return profile;
	}



	// STUB: GET USERS KEEPS BY ID
	internal List<Keep> GetKeepsByUserId(string profileId)
	{
		List<Keep> userKeeps = _profilesRepository.GetKeepsByUserId(profileId);
		return userKeeps;
	}



	// STUB: GET USERS VAULTS BY ID
	internal List<Vault> GetVaultsByUserId(string profileId)
	{
		List<Vault> userVaults = _profilesRepository.GetVaultsByUserId(profileId);
		return userVaults;
	}
}
