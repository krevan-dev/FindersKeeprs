import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultsService {
  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    logger.log(res.data)
    // AppState.profileVaults = [...AppState.profileVaults, res.data]
  }

  async getProfileVaults() {
    const res = await api.get('')
  }

  async getAccountVaults() {
    const res = await api.get('account/vaults')
    logger.log(res.data)
  }
}

export const vaultsService = new VaultsService()