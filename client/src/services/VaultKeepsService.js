import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
  
  async getVaultKeeps(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log(`GOT VAULT'S KEEPS 🖼️🖼️➡️📦`, response.data)
    AppState.vaults = response.data.map(pojo => new Vault(pojo))
    AppState.vaults.reverse()
  }

  async createVaultKeep(vaultData){
   const response = await api.post('api/vaultkeeps', vaultData)
   logger.log("CREATED VAULT 📦📦📦", response.data)
   const newVault = new Vault(response.data)
   AppState.vaults.push(newVault)
  }

  async destroyVaultKeep(vaultId){
    await api.delete(`api/vaults/${vaultId}`)
    AppState.activeVault = null
    AppState.vaults = AppState.vaults.filter(r => r.id != vaultId)
    AppState.profileVaults = AppState.profileVaults.filter(r => r.id != vaultId)
  }
}

export const vaultsService = new VaultsService()