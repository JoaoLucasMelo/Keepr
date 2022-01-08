import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
async getAll(){
  const res = await api.get("api/keeps")
  AppState.keeps = res.data
}
async getByVaultKeeps(vaultId){
  const res = await api.get("api/vaults/" + vaultId + "/keeps")
  AppState.activeVaultKeeps = res.data
}
async getKeepById(id){
  const res = await api.get("api/keeps/" + id)
  AppState.activeKeep = res.data
  logger.log('GET BY ID', res.data)
}
}
export const keepsService = new KeepsService()