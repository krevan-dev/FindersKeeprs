import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class ProfilesService {
  async getProfileById(userId) {
    const res = await api.get('api/profiles/' + userId)
    // logger.log("Profile", res.data)
    AppState.activeProfile = res.data
  }

  async getKeepsByProfileId(userId) {
    const res = await api.get('api/profiles/' + userId + '/keeps')
    // logger.log("keeps", res.data)
    AppState.profileKeeps = res.data
  }

  async getVaultsByProfileId(userId) {
    const res = await api.get('api/profiles/' + userId + '/vaults')
    // logger.log("vaults", res.data)
    AppState.profileVaults = res.data
  }
}


export const profilesService = new ProfilesService()