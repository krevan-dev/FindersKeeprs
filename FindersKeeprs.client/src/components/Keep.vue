<template>
  <div class="component pt-3 brick">
    <div class="card bg-dark text-white selectable user-select-none" data-bs-toggle="modal" :data-bs-target="'#viewKeep-' + keep.id" :title="keep.name">
      <img :src="keep.img" class="card-img">
      <div class="card-img-overlay d-flex justify-content-between">
        <h5 class="card-title">{{keep.name}}</h5>
        <img class="cardCreatorImg rounded-circle" :src="keep.creator.picture" :title="keep.creator.name">
      </div>
    </div>
  </div>

  <!-- NOTE modal body below -->
  <Modal :id="'viewKeep-' + keep.id">
    <template #modal-title>
      <div>
      </div>
    </template>
    <template #modal-body>
      <div class="d-flex">
        <img
          class="card-img img-fluid w-50"
          :src="keep.img"
          :title="keep.name"
        />
        <div class="ps-5 justify-content-center">
          <h3>{{ keep.name }}</h3>
          <div>
            <p>{{ keep.description }}</p>
          </div>
          <div class="">
            <img
                :src="keep.creator.picture"
                class="profilePic ms-3 rounded-circle"
                alt="profile picture"
                :title="keep.creator.name"
              />
            <p>{{ keep.creator.name }}</p>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
export default {
  props: {
    keep: {
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
.cardCreatorImg{
  height: 40px;
  width: 40px;
}
.brick {
  display: inline-block;
  width: 100%;
}
.profilePic {
  height: 3vh;
  width: 1.5vw;
}
</style>