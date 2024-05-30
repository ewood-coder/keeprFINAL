<script setup>
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop.js';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService.js';
import VaultCard from '../components/VaultCard.vue';
import { vaultsService } from '../services/VaultsService.js';
import { profilesService } from '../services/ProfilesService.js';
import { Account } from '../models/Account.js';
import { accountService } from '../services/AccountService.js';


const account = computed(() => AppState.account)
const profile = computed(() => AppState.activeProfile)
const profileKeeps = computed(() => AppState.profileKeeps)
const profileVaults = computed(() => AppState.profileVaults)

const route = useRoute()


async function getProfile() {
	try {
		await profilesService.getProfile(route.params.profileId)
	} catch (error) {
		Pop.toast("could not get profile", 'error'),
			console.error(error)
	}
}

async function getProfileKeeps() {
	try {
		await keepsService.getProfileKeeps(route.params.profileId)
	} catch (error) {
		Pop.toast("Could not get profile's keeps", 'error')
		console.error(error)
	}
}

async function getProfileVaults() {
	try {
		await vaultsService.getProfileVaults(route.params.profileId)
	} catch (error) {
		Pop.toast("Could not get profile vaults", 'error')
		console.error(error)
	}
}

onMounted(() => {
	// AppState.useProfile = true
	getProfile()
	getProfileKeeps()
	getProfileVaults()
})

</script>


<template>
	<main>

		<div class="container-fluid mt-2">
			<!-- NOTE v-if keeps the profile from trying to draw, before the network response is back -->
			<section class="row" v-if="profile">

				<div v-if="profile.coverImg">
					<img class="cover-img" :src="profile.coverImg" :alt="`picture of ${profile.name}'s cover image`">
				</div>

				<div v-else>
					<img class="cover-img"
						src="https://preview.redd.it/art-mountains-oceans-cover-art-v0-jpj8uj9fzkfa1.jpg?auto=webp&s=beae04b18d2ef2164ae9bf334215f5c91d838262"
						alt="cover image placeholder of mountains">
				</div>

				<div class="col-12 text-center">
					<span>
						<img class="profile-img roundedImg p-1" :src="profile.picture" :alt="`picture of ${profile.name}`">
					</span>
					<div class="mt-2 fs-2 fw-bold markoOne">{{ profile.name }}</div>

					<div class="fs-5">{{ profileKeeps.length }} Keeps | {{ profileVaults.length }} Vaults</div>
				</div>
			</section>
		</div>

		<div class="container-fluid mt-3 px-md-4 px-lg-5">
			<section class="row justify-content-center">
				<div class="mt-5 mb-4 fs-1 fw-semibold">Vaults</div>

				<div v-for="vault in profileVaults" :key="vault.id" class="col-6 col-md-4 col-lg-3 mb-5">
					<VaultCard :vault="vault" />
				</div>
			</section>

			<section class="row justify-content-center">
				<div class="mt-5 mb-4 fs-1 fw-semibold">Keeps</div>

				<div v-for="keep in profileKeeps" :key="keep.id" class="col-6 col-md-4 col-lg-3 mb-5">
					<KeepCard :keep="keep" />
				</div>
			</section>
		</div>

	</main>
</template>


<style lang="scss" scoped>
.cover-img {
	width: 100%;
	height: 25vh;
	margin-bottom: -10vh;
	object-fit: cover;
	object-position: center;
	border-radius: 10px;
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
</style>
