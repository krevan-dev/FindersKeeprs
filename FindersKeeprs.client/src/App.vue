<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
    <div class="bg-dark text-light text-center p-4">
      Made by Kyle E.
    </div>
  </footer>
  <Modal id="createKeep">
    <template #modal-title></template>
    <template #modal-body><CreateKeep /></template>
  </Modal>
  <Modal id="createVault">
    <template #modal-title></template>
    <template #modal-body><CreateVault /></template>
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

</style>
