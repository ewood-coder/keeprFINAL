namespace keeprFINAL.Models;

public class Profile : RepoItem<string>
{
	// NOTE: RepoItem fills in the Id
	// public string Id { get; set; }

	public string Name { get; set; }
	public string Picture { get; set; }
	public string CoverImg { get; set; }

}
