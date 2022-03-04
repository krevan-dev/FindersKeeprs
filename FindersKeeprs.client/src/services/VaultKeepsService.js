import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultKeepsService {
  async createVaultKeep(newVaultKeep) {
    const res = await api.post('api/vaultkeeps', newVaultKeep)
    AppState.vaultKeeps = [...AppState.vaultKeeps, res.data]
    Pop.toast("Successfully added keep to vault.", 'success')
  }

  async removeVaultKeep(vaultKeepId) {
    await api.delete('api/vaultkeeps/' + vaultKeepId)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.id !== vaultKeepId)
    Pop.toast("Successfully removed keep from vault.", 'success')
  }
}

export const vkService = new VaultKeepsService()