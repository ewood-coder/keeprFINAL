<script setup>
import { computed, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';

const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const isKept = computed(() => AppState.account?.id === keep.value?.kept)


const editableKeepData = ref({
	name: '',
	description: '',
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
					<div class="fs-1 markoOne" id="createKeepModalLabel">Add Your Keep</div>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>

				<div class="modal-body">
					<form @submit.prevent="createKeep()">

						<div class="form-floating mb-4">
							<input v-model="editableKeepData.name" type="String" class="form-control" id="keepName"
								placeholder="Keep Name" required maxlength="13">
							<label for="keepName">Keep Title...</label>
						</div>

						<div class="form-floating mb-4">
							<input v-model="editableKeepData.img" type="url" class="form-control" id="keepUrl"
								placeholder="Keep Thumbnail" required maxlength="500">
							<label for="keepUrl">Insert Image Address...</label>
						</div>

						<div class="form-floating mb-4">
							<textarea v-model="editableKeepData.description" type="String" class="form-control rows"
								id="keepDescription" placeholder="Keep Description" maxlength="1000">
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
	background-color: rgb(235, 235, 235);
}

.rows {
	min-height: 50vh;
}
</style>