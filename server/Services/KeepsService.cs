namespace keeprFINAL.Services;

public class KeepsService
{
	private readonly KeepsRepository _keepsRepository;

	public KeepsService(KeepsRepository keepsRepository)
	{
		_keepsRepository = keepsRepository;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: CREATE KEEP
	internal Keep CreateKeep(Keep keepData)
	{
		Keep keep = _keepsRepository.CreateKeep(keepData);
		return keep;
	}



	// STUB: GET ALL KEEPS
	internal List<Keep> GetAllKeeps(string userId)
	{
		List<Keep> keeps = _keepsRepository.GetAllKeeps();
		return keeps;
	}



	// STUB: GET KEEP BY ID ----------------

	// NOTE overload: if you call GetKeepById and only supply an integer, this method runs
	// this is a private method, we are dealing with sensitive information, so this only callable by members of the service
	// internal Keep GetKeepById(int keepId)
	// {

	// 	if (keep == null)
	// 	{
	// 		throw new Exception($"Invalid id: {keepId}");
	// 	}

	// 	return keep;
	// }

	// NOTE overload: if you call GetKeepById and supply an integer and a string, this method runs
	internal Keep GetKeepById(int keepId)
	{
		Keep keep = _keepsRepository.GetKeepById(keepId);

		return keep;
	}



	// STUB: INCREMENT VIEWS ON KEEP FOR EACH VISIT
	internal void IncrementViews(int keepId)
	{
		_keepsRepository.IncrementViews(keepId);

	}



	// STUB: UPDATE KEEP
	internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
	{
		Keep keepToUpdate = GetKeepById(keepId);

		if (keepToUpdate.CreatorId != userId)
		{
			throw new Exception("NOT YOUR KEEP! ❌");
		}

		keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
		keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;
		keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;


		Keep updatedKeep = _keepsRepository.UpdateKeep(keepToUpdate);

		return updatedKeep;
	}



	// STUB: DELETE KEEP
	internal string DestroyKeep(int keepId, string userId)
	{
		Keep keepToDestroy = GetKeepById(keepId);

		if (keepToDestroy.CreatorId != userId)
		{
			throw new Exception("YOU CAN'T DELETE OTHER PEOPLE'S KEEPS BRO");
		}

		_keepsRepository.DestroyKeep(keepId);

		return $"The keep with the ID: '{keepToDestroy.Id}' has been deleted!";
	}
}
