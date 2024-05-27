namespace keeprFINAL.Models;

public class VaultKeep : RepoItem<int>
{
	// NOTE: RepoItem fills in the Id
	// public string Id { get; set; }

	public string CreatorId { get; set; }
	public int KeepId { get; set; }
	public int VaultId { get; set; }
}
