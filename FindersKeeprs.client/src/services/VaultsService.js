import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"


class VaultsService {
  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    // logger.log(res.data)
    AppState.profileVaults = [...AppState.profileVaults, res.data]
    AppState.accountVaults = [...AppState.accountVaults, res.data]
    Pop.toast("Vault successfully created!", "success")
  }

  async getVaultById(vaultId) {
    const res = await api.get('api/vaults/' + vaultId)
    // logger.log(res.data)
    AppState.activeVault = res.data
  }

  async deleteVault(vaultId) {
    const res = await api.delete('api/vaults/' + vaultId)
    // logger.log(res.data)
    AppState.activeVault = {}
    Pop.toast("Vault successfully deleted!", "success")
  }

  async getVaultKeeps(vaultId) {
    const res = await api.get('api/vaults/' + vaultId + '/keeps')
    logger.log(res.data)
    AppState.vaultKeeps = res.data
  }
}

export const vaultsService = new VaultsService()