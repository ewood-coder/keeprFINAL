<script setup>
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateKeepModal from './components/CreateKeepModal.vue';
import KeepModal from './components/KeepModal.vue';
import CreateVaultModal from './components/CreateVaultModal.vue';



</script>

<template>
	<header>
		<Navbar />
	</header>
	<main>
		<router-view />
	</main>



	<!-- SECTION: MODALS -->
	<KeepModal />
	<CreateKeepModal />
	<CreateVaultModal />
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
	--main-height: calc(100vh - 32px - 64px);

	--bgDarkBlue: #2b2d42;
	--bgPink: #E86A92;
	--bgGold: #FFD41C;
	--bgLightBlue: #2bb6cf;
	--bgTurquoise: #41e2ba;
	--bgGreen: #22c122;
	--bgGray: #868686;
	--bgRed: #da0000;
}

.txtShadow {
	text-shadow: 3px 3px 3px black;
}

.grayText {
	color: rgb(98, 98, 98);
}

.createBtn {
	background-color: var(--bgLightBlue);
	border: none;
	transition: 0.4s;
	color: white;
}

.createBtn:hover {
	background-color: var(--bgGreen);
	color: white;
}

.markoOne {
	font-family: "Marko One", serif;
}

.quando {
	font-family: "Quando", serif;
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
