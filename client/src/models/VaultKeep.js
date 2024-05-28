import { DatabaseItem } from "./DatabaseItem.js";

export class VaultKeep extends DatabaseItem {
  constructor(data) {
    super(data) //calls constructor on DataBaseItem

    // id, createdAt, updatedAt inherited from DatabaseItem

		this.creatorId = data.creatorId
		this.keepId = data.keepId
		this.vaultId = data.vaultId
  }
}
