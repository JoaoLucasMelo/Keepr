<template>
  <div class="home masonry">
    <div class="item" v-for="k in keeps" :key="k.id">
      <Keep :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
import { accountService } from "../services/AccountService"
export default {
  name: 'Home',
  setup() {
    document.title = "Keepr | Home"
    onMounted(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  margin-top: 13vh;
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
