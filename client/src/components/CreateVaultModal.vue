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


const editableKeepData = ref({
	name: '',
	img: '',
})

// STUB: FUNCTIONS: -----------------------------------

async function createKeep() {
	try {
		logger.log('Creating keep! 🖼️', editableKeepData)
		// NOTE .value pulls out the data stored inside of the ref object (whatever is inside the parentheses)

		await keepsService.createKeep(editableKeepData.value)

		// NOTE form.reset()
		editableKeepData.value = {
			name: '',
			description: '',
			img: '',
		}

		// NOTE closes Modal after submit
		Modal.getOrCreateInstance('#createKeepModal').hide()
	}
	catch (error) {
		Pop.error(error);
	}
}
// -----------------------------------------------------



</script>


<template>
	<div class="modal fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered modal-xl">
			<div class="modal-content">

				<div class="modal-header">
					<div class="fs-1 markoOne" id="postFormModalLabel">Add Your Keep</div>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>

				<div class="modal-body">
					<form @submit.prevent="createKeep()">

						<div class="form-floating mb-4">
							<input v-model="editableKeepData.name" type="String" class="form-control" id="keepName"
								placeholder="Keep Name" required maxlength="15">
							<label for="keepName">Keep Title...</label>
						</div>

						<div class="form-floating mb-4">
							<input v-model="editableKeepData.img" type="url" class="form-control" id="keepUrl"
								placeholder="Keep Thumbnail" maxlength="500">
							<label for="keepUrl">Insert Image Address...</label>
						</div>

						<div class="form-floating mb-4">
							<textarea v-model="editableKeepData.description" type="String" class="form-control rows"
								id="keepDescription" placeholder="Keep Description" required maxlength="1000">
							</textarea>
							<label for="keepDescription">Description...</label>
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
	background-color: rgb(216, 215, 215);
}

.rows {
	min-height: 50vh;
}

img {
	width: 100%;
	margin-bottom: 1em;
}

.profileImg {
	width: 50px;
	height: 50px;
	border-radius: 50%;
}

.btnLike {
	color: rgb(0, 0, 0);
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnLike:hover {
	transition: all 0.3s ease-in-out;
	color: #ff3f92;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnAlreadyLiked {
	/* color: #e44b7e; */
	/* color: #0084ff; */
	color: #ff3f92;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnLikeOff {
	color: white;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}
</style>