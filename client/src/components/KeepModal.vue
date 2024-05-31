<script setup>
import { computed, effect, onMounted, ref, watchEffect } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';


const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const profileVaults = computed(() => AppState.profileVaults)
const myVaults = computed(() => AppState.myVaults)
const isKept = computed(() => isCreatorOfVault.value && keep.value?.vaultKeepId)
const isCreator = computed(() => AppState.account?.id === keep.value?.creatorId)
const isCreatorOfVault = computed(() => AppState.account?.id === AppState.activeVault?.creatorId)

const route = useRoute()


// STUB: FUNCTIONS: -----------------------------------

async function destroyKeep() {
	try {
		const res = await Pop.confirm('Are you sure you want to delete this keep?')
		if (!res) {
			return
		}
		await keepsService.destroyKeep(AppState.activeKeep.id)
		Pop.success('Keep Deleted 🗑️')
	}
	catch (error) {
		Pop.error(error)
	}
}

let selectedVaultId = ref('')
async function saveToVault(event) {
	const vaultId = event.target.value
	if (vaultId == '') return
	try {
		await vaultKeepsService.addKeepToVault(AppState.activeKeep.id, vaultId)
		Pop.success('Keep Saved to Vault 📥')
		AppState.activeKeep.kept++
		selectedVaultId.value = ''

	}
	catch (error) {
		Pop.error(error)
	}
}

async function removeFromVault() {
	try {
		await vaultKeepsService.removeKeepFromVault(keep.value.vaultKeepId)
		Pop.success('Keep Removed from Vault 📤')
		AppState.activeKeep.kept--
	}
	catch (error) {
		Pop.error(error)
	}
}


</script>


<template>
	<div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered modalRadius modal-xl">
			<div class="modal-content">

				<div class="modal-body p-0">
					<div class="container-fluid">
						<section class="row">

							<div class="col-12 col-lg-6">
								<img :src="keep?.img" :alt="`image of ${keep?.name}`" class="modalImg">
							</div>

							<div class="col-12 col-lg-6 outer d-flex flex-column justify-content-between ">
								<div class="fs-5 gap-3 grayText px-2 pt-4 " id="keepModalLabel">
									<div class="d-flex justify-content-center fs-5 gap-3">
										<i class="mdi mdi-eye-outline">&nbsp;{{ keep?.views }}</i>
										<i class="mdi mdi-heart-box-outline">&nbsp;{{ keep?.kept }}</i>
									</div>
								</div>


								<div class="px-5 ">
									<div class="">
										<div class="fw-bold fs-1 text-center py-3 my-auto markoOne">
											{{ keep?.name }}
										</div>

										<div class="lh-lg grayText pb-5 my-auto txtCenter">
											<div class="txtLeft">
												{{ keep?.description }}
											</div>
										</div>
									</div>

								</div>
								<section class="pt-3 pb-3 inner">
									<div class="d-flex align-items-center justify-content-between flex-wrap gap-3 px-3">
										<!-- <span v-if="isCreator" class="d-flex align-items-center gap-2 row">
											<button class="btn btn-danger col-2" data-bs-dismiss="modal"
												@click="destroyKeep">Delete</button>
										</span> -->
										<div class="" v-if="keep">
											<RouterLink :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
												<img :src="keep?.creator.picture" :alt="keep.creator.name" role="button"
													:title="`Profile: ${keep.creator.name}`" data-bs-dismiss="modal"
													class="profileImg">
											</RouterLink>
											<span class="ps-2">{{ keep?.creator.name }}</span>
										</div>

										<div class="d-flex justify-content-between gap-2 ">
											<div v-if="account == null"></div>

											<div v-if="account">

												<div v-if="!isKept">

													<select class="form-select text-dark text-uppercase"
														aria-label="Default select example" @change="saveToVault"
														v-model="selectedVaultId">
														<option selected disabled value="">Save To Vault</option>
														<option v-for="vault in myVaults" :key="vault.id" class="text-capitalize"
															:value="`${vault.id}`">
															{{ vault.name }}
														</option>
													</select>

												</div>


												<div v-else>
													<button class="btn btn-danger me-3" data-bs-dismiss="modal"
														@click="removeFromVault" type="button">Remove from vault</button>
												</div>

											</div>

											<button type="button" class="btn closeBtn" data-bs-dismiss="modal">Close</button>

										</div>
									</div>
								</section>
							</div>
						</section>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>


<style scoped>
.txtCenter {
	text-align: center;
}

.txtLeft {
	display: inline-block;
	text-align: left;
}

/* .outer { */
/* position: relative; */
/* padding-bottom: 80px; */
/* } */

/* .inner {
	position: absolute;
	height: 80px;
	width: 90%;

	left: 2em;
	right: 0;
	bottom: 0;
} */

/* @media screen and (max-width: 1199px) { */
/* .outer {
		position: relative;
		padding-bottom: 130px;
	} */

/* .inner {
		position: absolute;
		height: 80px;
		width: 90%; */
/* padding-left: 10px;
	padding-right: 10px; */
/* left: 2em;
		right: 0;
		bottom: 50px;
	} */
/* } */

/* 
@media screen and (max-width: 374px) {

	.inner {
		position: absolute;
		height: 80px;
		width: 90%;
		/* padding-left: 10px;
	padding-right: 10px; */
/* left: 1em;
right: 0;
bottom: 50px;
}
} */

.modalImg {
	border-top-left-radius: 5px;
	border-bottom-left-radius: 5px;

	width: 100%;
	height: 100%;
	/* max-width: 35em; */
	max-height: 45em;

	object-fit: cover;
}

@media screen and (max-width: 991px) {
	.modalImg {
		border-top-left-radius: 0px;
		border-bottom-left-radius: 0px;

		width: 100%;
		height: 100%;
		/* max-width: 35em; */
		/* max-height: 35em; */

		object-fit: cover;
	}
}


.row>* {
	padding-left: 0px !important;
	padding-right: 0px !important;
}

.closeBtn {
	color: white;
	background-color: rgb(130, 130, 255);
}

.closeBtn:hover {
	color: white;
	background-color: rgb(94, 94, 255);
}

img {
	width: 100%;
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