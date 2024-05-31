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


  // STUB: CREATE VAULTKEEP
//   async addKeepToVault(vaultKeepData){
//    const response = await api.post('api/vaultkeeps', vaultKeepData)
//    logger.log("CREATED VAULTKEEP 🖼️🖼️➡️📦", response.data)
//    const newVaultKeep = new VaultKeep(response.data)
//    AppState.vaultKeeps.push(newVaultKeep)
//   }

 async addKeepToVault(keepId, vaultId){
    const keepToVaultData = { keepId: parseInt(keepId), vaultId: vaultId }
    const response = await api.post('api/vaultkeeps', keepToVaultData)
    logger.log("CREATED VAULTKEEP 🖼️🖼️➡️📦", response.data)

  //  //  AppState.vaultKeeps.push(new VaultKeep(response.data))

  //   const newVaultKeep = new Keep(response.data)
  //   AppState.vaultKeeps.push(newVaultKeep)
   }


  // STUB: DELETE VAULTKEEP
  async removeKeepFromVault(vaultKeepId){
    await api.delete(`api/vaultkeeps/${vaultKeepId}`)
	
    // NOTE: FOR FUTURE ME: This line filters out the deleted item(s) in each array for you after it performs the above function, so you don't have to refresh the page manually to see that its been deleted.
    AppState.vaultKeeps = AppState.vaultKeeps.filter(keep => keep.id != AppState.activeKeep.id)
    AppState.activeKeep.vaultKeepId =null
  }
}

export const vaultKeepsService = new VaultKeepsService()