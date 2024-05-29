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
		<div class="modal-dialog modal-xl">
			<div class="modal-content">

				<div class="modal-header">
					<h1 class="modal-title fs-5 my-auto text-capitalize" id="createKeepModalLabel">
						{{ keep?.name }} &nbsp;>&nbsp; <i class="mdi mdi-eye-outline"></i> {{ keep?.views }} | <i
							class="mdi mdi-heart-box-outline"></i> {{ keep?.kept }}
					</h1>

					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>

				<div class="modal-body">
					<form @submit.prevent="createKeep()">

						<div class="form-floating mb-3">
							<textarea v-model="editableKeepData.name" type="String" class="form-control rows" id="keepName"
								placeholder="Keep Name" required maxlength="15">
							</textarea>
							<label for="keepName">Keep Name...</label>
						</div>

						<div class="form-floating mb-3">
							<input v-model="editableKeepData.img" type="url" class="form-control" id="keepUrl"
								placeholder="Keep Thumbnail" maxlength="500">
							<label for="keepUrl">Insert Image Address...</label>
						</div>

						<div class="form-floating mb-3">
							<textarea v-model="editableKeepData.description" type="String" class="form-control rows"
								id="keepDescription" placeholder="Keep Description" required maxlength="1000">
							</textarea>
							<label for="keepDescription">Description...</label>
						</div>

						<div class="text-end">
							<button class="fs-4 text-light sendBtn rounded p-2 px-3" type="submit">
								<div>Create <i class="mdi mdi-plus"></i></div>
							</button>
						</div>
					</form>
				</div>

				<div class="modal-footer justify-content-between">
					<div class="d-flex gap-2 align-items-center">

						<img :src="keep?.creator.picture" alt="" class="profileImg">
						<span class="mb-3">
							{{ keep?.creator.name }}
						</span>

					</div>


					<div class="d-flex">

						<span class="pe-4">
							<div v-if="account == null">
								<i class="mdi mdi-heart-off-outline fs-1 rounded-4 btnLikeOff px-2" title="Login Required"></i>
							</div>

							<div v-else>
								<button v-if="!isFavorited" @click="FavoriteRecipe(keep?.id)" class="btnLike rounded-4"
									:title="`Favorite this Recipe`">
									<i class="mdi mdi-heart-outline fs-1"></i>
								</button>
								<button v-if="isFavorited" @click="RemoveFavoriteRecipe(isFavorited.favoriteId)"
									class="btnAlreadyLiked rounded-4" :title="`Unfavorite this Recipe`">
									<i class="mdi mdi-heart fs-1"></i>
								</button>
							</div>
						</span>

						<!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> -->
					</div>

				</div>
			</div>
		</div>
	</div>
</template>


<style scoped>
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