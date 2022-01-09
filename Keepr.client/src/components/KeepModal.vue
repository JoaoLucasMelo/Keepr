<template>
  <div class="KeepModal">
    <div
      class="modal fade"
      :id="'a' + keep.id + 'a'"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
          <div class="modal-body row modalcard m-0">
            <div class="col-6 backpic p-0"></div>
            <div class="col-6">
              <div class="text-end">
                <button
                  type="button"
                  class="btn-close me-4 mt-4"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="d-flex heigth flex-column justify-content-between">
                <div>
                  <div class="row">
                    <div class="font d-flex justify-content-center">
                      <p class="d-flex align-items-center f-14 me-4">
                        <i
                          :title="'Viewed ' + activeKeep.views + ' times'"
                          class="mdi mdi-24px mdi-eye-outline color me-2"
                        ></i>
                        {{ activeKeep.views }}
                      </p>
                      <p class="d-flex align-items-center f-14">
                        <i
                          :title="'Kept ' + activeKeep.keeps + ' times'"
                          class="mdi mdi-24px mdi-alpha-k-circle color me-2"
                        ></i>
                        {{ activeKeep.keeps }}
                      </p>
                    </div>
                  </div>
                  <div class="row text-center">
                    <p class="keepname font m-0">{{ keep.name }}</p>
                    <p class="keepdesc font">{{ keep.description }}</p>
                  </div>
                </div>
                <div>
                  <div class="row justify-content-center py-3">
                    <div class="line"></div>
                  </div>
                  <div class="row">
                    <div class="mt-3 d-flex">
                      <div class="col-4 d-flex justify-content-center">
                        <div class="dropdown">
                          <a
                            class="btn btn-secondary dropdown-toggle"
                            role="button"
                            id="dropdownMenuLink"
                            data-bs-toggle="dropdown"
                            aria-expanded="false"
                          >
                            Dropdown link
                          </a>
                          <ul
                            class="dropdown-menu"
                            aria-labelledby="dropdownMenuLink"
                          >
                            <li v-for="v in myVaults" :key="v.id">
                              <a
                                @click="addKeepToVault(keep.id, v.id)"
                                class="dropdown-item"
                                >{{ v.name }}</a
                              >
                            </li>
                          </ul>
                        </div>
                      </div>
                      <div class="col-2 d-flex justify-content-center">
                        <i
                          v-if="account.id === keep.creatorId"
                          @click="removeKeep(keep.id)"
                          class="mdi mdi-24px mdi-trash-can selectable"
                        ></i>
                      </div>
                      <div
                        @click="profile(keep.creatorId)"
                        class="
                          selectable
                          col-6
                          d-flex
                          justify-content-center
                          align-items-center
                        "
                      >
                        <p class="font profilename m-0 me-3 f-14">
                          {{ keep.creator.name }}
                        </p>
                        <img
                          class="profpic"
                          :src="keep.creator.picture"
                          alt=""
                        />
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { profileService } from "../services/ProfileService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { onMounted } from "@vue/runtime-core"
import { keepsService } from "../services/KeepsService"
import { AppState } from "../AppState"
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const router = useRouter()
    return {
      router,
      imgUrl: computed(() => `url(${props.keep.img})`),
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      async profile(id) {
        try {
          await profileService.getProfile(id)
          router.push({ name: "Profile", params: { id: id } })
          Modal.getOrCreateInstance(document.getElementById('a' + props.keep.id + 'a')).hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async removeKeep(id) {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById('a' + props.keep.id + 'a')).toggle()
            await keepsService.removeKeep(id)
            Pop.toast('Keep deleted!', 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async addKeepToVault(keepId, vaultId) {
        try {
          logger.log(keepId, vaultId)
          await keepsService.addKeepToVault(keepId, vaultId)
          Pop.toast('Keep added', 'success')
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
.modal-dialog {
  background-color: transparent;
}
.modal-content {
  background-color: transparent;
  border: 0;
}
.modalcard {
  background-color: white;
  height: 75vh;
  border-radius: 60px;
}
.backpic {
  background-image: v-bind(imgUrl);
  background-size: cover !important;
  border-bottom-left-radius: 60px;
  border-top-left-radius: 60px;
}
.modal-body {
  padding: 0;
}
.color {
  color: #e60023;
}
.keepname {
  font-size: 5vh;
  color: rgb(42, 42, 42);
}
.keepdesc {
  font-size: 3vh;
  color: rgb(42, 42, 42);
  padding-top: 5vh;
}
.line {
  margin: 5px;
  height: 0.8px;
  width: 90%;
  background: rgb(255, 255, 255);
  background: linear-gradient(
    90deg,
    rgba(255, 255, 255, 0) 0%,
    rgb(146, 146, 146) 25%,
    rgb(146, 146, 146) 75%,
    rgba(225, 225, 225, 0) 100%
  );
}
.heigth {
  height: 63vh;
}
.profpic {
  height: 2vw;
  width: 2vw;
  border-radius: 50%;
  object-fit: cover;
}
.profilename {
}
</style>