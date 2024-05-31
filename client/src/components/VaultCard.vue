<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';
import { AppState } from '../AppState.js';
import { RouterLink } from 'vue-router';


const account = computed(() => AppState.account)
const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)
const profiles = computed(() => AppState.profiles)


const props = defineProps({
	vault: { type: Vault, required: true },
})


// STUB: FUNCTIONS: -----------------------------------

async function destroyVault(vaultId) {
	try {
		const res = await Pop.confirm('Are you sure you want to delete this vault?')
		if (!res) {
			return
		}
		await vaultsService.destroyVault(vaultId)
		Pop.success('Vault Deleted')
	}
	catch (error) {
		Pop.error(error)
	}
}

// ---------------------------------------------------

</script>


<template>
	<div class="vault-card rounded-4 mask1 container1">

		<router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
			<img :src="vault.img" :alt="vault.name" role="button" :title="`image of vault with name: ${vault.name}`"
				class="vault-img rounded">
		</router-link>

		<div class="bottom-left">
			<div class="px-2 py-1 fs-5 text-capitalize quando txtShadow">{{ vault.name }}</div>
		</div>


		<div v-if="vault.isPrivate == true && vault.creatorId == account?.id">
			<span class="lockBG px-1 py-0 top-left"><i class="mdi mdi-lock mx-auto"></i></span>
		</div>


		<button v-if="vault.creatorId == account?.id" @click="destroyVault(vault.id)" class="btnDelete top-right"
			:title="`Delete Vault`">
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

.lockBG {
	color: white;
	background-color: #000000;

	border-radius: 9999px;
}

.vault-card {
	margin-bottom: 20px;
}

.vault-img {
	width: 100%;
	height: auto;
	/* max-height: 40vh; */
	object-fit: cover;
	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
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
	text-shadow: 2px 2px 2px rgb(0, 0, 0);
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