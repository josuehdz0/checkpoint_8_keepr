import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"

class VaultsService{

  async setActiveVault(vault){
    AppState.vault = vault
    logger.log(vault, "this vault")
  }

}
export const vaultsService = new VaultsService()