<template>
  <div class="component container-fluid">
    <div class="row">
      <div class="col d-flex mt-5">
        <img :src="profile.picture" height="150" />
        <div>
          <h2 class="m-0">{{ profile.name }}</h2>
          <p class="m-0">{{ profile.email }}</p>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'


export default {
  setup() {
    const route = useRoute()
    onMounted(() => {
      profilesService.getById(route.params.id)
    })
    return {
      profile: computed(() => AppState.activeProfile),
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