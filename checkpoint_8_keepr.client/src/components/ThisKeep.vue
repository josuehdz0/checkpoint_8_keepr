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
          <img :src="keep.img" :alt="keep.name" class="img-fluid rounded-start">
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

          <div class="row justify-content-evenly">
            <div v-if="account.id" class="col-5 d-flex align-items-center ps-2 pe-0">
              <div class="btn-group ">
                <button class="btn btn-sm btn-secondary-outline dropdown-toggle" type="button" data-bs-toggle="dropdown"
                  aria-expanded="false">
                  My Vaults
                </button>
                <ul class="dropdown-menu">
                  <li v-for="v in vaults" :key="v.id">
                    <a class="dropdown-item" href="#">{{ v.name }}
                      <h v-if="v.isPrivate" class="mdi mdi-lock"></h>
                    </a>
                  </li>
                </ul>
              </div>
              <button class="btn btn-sm"> Save</button>
              <button v-if="keep.creatorId == account.id" class="btn d-flex align-items-center"
                @click="deleteKeep(keep.id)">
                <div class="mdi mdi-trash-can fs-2 text-danger"></div>
              </button>
            </div>
            <div class="col-4  d-flex align-items-center p-0">
              {{ keep.creator.name }}
            </div>

            <div class="col-2 d-flex justify-content-end pe-1 pb-1">
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
import { profilesService } from "../services/ProfilesService.js";
import { onMounted } from "vue";
// import bootstrap from "bootstrap";

export default {
  setup() {
    const router = useRouter();
    const account = AppState.account;

    async function getMyVaults() {
      try {
        await profilesService.getVaultsByCreatorId(account.id)
      } catch (error) {
        Pop.error(error, "Getting my Vaults")
      }
    }

    onMounted(() => {
      getMyVaults();
    })


    return {
      keep: computed(() => AppState.keep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),

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
      }


    }
  }
}
</script>


<style lang="scss" scoped>
.photo {
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