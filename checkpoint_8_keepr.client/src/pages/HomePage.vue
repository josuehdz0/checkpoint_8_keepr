<template>
  <div class="container">
    <div class="row">
      <section class="bricks">
        <div v-for="k in keeps">
          <KeepCard :keep="k" />
        </div>
      </section>
    </div>
  </div>
  <Modal id="exampleModal">
    <ThisKeep />
  </Modal>
</template>

<script>
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { onMounted, onUnmounted, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { computed } from "@vue/reactivity";
import ThisKeep from "../components/ThisKeep.vue";
import { profilesService } from "../services/ProfilesService.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        Pop.error(error, "getting all keeps");
      }
    }

    async function getMyVaults() {
      try {
        await profilesService.getVaultsByAccountId(AppState.account.id)
      } catch (error) {
        Pop.error(error, "Getting my Vaults")
      }
    }

    onMounted(() => {
      getAllKeeps();
      // NOTE vvv this is in my AuthService, runs everytime somebody logs in.
      // getMyVaults();

    });

    onUnmounted(() => {
      profilesService.clearProfile();
      keepsService.clearKeeps();
      vaultsService.clearVaults();
    });
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { ThisKeep }
}
</script>

<style scoped lang="scss">
$gap: .9em;

.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.bricks {
  columns: 150px;
  column-gap: $gap;

  &>div {

    margin-top: $gap;
    display: inline-block;
  }
}




@media screen and (min-width: 789px) {
  .bricks {
    columns: 200px;
    column-gap: $gap;

    &>div {

      margin-top: $gap;
      display: inline-block;
    }
  }
}
</style>
