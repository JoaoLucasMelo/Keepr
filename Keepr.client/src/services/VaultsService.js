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
  AppState.activeProfVaults.push(res.data)
  AppState.myVaults.push(res.data)
}

async removeVault(id){
  const res = await api.delete("api/vaults/" + id)
  let newVaults = AppState.activeProfVaults?.filter( v => v.id !== id)
  AppState.activeProfVaults = newVaults
}

async editVault(id, input){
  const res = await api.put("api/vaults/" + id, input)
  AppState.activeVault = res.data
}
}
export const vaultsService = new VaultsService()