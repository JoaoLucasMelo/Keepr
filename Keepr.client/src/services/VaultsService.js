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
}
}
export const vaultsService = new VaultsService()