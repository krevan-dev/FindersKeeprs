<template>
  <div class="component">
    {{vault.name}}
    <button v-if="vault.creatorId == account.id" class="btn btn-danger" @click="deleteVault()">Delete</button>
    {{vault.creatorId}}
    <div class="row">
      <div class="masonry">
        <VaultKeep v-for="vk in vaultKeeps" :key="vk.id" :vaultKeep="vk"/>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { router } from '../router'
export default {
  setup(){
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await vaultsService.getVaultKeeps(route.params.id)
      } catch (error) {
        logger.log(error)
        router.push({ name: 'Home'})
      }
    })
    return {
      route,
      router,
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(route.params.id)
            router.push({ name: 'Home'})
          }
        } catch (error) {
          Pop.toast(error.message, "error")
          logger.log(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry {
  columns: 8 20vw;
  column-gap: 1rem;
  .brick {
    display: inline-block;
    width: 100%;
  }
}
</style>