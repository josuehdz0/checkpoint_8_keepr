<template>
  <div v-if="profile" class="container">
    <!-- NOTE Profile Info -->
    <div class="row">
      <div class="col-12">
        <div class="row">
          cover photo
        </div>
        <div class="row">

          {{ profile.name }}

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


<style lang="scss" scoped></style>