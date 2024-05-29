<script setup>
import { onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import { AppState } from '../AppState.js';


const accountData = ref({
	name: '',
	picture: '',
	coverImg: '',
})

onMounted(() => {
	// accountData.value = AppState.account // they become the same thing, until saved they they are different
	accountData.value = { ...AppState.account } // creates a copy, with no reference, so as we change it, it doesn't change the account in the appstate
})

async function saveAccount() {
	try {
		await accountService.updateAccount(accountData.value)
		Pop.toast("Account saved!", 'success')
	} catch (error) {
		Pop.toast("Could not save account changes", 'error')
		console.error(error)
	}
}

</script>


<template>
	<hr class="mb-3" />
	<form @submit.prevent="saveAccount()" class="row">
		<div class="mb-3 col-12 col-md-4 mb-5">
			<label for="account-name" class="mask px-3 mb-2 fs-5">Name</label>
			<input v-model="accountData.name" class="form-control" type="text" id="account-name" minlength="2" required
				maxlength="25" />
		</div>
		<div class="mb-3 col-12 col-md-4 mb-5">
			<label for="account-picture" class="mask px-3 mb-2 fs-5">Picture</label>
			<input v-model="accountData.picture" class="form-control" type="url" id="account-picture" maxlength="500">
		</div>
		<div class="mb-3 col-12 col-md-4 mb-5">
			<label for="account-cover-image" class="mask px-3 mb-2 fs-5">Cover Image</label>
			<input v-model="accountData.coverImg" class="form-control" type="url" id="account-cover-image" maxlength="500">
		</div>


		<div class="mb-3 col-12 d-flex justify-content-center">
			<button class="btn btn-primary text-light">Save <i class="mdi mdi-floppy"></i></button>
		</div>
	</form>
</template>


<style lang="scss" scoped>
.cover-img {
	width: 100%;
	height: 20vh;
	margin-bottom: -10vh;
	object-fit: cover;
	object-position: center;
}

.profile-img {
	height: 100px;
	width: 100px;
	object-fit: cover;
	object-position: center;
}

.mask {
	background-color: rgba(165, 165, 165, 0.1);
	backdrop-filter: blur(15px);
	border-radius: 10px;

	text-shadow: 2px 2px 2px black;
}
</style>
