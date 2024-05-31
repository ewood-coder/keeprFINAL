import { AppState } from "../AppState.js"
import { Profile } from "../models/Profile.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class ProfilesService {

	// async searchProfiles(query) {
	// 	const response = await api.get(`api/profiles?query=${query}&page=${AppState.currentPage}`)
	// 	logger.log('SEARCHING FOR PROFILES', query)
	// 	AppState.profiles = response.data.map(p => new Profile(p))
	// 	console.log(response.data)
	// }

	async setActiveProfile(profile) {
		AppState.activeProfile = profile
		this.setActiveProfile(profile.id)
	}

	async getProfile(profileId) {
		AppState.activeProfile = null
		const response = await api.get(`api/profiles/${profileId}`)
		console.log('got profiles! 🧑‍🎨', response.data)
		AppState.activeProfile = new Profile(response.data)
	}
}


export const profilesService = new ProfilesService()
