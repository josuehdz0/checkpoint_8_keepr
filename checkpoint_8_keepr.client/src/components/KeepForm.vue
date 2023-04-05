<template>
  <div class="component">
    <div class="container-fluid">
      <div class="row p-2 justify-content-between">
        <div class="col-8 ps-3 mt-2 pt-3">
          <h1>Add your Keep</h1>
        </div>
        <div class="col-1 text-center d-flex  justify-content-center p-0 p-md-2">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
      </div>
      <div class="row my-4">

        <form @submit.prevent="createKeep()">
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
          <button type="submit" class="btn btn-primary m-3" data-bs-dismiss="modal">Create Keep</button>
        </form>


      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          const formData = editable.value
          const keep = await keepsService.createKeep(formData)
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