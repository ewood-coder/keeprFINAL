import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from '../models/Vault.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

	async updateAccount(accountData) {
		const response = await api.put('/account', accountData)
		console.log('updated account successfully 👍', response.data);
		AppState.account = new Account(response.data)
	}

	async getMyVaults() {
		AppState.myVaults = []
      const response = await api.get('/account/vaults')
		console.log('GOT ONLY MY VAULTS 📦✅', response.data)
		const vaults = response.data.map(vault => new Vault(vault))

      AppState.myVaults = vaults
	}
}

export const accountService = new AccountService()
