<template>
  <div class="Vault">
    <div>
      <div
        :title="vault.name"
        @click="getByVaultKeeps(vault.id)"
        class="card border-0 selectable"
      >
        <img class="card-img image" :src="vault.imgUrl" alt="" />
        <div
          class="
            card-img-overlay
            d-flex
            align-items-end
            justify-content-between
            w-100
          "
        >
          <p class="card-text name font m-0">{{ vault.name }}</p>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { onMounted } from "@vue/runtime-core"
import { useRouter } from "vue-router"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    const router = useRouter()
    return {
      router,
      async getByVaultKeeps(vaultId) {
        try {
          router.push({ name: "Vault", params: { id: vaultId } })
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
.name {
  font-size: 1.3vw;
  color: rgb(238, 238, 238);
}
.image {
  border-radius: 7%;
  height: 20vh;
  width: 12vw;
  object-fit: cover;
}
@media only screen and (max-width: 500px) {
  .image {
    width: 13vh;
    height: 15vh;
  }
  .name {
    font-size: 1.5vh;
  }
}
</style>