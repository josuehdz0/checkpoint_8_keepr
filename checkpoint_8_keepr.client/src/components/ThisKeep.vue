<template>
  <div v-if="keep">
    {{ keep.name }}
    test?


  </div>
</template>


<script>
import { AppState } from "../AppState.js";
import { computed } from "@vue/reactivity";

export default {
  setup() {
    return {
      keep: computed(() => AppState.keep)
    }
  }
}
</script>


<style lang="scss" scoped></style>