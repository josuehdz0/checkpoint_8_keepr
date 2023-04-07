<template>
  <div v-if="keep">

    <div class="container-fluid">
      <div class="row">
        <!-- NOTE Doing weird stuff to not get rounded edges on picture in mobile -->
        <div class="col-md-6 p-0 d-md-none photo">
          <img :src="keep.img" :alt="keep.name" class="img-fluid ">
          <button type="button" class="btn-close p-2 d-md-none top-right" data-bs-dismiss="modal"
            aria-label="Close"></button>
        </div>
        <div class="col-md-6 p-0 d-none d-md-flex">
          <img :src="keep.img" :alt="keep.name" class="img-fluid rounded-start photo">
        </div>
        <!-- NOTE Keep info -->
        <div class="col-md-6 d-flex flex-column justify-content-between infosize">

          <div class="row justify-content-center text-center pt-3">
            <div class="col-3">
              <div class="mdi mdi-eye-outline"> {{ keep.views }}</div>

            </div>
            <div class="col-3">
              <div class="mdi mdi-alpha-k-circle-outline">{{ keep.kept }}</div>

            </div>
          </div>
          <div class="row justify-content-center ">
            <h2 class="text-center">
              {{ keep.name }}
            </h2>
            <p>
              {{ keep.description }}
            </p>
          </div>

          <div class="row justify-content-end">
            <div v-if="account.id" class="col-6 d-flex align-items-center ps-2 pe-0">

              <div v-if="keep.vaultKeepId && vault.creatorId == account.id" class="ms-2">
                <button @click="removeKeep(keep.vaultKeepId)" class="btn btn-warning">
                  Remove
                  <i class="mdi mdi-bookmark-remove-outline"></i>
                </button>
              </div>

              <div v-else>
                <form @submit.prevent="handleSubmit(keep.id)" class="d-flex align-items-center">
                  <div class="">
                    <select class="form-select form-select-sm" id="vaultSelect" required v-model="editable.vaultId">
                      <option value="" selected disabled>My Vaults</option>
                      <option v-for="vault in myVaults" :key="vault.id" :value="vault.id">
                        {{ vault.name }}
                        <span v-if="vault.isPrivate" class="mdi mdi-lock"> 🔒</span>
                      </option>
                    </select>
                  </div>
                  <div class="d-flex align-items-center">

                    <button type="submit" class="btn btn-small ">Save</button>
                  </div>
                </form>
              </div>




            </div>


            <div class="col-6 d-flex justify-content-end align-items-center pe-1 pb-1">
              <button v-if="keep.creatorId == account.id" class="btn btn-sm d-flex align-items-center "
                @click="deleteKeep(keep.id)">
                <div class="mdi mdi-trash-can fs-5 text-danger "></div>
              </button>
              <b class="text-truncate">

                {{ keep.creator.name }}
              </b>
              <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                <img :src="keep.creator.picture" alt=" photo" height="50" class="profile-picture p-2"
                  data-bs-dismiss="modal" />
              </router-link>

            </div>
          </div>


        </div>
      </div>
    </div>



  </div>
</template>


<script>
import { AppState } from "../AppState.js";
import { computed } from "@vue/reactivity";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import { useRoute, useRouter } from "vue-router";
import { Modal } from "bootstrap";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { profilesService } from "../services/ProfilesService.js";
import { onMounted, ref } from "vue";
// import bootstrap from "bootstrap";

export default {
  setup() {
    const router = useRouter();
    const editable = ref({});





    return {
      editable,
      keep: computed(() => AppState.keep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      myVaults: computed(() => AppState.myVaults),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.vault),

      async deleteKeep(keepId) {
        try {
          if (await Pop.confirm('You sure you want to delete this keep?')) {
            await keepsService.deleteKeep(keepId)
            // router.push({ name: 'Home' })
            Modal.getOrCreateInstance('#exampleModal').hide()

          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },

      async removeKeep(vaultKeepId) {
        try {
          if (await Pop.confirm('You sure you want to remove this Keep from your Vault?')) {
            await vaultKeepsService.deleteVaultKeep(vaultKeepId)
            Modal.getOrCreateInstance('#exampleModal').hide()
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },

      async handleSubmit(keepId) {
        try {
          const vaultId = editable.value.vaultId
          const vaultKeepBody = { vaultId, keepId }
          await vaultKeepsService.createVaultKeep(vaultKeepBody)
          logger.log("VaultId", vaultId, "and KeepId", keepId)
          Modal.getOrCreateInstance('#exampleModal').hide()
          router.push({ name: 'Vault', params: { vaultId: vaultId } })
          editable.value = {}


        } catch (error) {
          Pop.error(error, "Creating Vaultkeep")

        }
      }


    }
  }
}
</script>


<style lang="scss" scoped>
.photo {
  height: auto;
  width: 100%;
  position: relative;
  text-align: center;
  color: white;
}

.infosize {
  height: auto;
  min-height: 100%;
}

.profile-picture {
  height: 8vh;
  width: 8vh;
  border-radius: 50%;
  object-fit: cover;
  // box-shadow: 2px 2px 4px black;
}

.top-right {
  position: absolute;
  text-shadow: 1px 1px 15px #00000086;
  top: 1px;
  right: .01em;
}
</style>