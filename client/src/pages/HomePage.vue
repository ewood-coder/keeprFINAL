<script setup>
import { computed, effect, onMounted, ref, watchEffect } from 'vue';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';


const keeps = computed(() => AppState.keeps)


// STUB: FUNCTIONS: -----------------------------

async function getKeeps(value) {
	try {
		await keepsService.getKeeps()
	} catch (error) {
		Pop.error(error)
	}
}

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

// ----------------------------------------------

onMounted(() => {
	getKeeps()
})

</script>

<template>

	<!-- <div class="-fluid py-4 px-md-5"> -->

	<!-- SECTION: KEEPS -->
	<!-- <section class="d-flex flex-wrap justify-content-center row"> -->
	<!-- <div  v-for="keep in keeps" :key="keep.id" class="col-6 col-md-4 col-lg-3 my-3"> -->
	<div class="keeps py-5 px-2 px-md-5">
		<KeepCard :keep="keep" v-for="keep in keeps" :key="keep.id" />
	</div>

	<!-- </div> -->
	<!-- </section> -->

	<!-- </div> -->
</template>

<style scoped lang="scss">
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
