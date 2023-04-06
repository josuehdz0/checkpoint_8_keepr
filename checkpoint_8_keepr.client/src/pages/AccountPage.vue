<template>
  <div class="container-fluid">
    <div class="row mt-4">
      <div class="about text-center">
        <h1 class="pb-2">Welcome {{ account.name }}</h1>
        <img class="p-0 profile-picture" :src="account.picture" alt="" />
        <p class="pt-3">{{ account.email }}</p>
      </div>
    </div>

    <div class="row justify-content-center pb-3">
      <div class="col-md-7 col-10">

        <div>
          <form @submit.prevent="handleSubmit">
            <!-- NOTE name -->
            <div class="mb-3">
              <label for="name" class="form-label">name</label>
              <input required type="text" v-model="editable.name" class="form-control" id="name"
                :placeholder="account.name" name="name">
            </div>
            <!-- NOTE image -->
            <div class="mb-3">
              <label for="picture" class="form-label">picture</label>
              <input type="text" v-model="editable.picture" class="form-control" id="picture"
                :placeholder="account.picture" name="picture">
            </div>
            <!-- NOTE coverimage -->
            <div class="mb-3">
              <label for="coverImg" class="form-label">coverImg</label>
              <input type="text" v-model="editable.coverImg" class="form-control" id="coverImg"
                :placeholder="account.coverImg" name="coverImg">
            </div>


            <div>
              <button type="submit" class="btn btn-primary">
                {{ editable.profile }} Save changes
              </button>
            </div>
          </form>


        </div>

      </div>
    </div>


  </div>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js";
import { useRouter } from "vue-router";
export default {
  setup() {
    const editable = ref({})
    const router = useRouter()

    return {
      editable,
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),

      async handleSubmit() {
        try {
          await profilesService.editProfile(editable.value)
          editable.value = {}
          router.push({ name: 'Profile', params: { profileId: this.account.id } })
        } catch (error) {
          Pop.error(error, "Editing Profile")
        }
      }
    }
  }
}
</script>

<style scoped>
.profile-picture {
  height: 16vh;
  width: 16vh;
  border: 1px solid whitesmoke;
  border-radius: 50%;
  object-fit: cover;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.363);
}
</style>
