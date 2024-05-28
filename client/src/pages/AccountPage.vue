<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import AccountForm from '../components/AccountForm.vue';

const account = computed(() => AppState.account)

</script>

<template>
	<div class="about text-center">

		<div v-if="account">
			<h1>Welcome {{ account.name }}</h1>
			<img class="rounded" :src="account.picture" alt="" />
			<p>{{ account.email }}</p>

			<div class="container">
				<AccountForm />
			</div>
		</div>


		<div v-else class="my-5 mx-auto">
			<div class="d-flex justify-content-center align-items-center my-5">
				<div class="loader1"></div>
			</div>

			<div class="d-flex justify-content-center align-items-center">
				<div class="loader"></div>
			</div>

			<div class="d-flex justify-content-center align-items-center my-5">
				<div class="loader2"></div>
			</div>
		</div>

	</div>
</template>

<style scoped lang="scss">
img {
	max-width: 100px;
}

/* HTML: <div class="loader"></div> */
.loader {
	width: fit-content;
	font-weight: bold;
	font-family: monospace;
	font-size: 40px;
	clip-path: inset(0 3ch 0 0);
	animation: l4 1s steps(4) infinite;
}

.loader:before {
	content: "Loading..."
}

@keyframes l4 {
	to {
		clip-path: inset(0 -1ch 0 0)
	}
}


/* HTML: <div class="loader1"></div> */
.loader1 {
	width: 250px;
	height: 30px;
	box-shadow: 0 3px 0 #f4f4f4;
	--g: repeat-x repeating-linear-gradient(90deg, #2E2E2E 0 16px, #0000 0 18px, #2E2E2E 0 34px, #0000 0 100%);
	background: var(--g), var(--g);
	background-size: 60px 14px, 60px 14px;
	animation: l8 1s infinite linear;
	border-radius: 10px;
}

@keyframes l8 {
	0% {
		background-position: 0 0, 0 100%
	}

	100% {
		background-position: 60px 0, 60px 100%
	}
}

.loader2 {
	width: 250px;
	height: 30px;
	box-shadow: 0 3px 0 #f4f4f4;
	--g: repeat-x repeating-linear-gradient(90deg, #2E2E2E 0 16px, #0000 0 18px, #2E2E2E 0 34px, #0000 0 100%);
	background: var(--g), var(--g);
	background-size: 60px 14px, 60px 14px;
	animation: l8 1s infinite linear reverse;
	border-radius: 10px;
}

@keyframes l8 {
	0% {
		background-position: 0 0, 0 100%
	}

	100% {
		background-position: 60px 0, 60px 100%
	}
}
</style>
