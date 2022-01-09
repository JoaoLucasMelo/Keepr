<template>
  <header class="container-fluid">
    <NavigationBar class="row p-0 fixed-top" />
  </header>
  <main class="container-fluid back scrollbar">
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
.scrollbar {
  overflow-y: scroll;
  height: 100vh;
}
.scrollbar::-webkit-scrollbar {
  width: 7px;
}
.scrollbar::-webkit-scrollbar-track {
  background: transparent;
}
.scrollbar::-webkit-scrollbar-thumb {
  background-color: #e60023;
  border-radius: 10px;
}
.grow {
  // box-shadow: 4px 2px 2px rgb(104, 104, 104);
  transition: all 0.2s ease-in-out;
}
.grow:hover {
  transform: scale(1.01);
  box-shadow: 3px 5px 5px rgb(117, 117, 117);
}
.btn:focus {
  outline: none;
  box-shadow: none;
}
</style>
