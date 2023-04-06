import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"

class KeepsService {

  async getAllKeeps() {
    const res = await api.get('api/keeps')
    logger.log(res.data, "here are all the keeps")
    const keeps = res.data.map(a => new Keep(a))
    AppState.keeps = keeps
  }
  async setActiveKeep(keep) {
    const keepId = keep.id
    const res = await api.get(`api/keeps/${keepId}`)
    logger.log(keepId)
    AppState.keep = keep
    AppState.keep.views++
    logger.log(keep, "this keep")
  }

  async createKeep(formData) {
    const res = await api.post('api/keeps', formData)
    logger.log("Creating Keep", res.data)
    AppState.keeps.push(new Keep(res.data))
    return res.data
  }

  async deleteKeep(keepId) {
    const res = await api.delete('api/keeps/' + keepId)
    logger.log("deleting keep", res.data)
    const keepIndex = AppState.keeps.findIndex(k => k.id == keepId)

    if (keepIndex !== -1) {
      AppState.keeps.splice(keepIndex, 1)
    }

  }

  clearKeeps() {
    AppState.keeps = []
  }


}

export const keepsService = new KeepsService()