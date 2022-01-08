<template>
  <div class="VaultPage">
    <div class="row justify-content-center">
      <div class="col-10 d-flex justify-content-between">
        <div>
          <p class="font vaultname m-0">{{ vault.name }}</p>
          <p class="font vaultdescription m-0">{{ vault.description }}</p>
          <p class="font keeps m-0">Keeps: {{ keeps.length }}</p>
        </div>
        <div class="align-self-center">
          <button class="btn btn-outline-secondary">Delete Vault</button>
        </div>
      </div>
    </div>
    <div class="masonry mt-5">
      <div class="item" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await keepsService.getByVaultKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    onMounted(async () => {
      try {
        await vaultsService.getVault(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.activeVaultKeeps),
      vault: computed(() => AppState.activeVault),

    }
  }
}
</script>


<style lang="scss" scoped>
.VaultPage {
  margin-top: 10vh;
}
.vaultname {
  margin-top: 5vh !important;
  font-size: 6vh;
}
.vaultdescription {
  font-size: 3vh;
  color: rgb(134, 134, 134);
}
.keeps {
  margin-top: 2vh !important;
  color: #e60023;
  font-size: 2vh;
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
</style>