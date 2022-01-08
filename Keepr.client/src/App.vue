<template>
  <header class="container-fluid">
    <NavigationBar class="row fixed-top" />
  </header>
  <main class="container-fluid back">
    <div class="row">
      <router-view />
    </div>
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
.back {
  background-color: white;
}
</style>
