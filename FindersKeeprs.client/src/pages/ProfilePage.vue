<template>
  <div class="component container-fluid">
    <div class="row">
      <div class="col d-flex mt-3">
        <img :src="profile.picture" class="rounded" height="150" />
        <div class="p-2">
          <h2>{{ profile.name }}</h2>
          <h5>Total Vaults:</h5>
          <h5>Total Keeps:</h5>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <VaultCard v-for="v in profileVaults" :key="v.id" :vault="v"/>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'


export default {
  setup() {
    const route = useRoute()
    onMounted(() => {
      profilesService.getProfileById(route.params.id)
    })
    onMounted(async () => {
      try {
        await profilesService.getKeepsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error.message, "error")
        logger.log(error)
      }
    })
    onMounted(async () => {
      try {
        await profilesService.getVaultsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error.message, "error")
        logger.log(error)
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults)
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