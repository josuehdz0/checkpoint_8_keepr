<template>
  <div class="component">
    <div class="container-fluid">
      <div class="row p-2 justify-content-between">
        <div class="col-8 ps-3 mt-2 pt-3">
          <h1>Add your Vault</h1>
        </div>
        <div class="col-1 text-center d-flex  justify-content-center p-0 p-md-2">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
      </div>
      <div class="row my-4">

        <form @submit.prevent="createVault()">
          <div class="mb-3 p-1">
            <label class="form-label"></label>
            <input v-model="editable.name" required type="text" class="form-control" id="name" aria-describedby="name"
              placeholder="Title...">
          </div>
          <div class="mb-3 p-1">
            <label class="form-label"></label>
            <input v-model="editable.img" required type="url" class="form-control" id="image" aria-describedby="image"
              placeholder="Image URL...">
          </div>
          <div class="mb-3 p-1">
            <label for="description" class="form-label "></label>
            <textarea v-model="editable.description" placeholder="Description..." required class="form-control"
              id="description" rows="2"></textarea>
          </div>
          <div class="mb-3 form-check">
            <input v-model="editable.isPrivate" type="checkbox" class="form-check-input" id="isPrivate" name="isPrivate"
              :checked="false">
            <label class="form-check-label" for="exampleCheck1">Make Vault Private?</label>
            <div id="isPrivate" class="form-text">Private Vaults can only be seen by you.</div>

          </div>

          <button type="submit" class="btn btn-primary m-3" data-bs-dismiss="modal">Create Vault</button>
        </form>


      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue";
import { vaultsService } from "../services/VaulltsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          if (this.editable.isPrivate === null || this.editable.isPrivate === undefined) {
            this.editable.isPrivate = false;
          }
          const formData = editable.value
          const vault = await vaultsService.createVault(formData)
          editable.value = {}
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>