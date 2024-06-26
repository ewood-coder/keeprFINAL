<script setup>
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop.js';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService.js';
import { Vault } from '../models/Vault.js';
import VaultCard from '../components/VaultCard.vue';
import { vaultsService } from '../services/VaultsService.js';
import { profilesService } from '../services/ProfilesService.js';
import { Account } from '../models/Account.js';
import { accountService } from '../services/AccountService.js';
import { VaultKeep } from '../models/VaultKeep.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import { router } from '../router.js';


const account = computed(() => AppState.account)
const vault = computed(() => AppState.activeVault)
const vaultKeeps = computed(() => AppState.vaultKeeps)

const route = useRoute()


// STUB: FUNCTIONS: -----------------------------------

async function getVaultKeeps() {
	try {
		await vaultKeepsService.getVaultKeeps(route.params.vaultId)
	} catch (error) {
		Pop.toast("Could not get vault's keeps", 'error')
		console.error(error)
	}
}

async function getVaultById() {
	try {
		await vaultsService.getVaultById(route.params.vaultId)
	} catch (error) {
		Pop.error(error)
		router.push({ name: 'Home' })
	}
}

// ---------------------------------------------------


onMounted(() => {
	getVaultKeeps()
	getVaultById()
	// vaultsService.setActiveVault(route.params.vaultId)
})

</script>


<template>
	<main>

		<div class="container-fluid mt-2">
			<!-- NOTE v-if keeps the profile from trying to draw, before the network response is back -->
			<section class="row justify-content-center" v-if="vault">

				<div v-if="vault?.img" class="d-flex justify-content-center container1">
					<img class="cover-img" :src="vault.img" :alt="`thumbnail of the ${vault.name} vault`">

					<div class="mt-2 fs-1 fw-bold markoOne mask1 text-white centered1">{{ vault.name }}</div>
					<div class="mb-2 fs-4 mask1 text-white centered2">by: {{ vault.creator.name }}</div>
				</div>

				<div v-else class="d-flex justify-content-center container1">
					<img class="cover-img"
						src="https://preview.redd.it/art-mountains-oceans-cover-art-v0-jpj8uj9fzkfa1.jpg?auto=webp&s=beae04b18d2ef2164ae9bf334215f5c91d838262"
						alt="cover image placeholder of mountains">

					<div class="mt-2 fs-2 fw-bold markoOne mask1 text-white centered1">{{ vault.name }}</div>
					<div class="mb-2 fs-5 mask1 text-white centered2">by: {{ vault.creator.name }}</div>
				</div>

				<div class="col-4 col-md-2 col-xl-1 pt-3">
					<!-- <div class="mask1 text-white centered">
						<div class="mt-2 fs-2 fw-bold markoOne">{{ vault.name }}</div>
						<div class="mb-2 fs-5">by: {{ vault.creator.name }}</div>
					</div> -->

					<div v-if="vaultKeeps.length == 1">
						<div class="fs-5 mb-3 filterBox fw-semibold">{{ vaultKeeps.length }} Keep</div>
					</div>
					<div v-else>
						<div class="fs-5 mb-3 filterBox fw-semibold">{{ vaultKeeps.length }} Keeps</div>
					</div>
				</div>
			</section>
		</div>

		<div class="container-fluid my-5 px-md-4 px-lg-5">

			<section class="row justify-content-center">
				<hr class="pb-4" />

				<div v-if="vaultKeeps.length == 1" class="px-1 px-md-0 row justify-content-center justify-content-md-start">
					<div v-for="keep in vaultKeeps" :key="keep.id" class="col-10 col-md-4 col-lg-3 mb-5">
						<KeepCard :keep="keep" />
					</div>
				</div>

				<div v-else class="row justify-content-center">
					<div class="keeps">
						<KeepCard :keep="keep" v-for="keep in vaultKeeps" :key="keep.id" />
					</div>
				</div>
			</section>

		</div>

	</main>
</template>


<style lang="scss" scoped>
.mask1 {
	text-shadow: 2px 2px 2px rgb(0, 0, 0);
}

.filterBox {
	background-color: #dad6ff;
	backdrop-filter: blur(15px);
	border-radius: 12px;

	display: flex;
	justify-content: center;
	align-items: center;
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

.centered1 {
	position: absolute;
	top: 65%;
	left: 50%;
	transform: translate(-50%, -50%);
}

.centered2 {
	position: absolute;
	top: 88%;
	left: 50%;
	transform: translate(-50%, -50%);
}

.container1 {
	position: relative;
	text-align: center;
	color: white;
}

/* ----------------------------------- */

.cover-img {
	width: 100%;
	height: auto;
	max-width: 30em;
	max-height: 12em;


	// margin-bottom: -10vh;
	object-fit: cover;
	object-position: center;
	border-radius: 10px;
	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
}

.profile-img {
	height: 100px;
	width: 100px;
	object-fit: cover;
	object-position: center;
}

.link {
	color: var(--bgLightBlue);
	transition: 0.4s;
}

.link:hover {
	color: var(--bgGreen);
}

.roundedImg {
	border-radius: 9999px;
	background-color: rgb(255, 255, 255);
	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
}

span>img {
	background: linear-gradient(-45deg, #ee7752, #e73c7e, #23a6d5, #23d5ab);
	background-size: 400% 400%;
	animation: gradient 10s ease infinite;
	height: 100vh;
}

@keyframes gradient {
	0% {
		background-position: 0% 50%;
	}

	50% {
		background-position: 100% 50%;
	}

	100% {
		background-position: 0% 50%;
	}
}



.keeps {
	column-count: 4;
	column-gap: 1rem;
	row-gap: 2rem;
}

@media (max-width: 1200px) {
	.keeps {
		column-count: 3;
	}
}

@media (max-width: 992px) {
	.keeps {
		column-count: 2;
	}
}
</style>
