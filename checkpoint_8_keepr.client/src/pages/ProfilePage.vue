<template>
  <div v-if="profile" class="container-fluid">
    <!-- NOTE Profile Page -->
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="row">
          <img :src="profile.coverImg" alt="" class="img-fluid rounded coverimg p-0">
        </div>
        <!-- NOTE  Profile info that translates over coverimg-->
        <div class="row justify-content-center profile-info">
          <div class="col-12 d-flex flex-column align-items-center">
            <img :src="profile.picture" alt="" class="p-0 profile-picture ">
            <h2 class="pt-2">
              {{ profile.name }}
            </h2>
            <div class="fw-semibold">
              # vaults | # keeps
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-10">
        <div class="row">
          <h3> Vaults</h3>
        </div>
        <div class="row">
          vault cards
        </div>
      </div>
      <div class="col-md-10">
        <div class="row">
          <h3> Keeps</h3>
        </div>
        <div class="row">
          Keep cards
        </div>

      </div>
    </div>
  </div>
</template>


<script>
import { onMounted, onUnmounted } from "vue";
import { useRoute } from "vue-router";
import { logger } from "../utils/Logger.js";
import { profilesService } from "../services/ProfilesService.js";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";

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
    onMounted(() => {
      getProfileById();
    });

    onUnmounted(() => {
      profilesService.clearProfile();
    })
    return {
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),

    }
  }
}
</script>


<style lang="scss" scoped>
.coverimg {
  height: 25vh;
  object-fit: cover;
  object-position: center;
}

.profile-info {
  translate: 0px -8vh;

}

.profile-picture {
  height: 16vh;
  width: 16vh;
  border: 1px solid whitesmoke;
  border-radius: 50%;
  object-fit: cover;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.363);
}
</style>