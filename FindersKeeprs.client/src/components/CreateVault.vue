<template>
  <div class="component">
    <h2 class="pb-2">Create A New Vault</h2>
    <form @submit.prevent="createVault()">
      <div class="form-floating mb-2">
        <input type="text" class="form-control" id="vaultTitle" placeholder="Add a title to your vault" v-model="newVault.name">
        <label for="vaultTitle">Title</label>
      </div>
      <div class="form-floating">
        <textarea class="form-control" id="vaultDescription" placeholder="Add a description of your vault here" style="height: 125px" v-model="newVault.description"></textarea>
        <label for="vaultDescription">Description</label>
      </div>
      <div class="form-check mt-2">
        <label class="form-check-label" for="flexCheckDefault">
          Would you like to mark this vault as private?
        </label>
        <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" v-model="newVault.isPrivate">
      </div>
      <div class="d-flex justify-content-between mt-4">
        <button type="button" class="btn btn-outline-danger rounded-pill" data-bs-dismiss="modal">Cancel</button>
        <button
        v-if="creating == true"
        class="btn btn-warning rounded-pill"
        type="button"
        disabled
      >
          <span>Standby...</span>
          <span
            class="spinner-border spinner-border-sm ms-2"
            role="status"
            aria-hidden="true"
          ></span>
        </button>
        <button v-else type="submit" class="btn btn-success rounded-pill">Create</button>
      </div>
    </form>
  </div>
</template>


<script>
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
import { ref } from '@vue/reactivity'
export default {
  setup(){
    const newVault = ref({})
    const creating = ref(false)
    return {
      newVault,
      creating,
      async createVault() {
        try {
          creating.value = true
          await vaultsService.createVault(newVault.value)
          Modal.getOrCreateInstance(document.getElementById('createVault')).hide();
          newVault.value = {}
          creating.value = false
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

</style>