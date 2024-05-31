import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
	async setActiveKeep(keep) {
		AppState.activeKeep = keep
		await this.getKeepById(keep.id)
  
    AppState.activeKeep.vaultKeepId = keep.vaultKeepId
	}
  
  async getKeepById(keepId) {
    AppState.activeKeep = null
    const response = await api.get(`api/keeps/${keepId}`)
    logger.log('GOT KEEP BY ID 🖼️', response.data)
    AppState.activeKeep = new Keep(response.data)
  }
  async getKeeps() {
    const response = await api.get('api/keeps')
    logger.log('GOT KEEPS 🖼️🖼️🖼️', response.data)
    AppState.keeps = response.data.map(pojo => new Keep(pojo))
    AppState.keeps.reverse()
  }

  async getProfileKeeps(profileId) {
	AppState.profileKeeps = []
	const response = await api.get(`api/profiles/${profileId}/keeps?`)
	console.log('GOT PROFILE KEEPS 🖼️✅', response.data)
	const keeps = response.data.map(keep => new Keep(keep))

	AppState.profileKeeps = keeps
}

  async createKeep(keepData){
    const response = await api.post('api/keeps', keepData)
    logger.log("CREATED KEEP 🖼️🖼️🖼️", response.data)
    AppState.keeps = [new Keep(response.data) , ...AppState.keeps ]
  }

  async  updateKeep(keepId, keepData){
    const response = await api.put(`api/keeps/${keepId}`, keepData)
    logger.log("UPDATED KEEP 🖼️✅", response.data)
    AppState.activeKeep = new Keep(response.data)
  } 

  async destroyKeep(keepId){
    await api.delete(`api/keeps/${keepId}`)
    AppState.activeKeep = null

    // NOTE: FOR FUTURE ME: This line filters out the deleted item(s) in each array for you after it performs the above function, so you don't have to refresh the page manually to see that its been deleted.
    AppState.keeps = AppState.keeps.filter(keep => keep.id != keepId)
    AppState.profileKeeps = AppState.profileKeeps.filter(keep => keep.id != keepId)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(keep => keep.id != keepId)
  }
}

export const keepsService = new KeepsService()