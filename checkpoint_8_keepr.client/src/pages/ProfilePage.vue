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
        <!-- NOTE Here are the Profiles Vaults -->
        <div class="row">
          vault cards
        </div>
      </div>
      <div class="col-md-10">
        <div class="row">
          <h3> Keeps</h3>
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

    onMounted(() => {
      getProfileById();
      getKeepsByProfileId();
    });

    onUnmounted(() => {
      profilesService.clearProfile();
    })
    return {
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),

    }
  },
  components: { ThisKeep }
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