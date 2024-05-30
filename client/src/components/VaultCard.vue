<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';
import { AppState } from '../AppState.js';


const account = computed(() => AppState.account)
const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)
const profiles = computed(() => AppState.profiles)


const props = defineProps({
	vault: { type: Vault, required: true },
})


// STUB: FUNCTIONS: -----------------------------------

async function setActiveVault() {
	console.log('setting active vault', props.vault)
	await vaultsService.setActiveVault(props.vault)
}

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
	<div class="vault-card rounded-4 mask1">

		<img :src="vault.img" :alt="vault.name" role="button" @click="setActiveVault()"
			:title="`image of vault with name: ${vault.name}`" class="vault-img">

		<div class="px-1 px-md-4 py-2 bgColor d-flex align-items-center flex-wrap gap-3">
			<div class="px-2 py-1 fs-5 text-capitalize quando">{{ vault.name }}</div>


			<div v-if="vault.isPrivate == true && vault.creatorId == account?.id">
				<span class="lockBG p-2"><i class="mdi mdi-lock fs-5 mx-auto"></i></span>
			</div>


			<button v-if="vault.creatorId == account?.id" @click="destroyVault(vault.id)" class="btnDelete p-1"
				:title="`Delete Vault`">
				<i class="mdi mdi-trash-can-outline fs-4 px-1 px-md-2"></i>
			</button>
		</div>

	</div>
</template>


<style scoped>
.btnDelete {
	color: white;
	background-color: #bf0101;
	border: solid 1px #bf0101;
	border-radius: 9999px;
	transition: 0.4s ease-in-out;
}

.btnDelete:hover {
	color: #ffffff;
	background-color: #e20000;
	border: solid 1px #bf0101;
	border-radius: 9999px;
	transition: 0.4s ease-in-out;
}

.lockBG {
	color: black;
	background-color: white;

	border-radius: 9999px;
}

.vault-card {
	/* background-color: #ff9074; */
	background-color: #fdac98;

	color: white;

	width: auto;
	height: 100%;

	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	/* box-shadow: rgba(50, 50, 93, 0.25) 0px 2px 5px -1px, rgba(0, 0, 0, 0.3) 0px 1px 3px -1px; */
}

.vault-img {
	width: 100%;
	height: 40vh;
	object-fit: cover;
}

.fontSize {
	font-size: 20px;
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
</style>