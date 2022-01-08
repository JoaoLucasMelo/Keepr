<template>
  <header>
    <NavigationBar />
  </header>
  <main class="row">
    <router-view />
  </main>
  <footer></footer>
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
@import url("https://fonts.googleapis.com/css2?family=Comfortaa&display=swap");
.font {
  font-family: "Comfortaa", cursive;
}
</style>
