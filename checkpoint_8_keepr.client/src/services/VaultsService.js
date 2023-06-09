import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {

  async setActiveVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    // logger.log(res.data, "this vault")
    AppState.vault = new Vault(res.data)
  }

  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    // logger.log("Creating Vault", res.data)
    AppState.vaults.push(new Vault(res.data))
    return res.data
  }

  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    // logger.log("deleteing vault", res.data)
    const vaultIndex = AppState.vaults.findIndex(v => v.id == vaultId)

    if (vaultIndex !== -1) {
      AppState.vaults.splice(vaultIndex, -1)
    }
  }

  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    // logger.log("Keeps by Vault Id", res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  clearVaults() {
    AppState.vaults = []
  }

}
export const vaultsService = new VaultsService()