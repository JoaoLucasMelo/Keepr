<template>
  <div class="VaultPage"></div>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { keepsService } from "../services/KeepsService"
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
    return {
      keeps: computed(() => AppState.activeVaultKeeps),

    }
  }
}
</script>


<style lang="scss" scoped>
</style>