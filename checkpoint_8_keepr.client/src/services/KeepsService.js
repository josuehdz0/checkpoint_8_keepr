import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"

class KeepsService{

  async getAllKeeps(){
    const res = await api.get('api/keeps')
    logger.log(res.data, "here are all the keeps")
    const keeps = res.data.map(a => new Keep(a))
    AppState.keeps = keeps
  }
  setActiveKeep(keep){
    AppState.keep = keep
    logger.log(keep, "this keep")
  }

  async createKeep(formData){
    const res = await api.post('api/keeps', formData)
    logger.log("Creating Keep", res.data)
    AppState.keeps.push(new Keep(res.data))
    return res.data
  }


}

export const keepsService = new KeepsService()