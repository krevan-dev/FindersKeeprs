<template>
  <div class="component pt-3 brick">
    <div class="card bg-dark text-white selectable user-select-none" data-bs-toggle="modal" :data-bs-target="'#viewKeep-' + vaultKeep.id" :title="vaultKeep.name">
      <img :src="vaultKeep.img" class="card-img">
      <div class="card-img-overlay d-flex justify-content-between">
        <h5 class="card-title">{{vaultKeep.name}}</h5>
        <img class="cardCreatorImg rounded-circle" @click="goToProfile(vaultKeep.creator.id)" src="" title="">
      </div>
    </div>
  </div>

  <Modal :id="'viewKeep-' + vaultKeep.id">
    <template #modal-title>
      <div></div>
    </template>
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-6">
          <img
            class="card-img img-fluid"
            :src="vaultKeep.img"
            :title="vaultKeep.name"
          />
          </div>
          <div class="col-6 ps-3">
            <div class="row">
              <div class="d-flex justify-content-between align-items-center">
                <h3>{{ vaultKeep.name }}</h3>
                <h6>
                  <i class="mdi mdi-eye" title="Total Views" /> {{vaultKeep.views}} | 
                  <!-- <i class="mdi mdi-share-variant" title="Total Shares" /> {{vaultKeep.shares}} | -->
                  <i class="mdi mdi-pin" title="Total Keeps" /> {{vaultKeep.keeps}} 
                </h6>
                <button v-if="vaultKeep.creatorId == account.id" class="btn btn-danger btn-sm mdi mdi-delete" @click="deleteKeep()"/>
              </div>
            </div>
            <div class="row">
              <div class="py-5 mb-5">
                <p>{{ vaultKeep.description }} {{vaultKeep.vaultKeepId}}</p>
              </div>
            </div>
            <div class="row">
              <div class="d-flex selectable mt-5" @click="goToProfile(vaultKeep.creator?.id)">
                <img
                    :src="vaultKeep.creator?.picture"
                    class="profilePic rounded"
                    alt="profile picture"
                    :title="vaultKeep.creator?.name"
                  />
                <h4 class="ps-2">{{ vaultKeep.creator?.name }}</h4>
              </div>
            </div>
            <div class="row">
              <div class="btn-group pt-2">
                <button v-if="vaultKeep.creatorId == account.id" type="button" class="btn btn-secondary btn-sm" @click="removeVaultKeep()">Remove keep from vault</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import { vkService } from '../services/VaultKeepsService'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
export default {
  props: {
    vaultKeep: {
      type: Object,
      required: true,
    }
  },
  setup(props){
    return {
      account: computed(() => AppState.account),
      accountVaults: computed(() => AppState.accountVaults),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById('viewKeep-' + props.vaultKeep.id)).hide()
            await keepsService.deleteKeep(props.vaultKeep.id)
          }
        } catch (error) {
          Pop.toast(error.message, "error")
          logger.log(error)
        }
      },
      async removeVaultKeep() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById('viewKeep-' + props.vaultKeep.id)).hide()
            await vkService.removeVaultKeep(props.vaultKeep.vaultKeepId)
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
.brick {
  display: inline-block;
  width: 100%;
}
.profilePic {
  height: 4vh;
  max-width: 10vw;
}
</style>