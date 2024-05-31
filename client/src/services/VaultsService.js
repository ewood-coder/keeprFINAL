import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
	// async setActiveVault(vault) {
	// 	AppState.activeVault = vault
	// 	// await this.getVaultKeeps(keep.id)
	// }
  
  async getVaultById(vaultId) {
    AppState.activeVault = null
    const response = await api.get(`api/vaults/${vaultId}`)
    logger.log('GOT VAULT BY ID 📦', response.data)
    AppState.activeVault = new Vault(response.data)
  }
  async getVaults() {
    const response = await api.get('api/vaults')
    logger.log('GOT VAULTS 📦📦📦', response.data)
    AppState.vaults = response.data.map(pojo => new Vault(pojo))
    AppState.vaults.reverse()
  }

  async getProfileVaults(profileId) {
	AppState.profileVaults = []
	const response = await api.get(`api/profiles/${profileId}/vaults?`)
	console.log('GOT PROFILE VAULTS 📦✅', response.data)
	const vaults = response.data.map(vault => new Vault(vault))

	AppState.profileVaults = vaults
  }

  async createVault(vaultData){
   const response = await api.post('api/vaults', vaultData)
   logger.log("CREATED VAULT 📦📦📦", response.data)
   const newVault = new Vault(response.data)
   AppState.vaults.push(newVault)
  }

  async  updateVault(vaultId, vaultData){
   const response = await api.put(`api/vaults/${vaultId}`, vaultData)
   logger.log("UPDATED VAULT 📦✅", response.data)
   AppState.activeVault = new Vault(response.data)
  } 

  async destroyVault(vaultId){
    await api.delete(`api/vaults/${vaultId}`)
    AppState.activeVault = null

    // NOTE: FOR FUTURE ME: This line filters out the deleted item(s) in each array for you after it performs the above function, so you don't have to refresh the page manually to see that its been deleted.
    AppState.vaults = AppState.vaults.filter(vault => vault.id != vaultId)
    AppState.profileVaults = AppState.profileVaults.filter(vault => vault.id != vaultId)
  }
}

export const vaultsService = new VaultsService()