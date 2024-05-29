<script setup>
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'


</script>

<template>
	<header>
		<Navbar />
	</header>
	<main>
		<router-view />
	</main>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
	--main-height: calc(100vh - 32px - 64px);
}

.mask {
	background-color: rgba(86, 86, 86, 0.3);
	backdrop-filter: blur(15px);
	border-radius: 10px;

	text-shadow: 2px 2px 2px black;
}

footer {
	display: grid;
	place-content: center;
	height: 32px;
}
</style>
