<template>
  <div class="Profile">
    <div class="row justify-content-center">
      <div class="col-md-10 margintop d-md-flex justify-content-between">
        <div
          class="d-md-flex"
          v-if="account.id === activeProfile.id && edit === false"
        >
          <div>
            <img
              class="profpic elevation-5"
              :src="activeProfile.picture"
              alt=""
            />
          </div>
          <div>
            <div class="d-flex">
              <p class="font m-0 username">{{ activeProfile.name }}</p>
              <i
                @click="edit = !edit"
                title="Edit Profile"
                class="mdi mdi-24px mdi-pencil ms-3 text-danger pb-2 action"
              ></i>
            </div>
            <p class="font mt-2 m-0 vaultskeeps">
              Vaults: {{ activeProfVaults.length }}
            </p>
            <p class="font m-0 vaultskeeps">
              Keeps: {{ activeProfKeeps.length }}
            </p>
          </div>
        </div>
        <div
          class="d-md-flex"
          v-if="account.id === activeProfile.id && edit === true"
        >
          <form class="inputs font" @submit.prevent="editProfile">
            <div class="mb-3">
              <label for="ProfileName" class="form-label">Name:</label>
              <input
                type="text"
                class="form-control"
                id="ProfileName"
                placeholder="Name..."
                maxlength="15"
                v-model="editable.name"
              />
            </div>
            <div class="mb-3">
              <label for="ProfileName" class="form-label">ImgUrl:</label>
              <input
                type="url"
                class="form-control"
                id="ProfileName"
                placeholder="ImgUrl..."
                v-model="editable.picture"
              />
            </div>
            <div class="text-center mt-3">
              <button @click="edit = !edit" class="btn me-2">Cancel</button>
              <button type="submit" class="btn btn-outline-danger">Save</button>
            </div>
          </form>
        </div>
        <div class="d-md-flex" v-if="account.id !== activeProfile.id">
          <div>
            <img
              class="profpic elevation-5"
              :src="activeProfile.picture"
              alt=""
            />
          </div>
          <div>
            <p class="font m-0 username">{{ activeProfile.name }}</p>
            <p class="font mt-2 m-0 vaultskeeps">
              Vaults: {{ activeProfVaults.length }}
            </p>
            <p class="font m-0 vaultskeeps">
              Keeps: {{ activeProfKeeps.length }}
            </p>
          </div>
        </div>
        <div v-if="user.isAuthenticated && account?.id === activeProfile.id">
          <button @click="logout" class="btn btn-outline-danger font">
            Logout
          </button>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="font vaults">
        <p>
          Vaults
          <i
            v-if="account?.id === activeProfile.id && user.isAuthenticated"
            data-bs-toggle="modal"
            data-bs-target="#newVaultModal"
            class="mdi plusbtn mdi-plus action"
            title="Add New Vault"
          ></i>
        </p>
      </div>
      <div
        class="col-2 d-flex justify-content-center p-2"
        v-for="v in activeProfVaults"
        :key="v.id"
      >
        <Vault :vault="v" />
      </div>
    </div>
    <div class="row mb-5">
      <div class="font vaults">
        <p>
          Keeps
          <i
            v-if="account?.id === activeProfile.id && user.isAuthenticated"
            data-bs-toggle="modal"
            data-bs-target="#newKeepModal"
            class="mdi action plusbtn mdi-plus"
            title="Add New Keep"
          ></i>
        </p>
      </div>
      <div class="masonry">
        <div class="item" v-for="k in activeProfKeeps" :key="k.id">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
  </div>
  <NewVault />
  <NewKeep />
</template>

<script>
import { computed, onMounted, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
import { profileService } from "../services/ProfileService"
import { AuthService } from "../services/AuthService"
import { accountService } from "../services/AccountService"
export default {
  name: 'Profile',
  setup() {
    document.title = "Keepr | Profile"
    const route = useRoute()
    let edit = ref(false)
    let editable = ref({})
    onMounted(async () => {
      try {
        await profileService.getProfile(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    watchEffect(async () => {
      try {
        await profileService.getVaults(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    onMounted(async () => {
      try {
        await profileService.getKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })

    return {
      edit,
      editable,
      activeProfile: computed(() => AppState.activeProfile),
      activeProfVaults: computed(() => AppState.activeProfVaults),
      activeProfKeeps: computed(() => AppState.activeProfKeeps),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },
      async editProfile() {
        try {
          await accountService.editProfile(editable.value)
          edit.value = !edit.value
          editable.value = {}
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, 'error')
        }
      },
    }
  }
}
</script>

<style scoped>
.Profile {
  margin-top: 10vh;
}
.profpic {
  height: 20vh;
  width: 20vh;
  object-fit: cover;
  border-radius: 10%;
}
.margintop {
  margin-top: 5vw;
}
.username {
  font-size: 5.5vh;
  margin-left: 3vw !important;
}
.vaultskeeps {
  font-size: 3.2vh;
  margin-left: 3vw !important;
}
.vaults {
  margin-top: 8vh;
  padding-left: 5vh;
  font-size: 6vh;
}
.plusbtn {
  color: #e60023;
  transition: all 0.2s ease-in-out;
}
.plusbtn:hover {
  transform: scale(1.05);
}
.masonry {
  column-count: 4;
  column-gap: 2em;
}

.item {
  display: inline-block;
  margin: 0 0 1em;
  width: 100%;
}
.inputs {
  width: 35vw;
}
</style>
