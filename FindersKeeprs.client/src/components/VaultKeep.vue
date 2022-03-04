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
</template>


<script>
export default {
  props: {
    vaultKeep: {
      type: Object,
      required: true,
    }
  },
  setup(){
    return {}
  }
}
</script>


<style lang="scss" scoped>
.brick {
  display: inline-block;
  width: 100%;
}
</style>