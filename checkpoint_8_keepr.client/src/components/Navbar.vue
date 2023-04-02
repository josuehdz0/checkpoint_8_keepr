<template>
  <nav
    class="navbar navbar-expand-lg navbar-light bg-light px-3 shadow-sm d-flex justify-content-between py-0 fixed-bottom sticky-md-top">
    <div class="navbar-brand d-flex">
      <div class="d-flex flex-column align-items-center ">
        <router-link :to="{ name: 'Home' }">
          <h5 class="my-0 ">Home</h5>
        </router-link>
      </div>
    </div>
    <div class="d-flex flex-column align-items-center ">
      <h2 class="my-0 d-none d-md-flex">keepr</h2>
      <h1 class="mdi mdi-alpha-k-circle-outline d-md-none "></h1>
    </div>
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul>
    </div> -->
    <!-- LOGIN COMPONENT HERE -->
    <Login />
  </nav>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
