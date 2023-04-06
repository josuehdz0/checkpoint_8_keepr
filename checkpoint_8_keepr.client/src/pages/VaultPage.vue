<template>
  <div v-if="vault" class="containter-fluid">
    <div class="row justify-content-center mt-0 mt-md-4">
      <div class="col-md-6 vaultphoto rounded" :style="{ backgroundImage: `url(${vault.img})` }">
        <div class="text-container d-flex justify-content-center align-items-baseline my-0">
          <h3 class="vaulttext pb-2 ps-0">
            {{ vault.name }}
          </h3>
          <h2 v-if="vault.isPrivate" class="">
            <div class="mdi mdi-lock vaulttext"></div>
          </h2>
        </div>
        <div class="vaulttext text-container d-flex justify-content-center align-items-baseline pb-2 mb-1">
          {{ vault.creator.name }}
        </div>
      </div>
      <div v-if="vault.creatorId == account.id" class="col-md-7 d-flex justify-content-end me-md-5">
        <button @click="deleteVault(vault.id)" class="btn">
          <h3 class="mdi mdi-trash-can text-danger"></h3>
        </button>
      </div>


      <div class="col-md-10 col-11">

        <div class="row pt-2">
          <h2> {{ keeps.length }} Keeps </h2>
        </div>

        <div class="row justify-content-center">
          <section class="bricks ">
            <div v-for="k in keeps">
              <KeepCard :keep="k" />
            </div>
          </section>
        </div>

      </div>

    </div>
  </div>
  <Modal id="exampleModal">
    <ThisKeep />
  </Modal>
</template>


<script>
import { onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()


    async function getActiveVault() {
      try {
        await vaultsService.setActiveVault(route.params.vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
        router.push({ name: 'Home' })
      }
    }
    // async function getVaultKeepsByProfileId() {
    //   try {
    //     const vaultCreatorId = AppState.vault.creatorId
    //     await vaultKeepsService.getVaultKeepsByCreatorId(vaultCreatorId)
    //   } catch (error) {
    //     Pop.error(error, "Getting all VaultKeeps by Profile Id")

    //   }
    // }

    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.vaultId)
      } catch (error) {
        Pop.error(error, "Getting all Keeps by Vault Id")
      }
    }

    onMounted(() => {
      getActiveVault();
      getKeepsByVaultId();
      // getVaultKeepsByProfileId()
    })
    return {
      vault: computed(() => AppState.vault),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaulKeeps: computed(() => AppState.vaultKeeps),

      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm('You sure you want to delete this Vault')) {
            await vaultsService.deleteVault(vaultId)
            router.push({ name: 'Profile', params: { profileId: this.account.id } })

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
$gap: .8em;

.vaultphoto {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  height: 40vh;
  background-size: cover;
  background-position: center;
  position: relative;
}

.vaultphoto::before {
  content: '';
  position: absolute;
  left: 0;
  bottom: 0;
  width: 100%;
  height: 50%;
  border-radius: 0.375rem;
  /* adjust the height as per your preference */
  background: linear-gradient(to bottom, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.5) 100%);
}

.text-container {
  margin: 0 1rem;
  z-index: 1;
}

.vaulttext {
  color: aliceblue;
  margin: 0;
}

.bricks {
  columns: 140px;
  column-gap: $gap;

  &>div {

    margin-top: $gap;
    display: inline-block;
  }
}

@media screen and (min-width: 789px) {
  .vaultphoto {
    display: flex;
    flex-direction: column;
    justify-content: flex-end;
    height: 23vh;
    background-size: cover;
    background-position: center;
    position: relative;
  }

  .vaultphoto::before {
    content: '';
    position: absolute;
    left: 0;
    bottom: 0;
    width: 100%;
    height: 50%;
    border-radius: 0.375rem;
    /* adjust the height as per your preference */
    background: linear-gradient(to bottom, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.5) 100%);
  }

  .bricks {
    columns: 180px;
    column-gap: $gap;

    &>div {

      margin-top: $gap;
      display: inline-block;
    }
  }
}
</style>