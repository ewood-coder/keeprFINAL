<script setup>
import { computed, effect, onMounted, ref, watchEffect } from 'vue';
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { Profile } from '../models/Profile.js';
import { profilesService } from '../services/ProfilesService.js';
import KeepModal from './KeepModal.vue';
import { AppState } from '../AppState.js';
import { Vault } from '../models/Vault.js';


const account = computed(() => AppState.account)



const props = defineProps({
	keep: { type: Keep, required: true },
})


// STUB: FUNCTIONS: -----------------------------------

async function setActiveKeep() {
	console.log('setting active keep', props.keep)
	await keepsService.setActiveKeep(props.keep)
}

async function setActiveProfile() {
	console.log('setting active profile', props.keep.creator)
	await profilesService.setActiveProfile(props.keep.creator)
}

async function destroyKeep(keepId) {
	try {
		const res = await Pop.confirm('Are you sure you want to delete this keep?')
		if (!res) {
			return
		}
		await keepsService.destroyKeep(keepId)
		Pop.success('Keep Deleted')
	}
	catch (error) {
		Pop.error(error)
	}
}




// ---------------------------------------------------

</script>


<template>
	<div class="keep-card mask1 container1">

		<img :src="keep.img" :alt="keep.name" data-bs-toggle="modal" data-bs-target="#keepModal" role="button"
			@click="setActiveKeep()" :title="`Information about the keep: ${keep.name}`" class="keep-img rounded">

		<div class="bgColor bottom-left">
			<div class="fs-5 text-capitalize markoOne">{{ keep.name }}</div>
		</div>

		<RouterLink :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
			<img :src="keep.creator.picture" :alt="keep.creator.name" role="button"
				:title="`Profile: ${keep.creator.name}`" class="profile-img top-left">
		</RouterLink>
		<!-- @click="setActiveProfile()" -->


		<button v-if="keep.creatorId == account?.id" @click="destroyKeep(keep.id)" class="btnDelete top-right"
			:title="`Delete Keep`">
			<i class="mdi mdi-trash-can-outline"></i>
		</button>

	</div>
</template>


<style scoped>
.btnDelete {
	color: white;
	background-color: #e20000;
	border: solid 1px #e20000;
	border-radius: 15px;
	transition: 0.4s ease-in-out;
}

.btnDelete:hover {
	color: #ffffff;
	background-color: #ff0000;
	border: solid 1px #ff0000;
	border-radius: 5px;
	transition: 0.4s ease-in-out;
}


.keep-card {
	margin-bottom: 20px;
}

/* .keep-card {
	background-color: #fdac98;

	color: white;

	width: auto;
	height: 100%;

	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
} */

.keep-img {
	width: 100%;
	height: auto;
	object-fit: cover;
	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
}

.profile-img {
	width: 50px;
	height: 50px;

	border-radius: 9999px;
	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;

	background-color: white;
	padding: 2px;
}

@media screen and (max-width: 426px) {
	.profile-img {
		width: 35px;
		height: 35px;

		border-radius: 9999px;
		box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;

		background-color: white;
		padding: 2px;
	}
}

.filterBox {
	background-color: rgba(165, 165, 165, 0.5);
	backdrop-filter: blur(15px);
	border-radius: 15px;

	display: flex;
	justify-content: center;
	align-items: center;
}

.floatText {
	position: absolute;
}

.mask1 {
	text-shadow: 1px 1px 1px rgb(0, 0, 0);
}


/* SECTION: TEXT POSITIONS */

.bottom-left {
	position: absolute;
	bottom: 0px;
	left: 10px;
	/* width: 100%; */
}

@media screen and (max-width: 375px) {
	.bottom-left {
		position: absolute;
		bottom: 0px;
		left: 0px;
		width: 100%;
	}
}

.top-left {
	position: absolute;
	top: 5px;
	left: 5px;
}

.top-right {
	position: absolute;
	top: 5px;
	right: 5px;
}

.bottom-right {
	position: absolute;
	bottom: 8px;
	right: 16px;
}

.centered {
	position: absolute;
	top: 50%;
	left: 50%;
	transform: translate(-50%, -50%);
}

.container1 {
	position: relative;
	text-align: center;
	color: white;
}


/* ----------------------------------- */
</style>