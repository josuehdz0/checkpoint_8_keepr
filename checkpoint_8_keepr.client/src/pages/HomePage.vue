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
</template>

<script>
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { onMounted } from "vue";
import { AppState } from "../AppState.js";
import { computed } from "@vue/reactivity";

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        Pop.error(error, "getting all keeps")
      }
    }
    onMounted(() => {
      getAllKeeps();
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
$gap: .5em;

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
  columns: 300px;
  column-gap: $gap;

  &>div {
    // &> makes it only target the direct child of bricks, not all the divs inside bricks
    margin-top: $gap;
    display: inline-block;
  }
}
</style>
