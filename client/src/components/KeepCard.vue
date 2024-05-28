<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { AppState } from '../AppState.js';


const account = computed(() => AppState.account)
const keeps = computed(() => AppState.keeps)


const props = defineProps({
	keep: { type: Keep, required: true },
})


// STUB: FUNCTIONS: -----------------------------------
// async function FavoriteRecipe(recipeId) {
// 	try {
// 		await favoritesService.FavoriteRecipe(recipeId)
// 	} catch (error) {
// 		Pop.error(error)
// 	}
// }

// async function RemoveFavoriteRecipe(favoriteId) {
// 	try {
// 		await favoritesService.RemoveFavoriteRecipe(favoriteId)
// 	} catch (error) {
// 		Pop.error(error)
// 	}
// }

async function setActiveKeep() {
	console.log('setting active keep', props.keep)
	await keepsService.setActiveKeep(props.keep)
}

// ---------------------------------------------------

</script>


<template>
	<div class="keep-card rounded-4 mask1">

		<img :src="keep.img" :alt="keep.name" data-bs-toggle="modal" data-bs-target="#keepModal" role="button"
			@click="setActiveKeep()" :title="`keep information for ${keep.name}`" class="keep-img">

		<div class="px-4 py-2 bgColor">
			<div class="px-2 py-1 fontSize text-capitalize">{{ keep.name }}</div>
		</div>

		<div class="rounded">{{ keep.creatorId }}</div>
	</div>
</template>


<style scoped>
.keep-card {
	/* background-color: #ff9074; */
	background-color: #fdac98;

	color: white;

	width: auto;
	height: 100%;

	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	/* box-shadow: rgba(50, 50, 93, 0.25) 0px 2px 5px -1px, rgba(0, 0, 0, 0.3) 0px 1px 3px -1px; */
}

.keep-img {
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