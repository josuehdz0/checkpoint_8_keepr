<template>
  <div class="component bg-white rounded elevation-1" :style="backgro">
    <!-- <router-link :to="{ name: 'RestaurantDetails', params: { restaurantId: restaurant.id } }"> -->
    <img class="img-fluid rounded-top" :src="keep.img" alt="">
    <h4 class="p-2 text-dark text-center">{{ keep.name }}</h4>
    <div class="d-flex justify-content-between p-3 fw-bold text-info"><span> {{ keep.category }}</span> <span>{{
      keep.exposure
    }}</span> <i class="mdi mdi-note"></i>{{ keep.reviewCount }}</div>
    <!-- </router-link> -->
  </div>
</template>


<script>
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped></style>