<template>
  <div class="component pt-3 brick">
    <div class="card bg-dark text-white selectable user-select-none" data-bs-toggle="modal" :data-bs-target="'#viewKeep-' + keep.id" :title="keep.name">
      <img :src="keep.img" class="card-img">
      <div class="card-img-overlay d-flex justify-content-between">
        <h5 class="card-title">{{keep.name}}</h5>
        <img class="cardCreatorImg rounded-circle" @click="goToProfile(keep.creator.id)" :src="keep.creator.picture" :title="keep.creator.name">
      </div>
    </div>
  </div>

  <!-- NOTE modal body below -->
  <Modal :id="'viewKeep-' + keep.id">
    <template #modal-title>
      <div></div>
    </template>
    <template #modal-body>
      <div class="d-flex">
        <img
          class="card-img img-fluid w-50"
          :src="keep.img"
          :title="keep.name"
        />
        <div class="ps-3 justify-content-center">
          <h3>{{ keep.name }}</h3>
          <p>
            <i class="mdi mdi-eye" title="Total Views" /> {{keep.views}} | 
            <!-- <i class="mdi mdi-share-variant" title="Total Shares" /> {{keep.shares}} | -->
            <i class="mdi mdi-pin" title="Total Keeps" /> {{keep.keeps}} 
          </p>
          <div>
            <p>{{ keep.description }}</p>
          </div>
          <div class="d-flex selectable" @click="goToProfile(keep.creator.id)">
            <img
                :src="keep.creator.picture"
                class="profilePic rounded-circle"
                alt="profile picture"
                :title="keep.creator.name"
              />
            <p>{{ keep.creator.name }}</p>
          </div>
            <div v-if="keep.creatorId == account.id">
              <button class="btn btn-danger mdi mdi-delete" @click="deleteKeep()" />
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
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
export default {
  props: {
    keep: {
      type: Object,
      required: true,
    }
  },
  setup(props){
    const router = useRouter()
    return {
      account: computed(() => AppState.account),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById('viewKeep-' + props.keep.id)).hide()
            await keepsService.deleteKeep(props.keep.id)
          }
        } catch (error) {
          Pop.toast(error.message, "error")
          logger.log(error)
        }
      },
      goToProfile(creatorId) {
        Modal.getOrCreateInstance(document.getElementById('viewKeep-' + props.keep.id)).hide()
        router.push({ name: 'Profile', params: { id: creatorId }})
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.cardCreatorImg{
  height: 40px;
  width: 40px;
}
.brick {
  display: inline-block;
  width: 100%;
}
.profilePic {
  height: 5vh;
  max-width: 10vw;
}
</style>