<template>

	<div class="browser" ref="browser">

		<div class="browser__header">
			
			<img class="browser__header-buttons" src="@/assets/BrowserButtons.png" alt="buttons">
			<div class="browser__header-url">
				<img src="@/assets/PadlockSecure.png" alt="Secure connection" title="Secure connection" v-if="ssl">
				<img src="@/assets/PadlockNotSecure.png" alt="Unsecure connection" title="Unsecure connection" v-else>
				{{ url }}
			</div>

		</div>
		<div class="browser__body">
			<slot></slot>
		</div>

	</div>

</template>
<style lang="scss">
	.browser {
		width: 100%;
		// height: 100%;
		display: flex;
		flex-direction: column;
		overflow: hidden;
		// pointer-events: none;
	  user-select: none;

		&__header {
			flex-shrink: 1;
			background-color: #232835;
			display: flex;
			flex-direction: row;
			align-items: center;
			justify-content: flex-start;
			padding: 20px;
			border-top-left-radius: 10px;
			border-top-right-radius: 10px;

			&-buttons {
				margin-right: 20px;
				height: 13px;
			}

			&-url {
				color: #FFFFFF;
				font-family: sans-serif;
				font-size: .9rem;
				background-color: #1E1E2A;
				padding: 10px 15px;
				border-radius: 30px;
				flex-grow: 1;
				overflow: hidden;

				display: flex;
				flex-direction: row;
				align-items: center;
				letter-spacing: 0.5px;

				img {
					height: 15px;
					width: 15px;
					margin-right: 10px;
				}
			}
		}

		&__body {
			flex-grow: 1;
			background-color: #FFFFFF;
			border-bottom-left-radius: 10px;
			border-bottom-right-radius: 10px;
			overflow: hidden; //scroll;
			position: relative;

			img {
				height: auto;
				width: 100%;
			}
		}
	}
</style>
<script>
export default {
	props: {
		url: {
			type: String,
			default: 'Hello World'
		}
	},
	computed: {
		ssl() {
			return this.url.startsWith('https://')
		},
		urlPath() {
			return `${this.ssl ? 'https://' : 'http://'}${this.url}`
		}
	}
}
</script>