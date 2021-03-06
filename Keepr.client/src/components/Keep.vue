<template>
  <div class="Keep">
    <div :title="keep.name">
      <div
        @click="keepModal(keep.id)"
        class="card carddiv border-0 grow selectable"
      >
        <img class="card-img image" :src="keep.img" alt="" />
        <div
          class="
            card-img-overlay
            d-flex
            flex-column
            justify-content-between
            w-100
          "
        >
          <div
            class="text-end"
            v-if="
              route.name === 'Vault' &&
              user.isAuthenticated &&
              vault.creatorId === account?.id
            "
          >
            <button
              @click.stop="removeKeepFromVault(keep.vaultKeepId)"
              title="Remove from this Vault"
              class="btn font p-0"
            >
              <i class="mdi mdi-close text-danger mdi-24px"></i>
            </button>
          </div>
          <div v-else></div>
          <div
            class="d-flex w-100 justify-content-between"
            :title="'Go to ' + keep.creator.name + `'s Profile`"
          >
            <p class="card-text name font m-0">{{ keep.name }}</p>
            <button @click.stop="profile(keep.creatorId)" class="btn p-0">
              <img class="profpic" :src="keep.creator.picture" alt="" />
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <KeepModal :keep="keep" />
</template>


<script>
import { computed } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import { profileService } from "../services/ProfileService"
import Pop from "../utils/Pop"
import { useRoute, useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { keepsService } from "../services/KeepsService"
import { AppState } from "../AppState"
export default {
  props: {
    keep: { type: Object, required: true },
    vault: { type: Object },
  },
  setup(props) {
    const router = useRouter()
    const route = useRoute()
    return {
      route,
      router,
      img: computed(() => props.keep.img),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async profile(id) {
        try {
          await profileService.getProfile(id)
          router.push({ name: "Profile", params: { id: id } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async keepModal(id) {
        try {
          await keepsService.getVaultsAlreadyIn(props.keep.id)
          await keepsService.getKeepById(props.keep.id)
          Modal.getOrCreateInstance(document.getElementById('a' + id + 'a')).toggle()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async removeKeepFromVault(id) {
        try {
          if (await Pop.confirm()) {
            await keepsService.removeKeepFromVault(id)
            Pop.toast('Keep removed from this Vault', 'success')
          }
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
.carddiv {
  border-radius: 50px;
}
.image {
  border-radius: 50px;
}
.name {
  font-size: 1.7vw;
  color: rgb(255, 255, 255);
}
.profpic {
  height: 2.3vw;
  width: 2.3vw;
  border-radius: 50%;
  object-fit: cover;
}
@media only screen and (max-width: 500px) {
  .name {
    font-size: 2vh;
  }
  .profpic {
    height: 3vh;
    width: 3vh;
  }
}
</style>