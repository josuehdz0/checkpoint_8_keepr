<template>
  <nav
    class="navbar navbar-expand-lg navbar-light bg-light px-3 smallshadow d-flex justify-content-between py-0 fixed-bottom sticky-md-top">
    <div class="navbar-brand d-flex ">
      <div class="d-flex align-items-center ">
        <router-link :to="{ name: 'Home' }">
          <h5 class="my-0 ps-1 d-none d-md-flex">Home</h5>
          <h1 class="mdi mdi-home d-md-none text-dark"></h1>
        </router-link>
        <div class=" btn-group dropend">


          <h5 v-if="account.id" class="nav-link  my-0  px-3 fw-semibold d-none d-md-flex" href="#" role="button"
            data-bs-toggle="dropdown" aria-expanded="false">
            Create
          </h5>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#keepForm">new keep</a></li>
            <li>
              <hr class="dropdown-divider">
            </li>
            <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#vaultForm">new vault</a></li>
          </ul>

        </div>


      </div>
    </div>

    <div class="d-flex flex-column align-items-center ">
      <h2 class="my-0 d-none d-md-flex fw-bold">keepr</h2>
      <div class="btn-group dropup-center dropup">

        <button v-if="account.id" class="nav-link fw-semibold d-md-none fs-5 btn" href="#" role="button"
          data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu">
          <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#keepForm">new keep</a></li>
          <li>
            <hr class="dropdown-divider">
          </li>
          <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#vaultForm">new vault</a></li>
        </ul>

      </div>

    </div>

    <!-- LOGIN COMPONENT HERE -->
    <Login class="ps-md-5 ms-md-5 me-md-2 " />
  </nav>

  <Modal id="keepForm">
    <KeepForm />
  </Modal>

  <Modal id="vaultForm">
    <VaultForm />
  </Modal>
</template>

<script>
import Login from './Login.vue'
import KeepCard from "./KeepCard.vue";
import KeepForm from "./KeepForm.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import VaultForm from "./VaultForm.vue";

export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { Login, KeepForm, VaultForm },

}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.smallshadow {
  box-shadow: 0rem 1.125rem 3.25rem rgba(0, 0, 0, 0.553) !important;
}

/* .nav-link {
  text-transform: uppercase;
} */

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

nav {
  height: 80px;
}



@media screen and (min-width: 768px) {
  nav {
    height: 85px;
  }

  .smallshadow {
    box-shadow: 0rem 0.125rem 0.25rem rgba(0, 0, 0, 0.075) !important;
  }
}
</style>
