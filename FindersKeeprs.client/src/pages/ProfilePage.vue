<template>
  <div class="component container-fluid">
    <div class="row">
      <div class="col d-flex mt-3">
        <img :src="profile.picture" class="rounded" height="150" />
        <div class="p-2">
          <h2>{{ profile.name }}</h2>
          <h5>Total Vaults: {{profileVaults.length}}</h5>
          <h5>Total Keeps: {{profileKeeps.length}}</h5>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <h4>Vaults:</h4>
      <VaultCard v-for="v in profileVaults" :key="v.id" :vault="v"/>
    </div>
    <div class="row mt-3">
        <h4>Keeps:</h4>
      <div class="masonry">
        <Keep v-for="k in profileKeeps" :key="k.id" :keep="k"/>
      </div>
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