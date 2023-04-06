import { logger } from "../utils/Logger.js"

class VaultKeepsService {

  async createVaultKeep(vaultKeepData) {
    const res = await api.post('api/vaultkeeps/', vaultKeepData)
    logger.log('Putting vault in keep', res.data)

  }

}

export const vaultKeepsService = new VaultKeepsService()