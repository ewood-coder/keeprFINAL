<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import AccountForm from '../components/AccountForm.vue';

const account = computed(() => AppState.account)

</script>

<template>
	<main>

		<div class="text-center text-light">
			<div v-if="account" class="pt-3">
				<div class="mask py-1 mx-2 mx-md-5">
					<div class="fs-1 fw-semibold">Welcome {{ account.name }}</div>
					<div>
						<img class="rounded my-2" :src="account.picture" alt="" />
						<p class="fs-5">{{ account.email }}</p>
					</div>
				</div>

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

	</main>

</template>

<style scoped lang="scss">
img {
	max-width: 100px;
}

// STUB: LOADING... LOADER
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


// STUB: FIRST BOX LOADER
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


// STUB: SECOND BOX LOADER
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



// STUB: BACKGROUND ANIMATION
main {
	height: 95vh;
	background-color: hsla(200, 40%, 30%, .4);
	background-image:
		url('https://78.media.tumblr.com/cae86e76225a25b17332dfc9cf8b1121/tumblr_p7n8kqHMuD1uy4lhuo1_540.png'),
		url('https://78.media.tumblr.com/66445d34fe560351d474af69ef3f2fb0/tumblr_p7n908E1Jb1uy4lhuo1_1280.png'),
		url('https://78.media.tumblr.com/8cd0a12b7d9d5ba2c7d26f42c25de99f/tumblr_p7n8kqHMuD1uy4lhuo2_1280.png'),
		url('https://78.media.tumblr.com/5ecb41b654f4e8878f59445b948ede50/tumblr_p7n8on19cV1uy4lhuo1_1280.png'),
		url('https://78.media.tumblr.com/28bd9a2522fbf8981d680317ccbf4282/tumblr_p7n8kqHMuD1uy4lhuo3_1280.png');
	background-repeat: repeat-x;
	background-position:
		0 20%,
		0 100%,
		0 50%,
		0 100%,
		0 0;
	background-size:
		2500px,
		800px,
		500px 200px,
		1000px,
		400px 260px;
	animation: 50s para infinite linear;
}

@media only screen and (max-width: 767px) {
	main {
		height: 100%;
		background-color: hsla(200, 40%, 30%, .4);
		background-image:
			url('https://78.media.tumblr.com/cae86e76225a25b17332dfc9cf8b1121/tumblr_p7n8kqHMuD1uy4lhuo1_540.png'),
			url('https://78.media.tumblr.com/66445d34fe560351d474af69ef3f2fb0/tumblr_p7n908E1Jb1uy4lhuo1_1280.png'),
			url('https://78.media.tumblr.com/8cd0a12b7d9d5ba2c7d26f42c25de99f/tumblr_p7n8kqHMuD1uy4lhuo2_1280.png'),
			url('https://78.media.tumblr.com/5ecb41b654f4e8878f59445b948ede50/tumblr_p7n8on19cV1uy4lhuo1_1280.png'),
			url('https://78.media.tumblr.com/28bd9a2522fbf8981d680317ccbf4282/tumblr_p7n8kqHMuD1uy4lhuo3_1280.png');
		background-repeat: repeat-x;
		background-position:
			0 20%,
			0 100%,
			0 50%,
			0 100%,
			0 0;
		background-size:
			2500px,
			800px,
			500px 200px,
			1000px,
			400px 260px;
		animation: 50s para infinite linear;
	}
}

@keyframes para {
	100% {
		background-position:
			-5000px 20%,
			-800px 95%,
			500px 50%,
			1000px 100%,
			400px 0;
	}
}
</style>
