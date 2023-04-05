import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService{

  async setActiveVault(vault){
    AppState.vault = vault
    logger.log(vault, "this vault")
  }

  async createVault(formData){
    const res = await api.post('api/vaults', formData)
    logger.log("Creating Vault", res.data)
    AppState.vaults.push(new Vault(res.data))
    return res.data
  }

}
export const vaultsService = new VaultsService()