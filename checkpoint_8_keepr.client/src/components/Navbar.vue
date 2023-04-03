<template>
  <nav
    class="navbar navbar-expand-lg navbar-light bg-light px-3 shadow-sm d-flex justify-content-between py-0 fixed-bottom sticky-md-top">
    <div class="navbar-brand d-flex">
      <div class="d-flex align-items-center ">
        <router-link :to="{ name: 'Home' }">
          <h5 class="my-0 ps-1 d-none d-md-flex">Home</h5>
          <h1 class="mdi mdi-alpha-k-circle-outline d-md-none "></h1>
        </router-link>

        <h5 class="nav-link  my-0  px-3 fw-semibold d-none d-md-flex" href="#" role="button" data-bs-toggle="dropdown"
          aria-expanded="false">
          Create
        </h5>
        <ul class="dropdown-menu">
          <li><a class="dropdown-item" href="#">new keep</a></li>
          <li>
            <hr class="dropdown-divider">
          </li>
          <li><a class="dropdown-item" href="#">new vault</a></li>
        </ul>


      </div>
    </div>

    <div class="d-flex flex-column align-items-center ">
      <h2 class="my-0 d-none d-md-flex fw-bold">keepr</h2>
      <div class="btn-group dropup-center dropup">

        <button class="nav-link fw-semibold d-md-none fs-5 btn" href="#" role="button" data-bs-toggle="dropdown"
          aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu">
          <li><a class="dropdown-item" href="#">new keep</a></li>
          <li>
            <hr class="dropdown-divider">
          </li>
          <li><a class="dropdown-item" href="#">new vault</a></li>
        </ul>

      </div>

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
    <Login class="ps-md-5 ms-md-5 me-md-2 " />
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

/* .nav-link {
  text-transform: uppercase;
} */

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
