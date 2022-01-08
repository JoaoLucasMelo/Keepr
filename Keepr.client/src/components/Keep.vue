<template>
  <div class="Keep">
    <div>
      <div class="card border-0">
        <img class="card-img image" :src="keep.img" alt="" />
        <div
          class="
            card-img-overlay
            d-flex
            align-items-end
            justify-content-between
            w-100
          "
        >
          <p class="card-text name font m-0">{{ keep.name }}</p>
          <button @click.stop="profile(keep.creatorId)" class="btn p-0">
            <img class="profpic" :src="keep.creator.picture" alt="" />
          </button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import { profileService } from "../services/ProfileService"
import Pop from "../utils/Pop"
import { useRoute, useRouter } from "vue-router"
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const router = useRouter()
    return {
      router,
      img: computed(() => props.keep.img),
      async profile(id) {
        try {
          await profileService.getProfile(id)
          router.push({ name: "Profile", params: { id: id } })
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
.image {
  border-radius: 7%;
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
</style>