<script setup>
import { computed, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';

const vault = computed(() => AppState.activeVault)
const account = computed(() => AppState.account)
const isPrivate = computed(() => AppState.account?.id === vault.value?.isPrivate)


const editableVaultData = ref({
	name: '',
	description: '',
	img: '',
	isPrivate: false,
})

// STUB: FUNCTIONS: -----------------------------------

async function createVault() {
	try {
		logger.log('Creating vault! 📦', editableVaultData)
		// NOTE .value pulls out the data stored inside of the ref object (whatever is inside the parentheses)

		await vaultsService.createVault(editableVaultData.value)

		// NOTE form.reset()
		editableVaultData.value = {
			name: '',
			description: '',
			img: '',
			isPrivate: false,
		}

		// NOTE closes Modal after submit
		Modal.getOrCreateInstance('#createVaultModal').hide()
	}
	catch (error) {
		Pop.error(error);
	}
}
// -----------------------------------------------------
</script>


<template>
	<div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel"
		aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered modal-xl">
			<div class="modal-content">

				<div class="modal-header">
					<div class="fs-1 markoOne" id="createVaultModalLabel">Add Your Vault</div>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>

				<div class="modal-body">
					<form @submit.prevent="createVault()">

						<div class="form-floating mb-4">
							<input v-model="editableVaultData.name" type="String" class="form-control" id="vaultName"
								placeholder="Vault Name" required maxlength="13">
							<label for="vaultName">Vault Title...</label>
						</div>

						<div class="form-floating mb-4">
							<input v-model="editableVaultData.img" type="url" class="form-control" id="vaultUrl"
								placeholder="Vault Thumbnail" required maxlength="500">
							<label for="vaultUrl">Insert Image Address...</label>
						</div>

						<!-- <div class="form-floating mb-4">
							<textarea v-model="editableVaultData.description" type="String" class="form-control rows"
								id="vaultDescription" placeholder="Vault Description" required maxlength="1000">
							</textarea>
							<label for="vaultDescription">Description...</label>
						</div> -->

						<div class="grayText d-flex justify-content-center align-items-center">
							Private vaults can only be seen by you
						</div>

						<div class="form-floating mb-4">
							<div class="form-check d-flex justify-content-center align-items-center gap-3 fs-5">
								<input class="form-check-input" v-model="editableVaultData.isPrivate" type="checkbox"
									id="vaultPrivateCheck">
								<label class="form-check-label fw-bold" for="vaultPrivateCheck">
									Make Vault Private?
								</label>
							</div>
						</div>

						<div class="text-end">
							<button class="fs-4 text-light createBtn rounded p-1 px-3" role="button" type="submit">
								<div>Create <i class="mdi mdi-plus"></i></div>
							</button>
						</div>
					</form>
				</div>

			</div>
		</div>
	</div>
</template>


<style scoped>
/* @import "/src/assets/scss/modalAnimation.scss"; */

.modal-content {
	background-color: rgb(235, 235, 235);
}

.rows {
	min-height: 50vh;
}
</style>