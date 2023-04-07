import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /** @type {import('./models/Profile.js').Profile| Null} */
  profile: null,

  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],

  /** @type {import('./models/Keep.js').Keep | Null} */
  keep: null,

  /** @type {import('./models/Vault.js').Vault | Null} */
  vault: null,

  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],

  /** @type {import('./models/Vault.js').Vault[]} */
  myVaults: [],

  /** @type {import('./models/VaultKeep.js').VaultKeep[]} */
  vaultKeeps: [],


})
