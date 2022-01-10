<template>
  <div class="NewVault">
    <div
      class="modal fade"
      id="newVaultModal"
      tabindex="-1"
      aria-labelledby="newVaultModal"
      aria-hidden="true"
    >
      <div class="modal-dialog border-3">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title font">New Vault</h4>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body font">
            <form>
              <div>
                <label for="VaultName" class="form-label">Vault Name:</label>
                <input
                  type="text"
                  class="form-control"
                  id="vaultName"
                  placeholder="Vault Name..."
                  maxlength="20"
                  required
                  v-model="newVault.name"
                />
                <div class="text-end me-2">
                  <i
                    ><small>
                      {{ newVault.name?.length ? newVault.name.length : 0 }}
                      /20</small
                    ></i
                  >
                </div>
              </div>
              <div>
                <label for="VaultDescription" class="form-label"
                  >Vault Description:</label
                >
                <input
                  type="text"
                  class="form-control"
                  id="vaultDescription"
                  placeholder="Vault Description..."
                  maxlength="50"
                  required
                  v-model="newVault.description"
                />
                <div class="text-end me-2">
                  <i
                    ><small>
                      {{
                        newVault.description?.length
                          ? newVault.description.length
                          : 0
                      }}
                      /50</small
                    ></i
                  >
                </div>
              </div>
              <div class="mb-4">
                <label for="VaultImg" class="form-label"
                  >Vault Background Image:
                  <i class="text-secondary"
                    ><small>(Optional) </small></i
                  ></label
                >
                <input
                  type="text"
                  class="form-control"
                  id="vaultImage"
                  placeholder="Vault Background Image URL..."
                  v-model="newVault.imgUrl"
                />
              </div>
              <div class="form-check">
                <input
                  class="form-check-input border-0"
                  type="checkbox"
                  value="true"
                  id="flexCheckDefault"
                  v-model="newVault.isPrivate"
                />
                <label class="form-check-label" for="flexCheckDefault">
                  Private
                  <i class="text-secondary"
                    ><small>(Can only be seen by you)</small></i
                  >
                </label>
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
            <button @click="createVault" type="button" class="btn btn-danger">
              Create
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
import { vaultsService } from "../services/VaultsService"
import { Modal } from "bootstrap"
export default {
  setup() {
    let newVault = ref({})
    return {
      newVault,
      async createVault() {
        try {
          await vaultsService.createVault(newVault.value)
          newVault.value = {}
          const modalElem = document.getElementById('newVaultModal')
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