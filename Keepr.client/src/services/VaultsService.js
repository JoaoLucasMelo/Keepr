import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
async getVault(id){
  const res = await api.get("api/vaults/" + id)
  AppState.activeVault = res.data
}

async createVault(newVault){
  const res = await api.post("api/vaults", newVault)
  logger.log('NEW VAULT',res.data)
  AppState.activeProfVaults.push(res.data)
}

async removeVault(id){
  const res = await api.delete("api/vaults/" + id)
  logger.log(res.data)
  let newVaults = AppState.activeProfVaults?.filter( v => v.id !== id)
  AppState.activeProfVaults = newVaults
}
}
export const vaultsService = new VaultsService()