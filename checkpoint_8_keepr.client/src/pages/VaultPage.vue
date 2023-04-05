<template>
  <div v-if="vault" class="containter-fluid">
    Vault page
  </div>
</template>


<script>
import { onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaulltsService.js";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(() => {
      getActiveVault()
    })

    async function getActiveVault() {
      try {
        await vaultsService.setActiveVault(route.params.vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
        router.push({ name: 'Home' })
      }
    }
    return {
      vault: computed(() => AppState.vault),
      account: computed(() => AppState.account),
    }
  }
}
</script>


<style lang="scss" scoped></style>