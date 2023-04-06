<template>
  <div v-if="profile" class="container-fluid">
    <!-- NOTE Profile Page -->
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="row mt-0 mt-md-4">
          <img
            :src="profile.coverImg ? profile.coverImg : 'https://pbs.twimg.com/media/Fhh5tFcXkAcwX4f?format=jpg&name=900x900'"
            alt="Cover Img" class="img-fluid rounded coverimg p-0 d-none d-md-flex">

          <img
            :src="profile.coverImg ? profile.coverImg : 'https://pbs.twimg.com/media/Fhh5tFcXkAcwX4f?format=jpg&name=900x900'"
            alt="Cover Img" class="img-fluid  coverimg p-0 d-md-none ">

        </div>
        <!-- NOTE  Profile info that translates over coverimg-->
        <div class="row justify-content-center profile-info">
          <div class="col-12 d-flex flex-column align-items-center">
            <img :src="profile.picture" alt="" class="p-0 profile-picture ">
            <h2 class="pt-3">
              {{ profile.name }}
            </h2>
            <div class="fw-semibold pt-1">
              {{ vaults.length }} vaults | {{ keeps.length }} keeps
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-10">
        <div class="row vaulttranslate">
          <h2> Vaults</h2>
        </div>
        <!-- NOTE Here are the Profiles Vaults -->
        <div class="row justify-content-between justify-content-md-evenly vaulttranslate">
          <div class="col-md-3 col-6 p-2 " v-for="v in vaults">

            <VaultCard :vault="v" />

          </div>
        </div>
      </div>
      <div class="col-md-10">
        <div class="row">
          <h2> Keeps</h2>
        </div>

        <!-- NOTE Here are the Profiles Keeps -->
        <div class="row">
          <section class="bricks">
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
import { onMounted, onUnmounted } from "vue";
import { useRoute } from "vue-router";
import { logger } from "../utils/Logger.js";
import { profilesService } from "../services/ProfilesService.js";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import ThisKeep from "../components/ThisKeep.vue";
import VaultCard from "../components/VaultCard.vue";
import { vaultsService } from "../services/VaultsService.js";
import { keepsService } from "../services/KeepsService.js";


export default {
  setup() {
    const route = useRoute();
    async function getProfileById() {
      try {
        const profileId = route.params.profileId;
        logger.log("here is the profile id", profileId);
        await profilesService.getProfileById(profileId);
      }
      catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }

    async function getKeepsByProfileId() {
      try {
        await profilesService.getKeepsByCreatorId(route.params.profileId)
      } catch (error) {
        Pop.error(error, "Getting all Keeps by ProfileId")
      }
    }

    async function getVaultsByProfileId() {
      try {
        await profilesService.getVaultsByCreatorId(route.params.profileId)
      } catch (error) {
        Pop.error(error, "Getting all Vaults by ProfileId")
      }
    }

    onMounted(() => {
      getProfileById();
      getKeepsByProfileId();
      getVaultsByProfileId();
    });

    onUnmounted(() => {
      profilesService.clearProfile();
      keepsService.clearKeeps();
      vaultsService.clearVaults();
    });
    return {
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),

    }
  },
  components: { ThisKeep, VaultCard }
}
</script>


<style lang="scss" scoped>
$gap: .8em;

.coverimg {
  height: 25vh;
  object-fit: cover;
  object-position: center;
}

.profile-info {
  translate: 0px -8vh;
}

.vaulttranslate {
  translate: 0px -6vh;
}

.profile-picture {
  height: 16vh;
  width: 16vh;
  border: 1px solid whitesmoke;
  border-radius: 50%;
  object-fit: cover;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.363);
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