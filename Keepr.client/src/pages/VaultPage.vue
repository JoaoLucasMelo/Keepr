<template>
  <div class="VaultPage">
    <div class="row justify-content-center">
      <div
        class="col-10 d-flex justified justify-content-between"
        v-if="account.id === vault.creatorId && edit === false"
      >
        <div class="justified">
          <div class="d-flex">
            <p class="font vaultname m-0">{{ vault.name }}</p>
            <i
              @click="edit = !edit"
              title="Edit Profile"
              class="
                mdi
                pencil
                mdi-24px mdi-pencil
                ms-3
                text-danger
                pb-2
                action
              "
            ></i>
          </div>
          <p class="font vaultdescription m-0">{{ vault.description }}</p>
          <p class="font keeps m-0">Keeps: {{ vaultkeeps.length }}</p>
        </div>
        <div class="align-self-center">
          <button
            title="Delete this Vault"
            v-if="account.id === vault.creatorId"
            @click="removeVault(vault.id, vault.creatorId)"
            class="btn font deletevault border border-2 p-2"
          >
            Delete Vault
          </button>
        </div>
      </div>
      <div
        class="col-10 d-flex justify-content-between mt-4"
        v-if="account.id === vault.creatorId && edit === true"
      >
        <form class="inputs font" @submit.prevent="editVault">
          <div class="mb-3">
            <label for="VaultName" class="form-label">Vault Name:</label>
            <input
              type="text"
              class="form-control"
              id="VaultName"
              placeholder="Vault Name..."
              maxlength="15"
              v-model="editable.name"
            />
          </div>
          <div class="mb-3">
            <label for="VaultDescription" class="form-label"
              >Vault Description:</label
            >
            <textarea
              type="text"
              class="form-control"
              id="VaultDescription"
              placeholder="Vault Description..."
              maxlength="200"
              v-model="editable.description"
            />
          </div>
          <div class="text-center mt-2">
            <button @click="edit = !edit" class="btn me-2">Cancel</button>
            <button type="submit" class="btn btn-outline-danger">Save</button>
          </div>
        </form>
      </div>
      <div
        class="col-10 d-flex justify-content-between"
        v-if="account.id !== vault.creatorId"
      >
        <div>
          <p class="font vaultname m-0">{{ vault.name }}</p>
          <p class="font vaultdescription m-0">{{ vault.description }}</p>
          <p class="font keeps m-0">Keeps: {{ vaultkeeps.length }}</p>
        </div>
        <div class="align-self-center">
          <button
            title="Delete this Vault"
            v-if="account.id === vault.creatorId"
            @click="removeVault(vault.id, vault.creatorId)"
            class="btn font deletevault border border-2 p-2"
          >
            Delete Vault
          </button>
        </div>
      </div>
    </div>
    <div class="masonry mt-5">
      <div class="item" v-for="k in vaultkeeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, ref } from "@vue/runtime-core"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute, useRouter } from "vue-router"
import { AppState } from "../AppState"
export default {
  name: 'Vault',
  setup() {
    document.title = "Keepr | Vault"
    const route = useRoute()
    const router = useRouter()
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
    let edit = ref(false)
    let editable = ref({})
    return {
      edit,
      editable,
      router,
      vaultkeeps: computed(() => AppState.activeVaultKeeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      async removeVault(id, creatorId) {
        try {
          if (await Pop.confirm()) {
            router.push({ name: 'Profile', params: { id: creatorId } })
            await vaultsService.removeVault(id)
            Pop.toast('Vault deleted!', 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async editVault() {
        try {
          await vaultsService.editVault(this.vault.id, editable.value)
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


<style lang="scss" scoped>
.VaultPage {
  margin-top: 10vh;
}
.vaultname {
  margin-top: 5vh !important;
  font-size: 6vh;
}
.pencil {
  margin-top: 5vh !important;
}
.vaultdescription {
  font-size: 3vh;
  color: rgb(134, 134, 134);
  border: #e60023;
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
.deletevault {
  color: #e60023;
  border-color: #e60023 !important;
}
.deletevault:hover {
  background-color: #e60023;
  color: rgb(238, 238, 238);
}
.inputs {
  width: 35vw;
}
@media only screen and (max-width: 500px) {
  .inputs {
    width: 35vh;
  }
  .masonry {
    column-count: 2;
    column-gap: 1em;
  }

  .item {
    margin: 0 0 0.5em;
  }
  .justified {
    display: flex;
    flex-direction: column;
    align-items: center !important;
  }
  .deletevault {
    margin-top: 2.5vh;
  }
}
</style>