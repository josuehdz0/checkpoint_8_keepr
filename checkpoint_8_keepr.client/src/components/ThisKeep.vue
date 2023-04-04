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
              <div class="mdi mdi-alpha-k-circle-outline"></div>

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

          <div class="row">
            <b class="col-8 d-flex align-items-center">My Vaults
              <button class="btn"> Save</button>
            </b>
            <div class="col-4 d-flex justify-content-end">
              <img :src="keep.creator.picture" alt=" photo" height="50" class="rounded-circle p-2 m-0 bottom-right" />
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

export default {
  setup() {
    return {
      keep: computed(() => AppState.keep)
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

.top-right {
  position: absolute;
  text-shadow: 1px 1px 15px #00000086;
  top: 1px;
  right: .01em;
}
</style>