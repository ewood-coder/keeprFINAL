<script setup>
import { computed, effect, onMounted, ref, watchEffect } from 'vue';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';
import { Account } from '../models/Account.js';


const account = computed(() => AppState.account)
const keeps = computed(() => AppState.keeps)



// STUB: FUNCTIONS: -----------------------------

async function getKeeps(value) {
	try {
		await keepsService.getKeeps()
	} catch (error) {
		Pop.error(error)
	}
}
// ----------------------------------------------

onMounted(() => {
	getKeeps()
})


async function createKeep(event) {
	event.preventDefault()
	const form = event.target
	const keep = {
		name: form.Name.value,
		description: form.Description.value,
		img: form.Img.value,
	}
	try {
		await keepsService.createKeep(keep)
		Pop.success('Keep Created')
		form.reset()
	} catch (error) {
		Pop.error(error)
	}

}
</script>

<template>
	<div class="container-fluid py-5 px-md-5">

		<!-- SECTION: KEEPS -->
		<section class="d-flex flex-wrap justify-content-center row">
			<div v-for="keep in keeps" :key="keep.id" class="col-5 col-md-6 col-lg-4 mb-4 gap-1 px-md-4 py-2">

				<KeepCard :keep="keep" />

			</div>
		</section>

	</div>
</template>

<style scoped lang="scss"></style>
