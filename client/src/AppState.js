import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,


  /** @type {import('./models/Profile.js').Profile[]} profiles from the database */
  profiles: [],
  useProfile: false,
  /** @type {import('./models/Profile.js').Profile | null} single profile from the database*/
  activeProfile: null,
  /** @type {import('./models/Keep.js').Keep[]}  */
  profileKeeps: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  profileVaults: [],


  /** @type {import('./models/Keep.js').Keep[]} keeps from the database*/
  keeps: [],
  /** @type {import('./models/Keep.js').Keep | null} single keep from the database*/
  activeKeep: null,


  /** @type {import('./models/Vault.js').Vault[]} vaults from the database*/
  vaults: [],
  /** @type {import('./models/Vault.js').Vault | null} single vault from the database*/
  activeVault: null,



  /** @type {import('./models/VaultKeep.js').VaultKeep[]} vaultKeeps from the database*/
  vaultKeeps: [],
  /** @type {import('./models/VaultKeep.js').VaultKeep | null} single vaultKeep from the database*/
  activeVaultKeep: null,
})