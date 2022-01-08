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
      <div>
        <button
          class="
            btn
            selectable
            text-success
            lighten-30
            text-uppercase
            my-2 my-lg-0
          "
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { useRouter } from "vue-router"
import { AuthService } from "../services/AuthService"
import { AppState } from "../AppState"
export default {
  setup() {
    const router = useRouter()
    return {
      router,
      homepage() {
        router.push({ name: "Home" })
      },
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
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