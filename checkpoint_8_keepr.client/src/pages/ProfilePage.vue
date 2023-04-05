<template>
  <div class="component">


  </div>
</template>


<script>
import { onMounted } from "vue";
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
    return {
      profile: computed(() => AppState.profile)
    }
  }
}
</script>


<style lang="scss" scoped></style>