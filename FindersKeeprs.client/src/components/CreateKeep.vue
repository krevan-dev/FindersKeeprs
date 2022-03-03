<template>
  <div class="component">
    <h2 class="pb-2">Create New Keep</h2>
    <form @submit.prevent="createKeep()">
      <div class="form-floating mb-2">
        <input type="text" class="form-control" id="keepTitle" placeholder="Add a title to your keep" v-model="newKeep.name">
        <label for="keepTitle">Title</label>
      </div>
      <div class="form-floating mb-2">
        <input type="text" class="form-control" id="keepImgUrl" placeholder="Add an image source to your keep" v-model="newKeep.img">
        <label for="keepImgUrl">Image URL</label>
      </div>
      <div class="form-floating">
        <textarea class="form-control" id="keepDescription" placeholder="Add a description of your keep here" style="height: 125px" v-model="newKeep.description"></textarea>
        <label for="keepDescription">Description</label>
      </div>
      <div class="d-flex justify-content-between mt-4">
        <button type="button" class="btn btn-outline-danger rounded-pill" data-bs-dismiss="modal">Cancel</button>
        <button
        v-if="loading == true"
        class="btn btn-warning rounded-pill"
        type="button"
        disabled
      >
          <span>Posting...</span>
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
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { ref } from '@vue/reactivity'
export default {
  setup(){
    const newKeep = ref({})
    const loading = ref(false)
    return {
      newKeep,
      loading,
      async createKeep() {
        try {
          loading.value = true
          await keepsService.createKeep(newKeep.value)
          Modal.getOrCreateInstance(document.getElementById('createKeep')).hide();
          newKeep.value = {}
          loading.value = false
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