<template>
  <div class="NewKeep">
    <div
      class="modal fade"
      id="newKeepModal"
      tabindex="-1"
      aria-labelledby="newKeepModal"
      aria-hidden="true"
    >
      <div class="modal-dialog border-3">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title font">New Keep</h4>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body font">
            <form>
              <div class="mb-3">
                <label for="KeepName" class="form-label">Keep Name:</label>
                <input
                  type="text"
                  class="form-control"
                  id="KeepName"
                  placeholder="Keep Name..."
                  maxlength="20"
                  required
                  v-model="newKeep.name"
                />
              </div>
              <div class="mb-3">
                <label for="KeepImg" class="form-label">Image:</label>
                <input
                  type="text"
                  class="form-control"
                  id="KeepImage"
                  placeholder="Image URL..."
                  v-model="newKeep.img"
                />
              </div>
              <div class="mb-3">
                <label for="KeepDescription" class="form-label"
                  >Description:</label
                >
                <textarea
                  type="text"
                  class="form-control"
                  id="KeepDescription"
                  placeholder="Description..."
                  required
                  v-model="newKeep.description"
                />
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn text-secondary"
              data-bs-dismiss="modal"
            >
              Cancel
            </button>
            <button @click="createKeep" type="button" class="btn btn-danger">
              Post
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { Modal } from "bootstrap"
export default {
  setup() {
    let newKeep = ref({})
    return {
      newKeep,
      async createKeep() {
        try {
          await keepsService.createKeep(newKeep.value)
          newKeep.value = {}
          const modalElem = document.getElementById('newKeepModal')
          Modal.getOrCreateInstance(modalElem).hide()
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
.modal-content {
  border-color: #e60023;
  border-width: 1px;
  border-style: solid;
}
</style>