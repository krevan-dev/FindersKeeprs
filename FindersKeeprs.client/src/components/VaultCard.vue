<template>
  <div class="col-md-2 p-2">
    <div class="card selectable" @click="goToVaultPage(vault.id)" style="width: 18rem;">
      <div class="card-body">
        <h5 class="card-title">{{vault.name}}</h5>
        <p class="card-text">{{vault.description}}</p>
        <div>
          <i v-if="vault.isPrivate == true" class="mdi mdi-lock"></i>
          <i v-else class="mdi mdi-lock-open"></i>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { router } from '../router'
export default {
  props: {
    vault: {
      type: Object,
      required: true,
    }
  },
  setup(){
    return {
      goToVaultPage(vaultId) {
        router.push({ name: 'Vault', params: {id: vaultId}})
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>