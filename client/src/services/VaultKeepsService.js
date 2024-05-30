import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  
  async getVaultKeeps(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log(`GOT VAULT'S KEEPS 📦➡️🖼️`, response.data)
    AppState.vaultKeeps = response.data.map(pojo => new Keep(pojo))
    AppState.vaultKeeps.reverse()
  }

//   async createVaultKeep(vaultData){
//    const response = await api.post('api/vaultKeeps', vaultData)
//    logger.log("CREATED VAULTKEEP 🖼️🖼️➡️📦", response.data)
//    const newVaultKeep = new VaultKeep(response.data)
//    AppState.vaultKeeps.push(newVaultKeep)
//   }

  async destroyVaultKeep(vaultKeepId){
    await api.delete(`api/vaultKeeps/${vaultKeepId}`)
    AppState.activeVaultKeep = null
    AppState.vaultKeeps = AppState.vaultKeeps.filter(r => r.id != vaultKeepId)
   //  AppState.profileVaults = AppState.profileVaults.filter(r => r.id != vaultKeepId)
  }
}

export const vaultKeepsService = new VaultKeepsService()