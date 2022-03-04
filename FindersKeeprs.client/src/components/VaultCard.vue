<template>
  <div class="col-md-3 p-2">
    <div class="card" style="width: 18rem;">
      <div class="card-body">
        <h5 class="card-title">{{vault.name}}</h5>
        <p class="card-text">{{vault.description}}</p>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    vault: {
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

</style>