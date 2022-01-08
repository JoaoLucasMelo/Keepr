<template>
  <div class="NavigationBar elevation-3">
    <div class="navbar">
      <div class="ms-4">
        <img
          @click="homepage()"
          class="img-fluid logo selectable"
          src="../assets/img/K.png"
          alt=""
        />
      </div>
    </div>
  </div>
</template>


<script>
import { useRouter } from "vue-router"
export default {
  setup() {
    const router = useRouter()
    return {
      router,
      homepage() {
        router.push({ name: "Home" })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.navbar {
  height: 10vh;
  background-color: white;
}
.logo {
  height: 3.5vh;
}
</style>