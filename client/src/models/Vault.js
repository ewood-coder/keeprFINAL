import { DatabaseItem } from "./DatabaseItem.js";
import { Profile } from "./Profile.js";

export class Vault extends DatabaseItem {
   constructor(data) {
      super(data) //calls constructor on DataBaseItem

    // id, createdAt, updatedAt inherited from DatabaseItem

		this.creatorId = data.creatorId
		this.name = data.name
		this.description = data.description
		this.img = data.img
		this.isPrivate = data.isPrivate

		
		// NOTE: one will just be an object, and one will be a classed object, but they could end up working the exact same, depending on how your backend is set up. The first one is the preferred method though, cause it will type the creator property, which gives you intellisense on that property in vscode. 
		this.creator = new Profile(data.creator)
		// this.creator = data.creator
  }
}
