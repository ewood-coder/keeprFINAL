namespace keeprFINAL.Models;

public class Vault : RepoItem<int>
{
	// NOTE: RepoItem fills in the Id
	// public string Id { get; set; }

	public string CreatorId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string Img { get; set; }
	public bool IsPrivate { get; set; }


	public Profile Creator { get; set; }

}
