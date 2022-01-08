import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService{
async getVault(id){
  const res = await api.get("api/vaults/" + id)
  AppState.activeVault = res.data
}
}
export const vaultsService = new VaultsService()