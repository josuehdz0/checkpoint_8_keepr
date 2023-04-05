import { AppState } from "../AppState.js"
import { Profile } from "../models/Account.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfilesService{


  async getProfileById(profileId){
    const res = await api.get('api/profiles/'+ profileId)
    logger.log ('getting profile', res.data)
    AppState.profile = new Profile(res.data)
  }

  clearProfile(){
    AppState.profile = null
  }

  async getKeepsByCreatorId(creatorId){
    const res = await api.get(`api/profiles/${creatorId}/keeps`)
    logger.log("Keeps by creator id", res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

}

export const profilesService = new ProfilesService()