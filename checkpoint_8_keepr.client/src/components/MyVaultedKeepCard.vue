<template>
  <div @click="setActiveKeep()" data-bs-toggle="modal" data-bs-target="#exampleModal">

    <div class="component bg-white rounded shadow-sm">
      <!-- <router-link :to="{ name: 'RestaurantDetails', params: { restaurantId: restaurant.id } }"> -->

      <img class="img-fluid rounded" :src="keep.img" alt="">

      <div class="innershadow bottom-center">
      </div>
      <h4 class="p-2 m-0 text-light bottom-left">{{ keep.name }}</h4>
      <img :src="keep.creator.picture" alt=" photo" height="50" class="profile-picture p-2 bottom-right" />



      <!-- </router-link> -->
    </div>

  </div>
</template>


<script>
import { keepsService } from "../services/KeepsService.js";

export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    return {
      async setActiveKeep() {
        keepsService.setActiveKeep(props.keep)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.component {
  position: relative;
  text-align: center;
  color: white;
}

.bottom-left {
  position: absolute;
  text-shadow: 1px 1px 15px #00000086;
  bottom: 1px;
  left: .01em;
}

.bottom-right {
  position: absolute;
  bottom: 1px;
  right: .01em;
}

.bottom-center {
  position: absolute;
  bottom: 8px;
  right: 50%;
}

.innershadow {
  height: 90px;
  background: linear-gradient(180.3deg, rgba(0, 0, 0, 0) 49.73%, rgba(0, 0, 0, 0.664) 90%);
  // translate: 0px -90px;
}

.profile-picture {
  height: 6vh;
  width: 6vh;
  border-radius: 50%;
  object-fit: cover;
  // box-shadow: 2px 2px 4px black;
}
</style>