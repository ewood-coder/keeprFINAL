import { DatabaseItem } from "./DatabaseItem.js";
import { Profile } from "./Profile.js";

export class Keep extends DatabaseItem {
  constructor(data) {
    super(data) //calls constructor on DataBaseItem

    // id, createdAt, updatedAt inherited from DatabaseItem

    this.creatorId = data.creatorId
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.views = data.views

	this.kept = data.kept
    this.creator = new Profile(data.creator)
  }
}
