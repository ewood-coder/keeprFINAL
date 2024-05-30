<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';

const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const isKept = computed(() => AppState.account?.id === keep.value?.kept)
const isCreator = computed(() => AppState.account?.id === keep.value?.creatorId)

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
// -----------------------------------------------------



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

							<div class="col-12 col-lg-6">
								<div class="fs-5 gap-3 fs-4 d-flex justify-content-center grayText px-2 pt-4 pb-2 py-md-4"
									id="keepModalLabel">
									<div><i class="mdi mdi-eye-outline"></i> {{ keep?.views }}</div>
									<div><i class="mdi mdi-heart-box-outline"></i> {{ keep?.kept }}</div>
								</div>


								<div class="px-4 px-lg-5 pt-1 pt-lg-4 mt-3">
									<div class="pt-lg-4">
										<div class="fw-bold fs-1 text-center py-3 my-auto markoOne">{{ keep?.name }}</div>

										<div class="lh-lg grayText pb-5 my-auto">{{ keep?.description }}</div>
									</div>

									<section class="pt-3 pb-3">
										<div class="d-flex align-items-center justify-content-between flex-wrap gap-3">
											<!-- <span v-if="isCreator" class="d-flex align-items-center gap-2 row">
												<button class="btn btn-danger col-2" data-bs-dismiss="modal"
													@click="destroyKeep">Delete</button>
											</span> -->
											<div class="pe-4">
												<img :src="keep?.creator.picture" alt="" class="profileImg">
												<span class="ps-2">{{ keep?.creator.name }}</span>
											</div>

											<div class="d-flex justify-content-between gap-2">
												<div v-if="account == null"></div>

												<div v-else>
													<div class="dropdown-center">
														<button
															class="btn hoverBG text-dark lighten-30 text-uppercase dropdown-toggle"
															type="button" data-bs-toggle="dropdown" aria-expanded="false">
															Save To Vault
														</button>
														<ul class="dropdown-menu dropdown-menu-dark">
															<li>
																<a class="dropdown-item" href="createKeepModal">Vault Name</a>
																<hr class="my-1" />
															</li>
														</ul>
													</div>
												</div>

												<button type="button" class="btn closeBtn" data-bs-dismiss="modal">Close</button>

											</div>
										</div>
									</section>
								</div>
							</div>
						</section>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>


<style scoped>
.hoverBG {
	border: solid 1px black;
}

.hoverBG:hover {
	border: solid 1px black;
	background-color: rgb(225, 225, 225);
}

.modalRadius {
	/* border-radius: 10px !important; */
}

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