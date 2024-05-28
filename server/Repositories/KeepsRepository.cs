// using keeprFINAL.Interfaces;
using System.Runtime.CompilerServices;


namespace keeprFINAL.Repositories;

// NOTE: ONLY USE THIS LINE IF YOU PLAN ON USING LINE 1
// public class RestaurantsRepository : IRepository<Restaurant>
public class KeepsRepository
{
	private readonly IDbConnection _db;

	public KeepsRepository(IDbConnection db)
	{
		_db = db;
	}


	// SECTION: FUNCTIONS ---------------------------------------------

	// STUB: POPULATE CREATOR
	private Keep PopulateCreator(Keep keep, Profile creator)
	{
		keep.Creator = creator;
		return keep;
	}



	// STUB: CREATE KEEP
	internal Keep CreateKeep(Keep keepData)
	{
		string sql = @"
			INSERT INTO
			keeps(creatorId, name, description, img, views)
			VALUES(@CreatorId, @Name, @Description, @Img, @Views);
			
			SELECT * FROM keeps WHERE id = LAST_INSERT_ID();";

		Keep keep = _db.Query<Keep>(sql, keepData).FirstOrDefault();


		return GetKeepById(keep.Id);
	}



	// STUB: GET ALL KEEPS
	internal List<Keep> GetAllKeeps()
	{
		string sql = @"
			SELECT
			keeps.*,
			accounts.*
			FROM keeps
			JOIN accounts ON accounts.id = keeps.creatorId
			ORDER BY keeps.createdAt;";

		List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator).ToList();

		return keeps;
	}



	// STUB: GET KEEP BY ID
	internal Keep GetKeepById(int keepId)
	{
		string sql = @"
			SELECT
			keeps.*,
			accounts.*
			FROM keeps
			JOIN accounts ON accounts.id = keeps.creatorId
			WHERE keeps.id = @keepId;";

		Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, new { keepId }).FirstOrDefault();

		return keep;
	}



	// STUB: INCREMENT VIEWS ON KEEP FOR EACH VISIT
	internal void IncrementViews(int keepId)
	{
		string sql = @"
			UPDATE keeps
			SET views = views + 1
			WHERE id = @keepId
			LIMIT 1;";

		_db.Execute(sql, new { keepId });
	}



	// STUB: UPDATE KEEP
	internal Keep UpdateKeep(Keep keepToUpdate)
	{
		string sql = @"
			UPDATE keeps
			SET
			name = @Name,
			description = @Description,
			img = @Img
			WHERE id = @Id;

			SELECT
			keeps.*,
			accounts.*
			FROM keeps
			JOIN accounts ON accounts.id = keeps.creatorId
			WHERE keeps.id = @Id;";


		Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
		{
			keep.Creator = account;
			return keep;
		}, keepToUpdate).FirstOrDefault();

		return keep;
	}



	// STUB: DELETE KEEP
	internal void DestroyKeep(int keepId)
	{
		string sql = "DELETE FROM keeps WHERE id = @keepId;";

		_db.Execute(sql, new { keepId });
	}
}