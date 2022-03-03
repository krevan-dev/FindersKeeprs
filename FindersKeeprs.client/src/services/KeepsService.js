import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    // logger.log(res.data)
    AppState.keeps = res.data
  }

  async createKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    AppState.keeps.unshift(res.data)
    Pop.toast("Keep successfully created!", 'success')
  }

  async deleteKeep(keepId) {
    await api.delete('api/keeps/' + keepId)
    AppState.keeps = AppState.keeps.filter(k => k.id !== keepId)
    Pop.toast("Keep successfully removed!", 'success')
  }
}

export const keepsService = new KeepsService()