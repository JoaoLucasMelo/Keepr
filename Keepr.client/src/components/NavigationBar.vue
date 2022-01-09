<template>
  <div class="NavigationBar elevation-3">
    <div class="navbar p-0 d-flex justify-content-between">
      <div>
        <img
          title="Home"
          @click="homepage()"
          class="img-fluid logo action"
          src="../assets/img/K.png"
          alt=""
        />
      </div>
      <form @submit.prevent="querySearch" v-if="route.name === 'Home'">
        <div class="font">
          <div class="searchBar">
            <input
              id="searchQueryInput"
              type="text"
              name="searchQueryInput"
              placeholder="Search..."
              v-model="userInput"
            />
            <button
              id="searchQuerySubmit"
              type="submit"
              name="searchQuerySubmit"
            >
              <svg style="width: 24px; height: 24px" viewBox="0 0 24 24">
                <path
                  fill="#666666"
                  d="M9.5,3A6.5,6.5 0 0,1 16,9.5C16,11.11 15.41,12.59 14.44,13.73L14.71,14H15.5L20.5,19L19,20.5L14,15.5V14.71L13.73,14.44C12.59,15.41 11.11,16 9.5,16A6.5,6.5 0 0,1 3,9.5A6.5,6.5 0 0,1 9.5,3M9.5,5C7,5 5,7 5,9.5C5,12 7,14 9.5,14C12,14 14,12 14,9.5C14,7 12,5 9.5,5Z"
                />
              </svg>
            </button>
          </div>
        </div>
      </form>
      <div v-if="user.isAuthenticated">
        <div
          class="d-flex align-items-center action"
          title="Go to Profile"
          @click="profile()"
        >
          <div class="me-3 desktop">
            <p class="font m-0">{{ account.name }}</p>
          </div>
          <div class="me-4">
            <img class="profpic" :src="account.picture" alt="" />
          </div>
        </div>
      </div>
      <div v-else class="me-md-5 me-4">
        <button @click="login" class="btn btn-danger font loginbtn">
          Login
        </button>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { useRoute, useRouter } from "vue-router"
import { AuthService } from "../services/AuthService"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { profileService } from "../services/ProfileService"
import { keepsService } from "../services/KeepsService"
export default {
  setup() {
    let userInput = ref("")
    const router = useRouter()
    const route = useRoute()
    return {
      userInput,
      route,
      router,
      account: computed(() => AppState.account),
      homepage() {
        router.push({ name: "Home" })
        keepsService.getAll()
      },
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async profile() {
        try {
          await profileService.getProfile(this.account.id)
          router.push({ name: "Profile", params: { id: this.account.id } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async querySearch() {
        try {
          await keepsService.querySearch(userInput.value)
          userInput.value = ("")
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
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
  margin-left: 6vw;
  height: 4.2vh;
}

.searchBar {
  width: 70vw;
  display: flex;
  flex-direction: row;
  align-items: center;
}

#searchQueryInput {
  width: 100%;
  height: 2.8rem;
  background: #ebebeb;
  outline: none;
  border: none;
  border-radius: 1.625rem;
  padding: 0 3.5rem 0 1.5rem;
  font-size: 1rem;
}

#searchQuerySubmit {
  width: 3.5rem;
  height: 2.8rem;
  margin-left: -3.5rem;
  background: none;
  border: none;
  outline: none;
}

#searchQuerySubmit:hover {
  cursor: pointer;
}
#searchQueryInput:hover {
  background: #e2e2e2;
}
.profpic {
  height: 4.5vh;
  width: 4.5vh;
  border-radius: 50%;
  object-fit: cover;
}
.desktop {
  display: block;
}
@media only screen and (max-width: 500px) {
  .desktop {
    display: none;
  }
  .logo {
    margin-left: 2vh;
    height: 4.2vh;
  }
  .profpic {
    height: 4.5vh;
    width: 4.5vh;
  }
  .loginbtn {
    margin-left: 0.5vh;
    padding: 0.5vh;
    font-size: 1.5vh;
  }
  .searchBar {
    width: 30vh;
    display: flex;
    flex-direction: row;
    align-items: center;
  }
  .justified {
    display: flex;
    justify-content: center;
  }
}
</style>