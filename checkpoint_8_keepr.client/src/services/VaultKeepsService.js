import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {

  async createVaultKeep(vaultKeepData) {
    const res = await api.post('api/vaultkeeps/', vaultKeepData)
    // logger.log('Putting vault in keep', res.data)
    AppState.vaultKeeps.push(new VaultKeep(res.data))

  }

  async deleteVaultKeep(vaultKeepId) {
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    // logger.log('Deleteing VaultKeep', res.data)

    const vaultKeepIndex = AppState.keeps.findIndex(k => k.vaultKeepId == vaultKeepId)

    if (vaultKeepIndex !== -1) {
      AppState.keeps.splice(vaultKeepIndex, 1)
    }

  }

}

export const vaultKeepsService = new VaultKeepsService()