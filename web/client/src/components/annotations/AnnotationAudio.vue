<template>

	<div 
		class="annotation annotation--audio"
	>
    <audio ref="audioFile" controls loop :src="`audio/${annotation.props.value}`"></audio>
  </div>

</template>
<script>
export default {
	props: {
		annotation: {
			type: Object,
			default() {
				return {}
			}
		}
	},
	methods: {
		audioController(){
			const element = this.annotation.events.onStart.find(el => el.bind === this.annotation.id)

			if (element.action === 'show') {
				this.$refs.audioFile.play()
			} else {
				this.$refs.audioFile.pause()
			}
		}
	},
	watch: {
		annotation: {
			deep: true,
			handler() {
				this.audioController()
			}
		}
	},
	mounted() {
		setTimeout(this.audioController, 0)
	}
}
</script>