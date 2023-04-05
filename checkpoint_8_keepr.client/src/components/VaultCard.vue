<template>
  <div class="component rounded vaultimg " :style="{ backgroundImage: `url(${vault.img})` }">
    <div class="text-container d-flex justify-content-between align-items-baseline my-0">
      <h3 class="vaulttext pb-2 ps-0">
        {{ vault.name }}
      </h3>
      <h2 v-if="vault.isPrivate" class="">
        <div class="mdi mdi-lock vaulttext"></div>
      </h2>

    </div>


  </div>
</template>


<script>
export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.vaultimg {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  height: 19vh;
  background-size: cover;
  background-position: center;
  position: relative;
}

.vaultimg::before {
  content: '';
  position: absolute;
  left: 0;
  bottom: 0;
  width: 100%;
  height: 50%;
  border-radius: 0.375rem;
  /* adjust the height as per your preference */
  background: linear-gradient(to bottom, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.5) 100%);
}


.text-container {
  margin: 0 1rem;
  z-index: 1;
}

.vaulttext {
  color: aliceblue;
  margin: 0;

}
</style>