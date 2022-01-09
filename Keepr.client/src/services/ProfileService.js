import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { accountService } from "./AccountService"
import { api } from "./AxiosService"

class ProfileService{
async getProfile(id){
  const res = await api.get("api/profiles/" + id)
  AppState.activeProfile = res.data
  
}
async getVaults(id){
  if(id === AppState.account.id){
    accountService.getVaults()
  }
  else {
  const res = await api.get("api/profiles/" + id + "/vaults")
  AppState.activeProfVaults = res.data
  }
}
async getKeeps(id){
  const res = await api.get("api/profiles/" + id + "/keeps")
  AppState.activeProfKeeps = res.data
}


}
export const profileService = new ProfileService()