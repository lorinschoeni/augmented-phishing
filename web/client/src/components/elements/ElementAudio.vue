<template>

	<div class="element__wrapper element-audio">
		
    <div class="element-audio__properties">

      <div class="form">
        <div class="form__label">Audio track</div>
        <select class="form__select" :value="props.value" @change="updateProp" data-key="value">
        	<option v-for="file in audioFiles" :value="file">{{ file }}</option>
        </select>
      </div>

      <div class="element-audio__controls">
      	<button class="button button--alt button--small" @click="audioPlay">
      		<span class="material-symbols-outlined">play_arrow</span>
      	</button>
      	<!-- <button class="button button--alt button--small" @click="audioPause">
      		<span class="material-symbols-outlined">pause</span>
      	</button> -->
      	<audio controls :src="`audio/${props.value}`" ref="audioFile"></audio>
      </div>


    </div>

	</div>

</template>
<script>
export default {
	data() {
		return {
			audioFiles: [
				'NEWballoon1.mp4',
				'NEWballoon2.mp4',
				'NEWballoon3.mp4',
				'NEWcat.mp4',
				'NEWghostboo.mp4',
				'NEWhappy.mp4',
				'NEWkettle.mp4',
				'NEWknot1.mp4',
				'NEWknot2.mp4',
				'NEWrecordscratch.mp4',
				'NEWrunning.mp4',
				'NEWspidersounds.mp4',
				'confusion1-whoo.mp4',
				'fear1-birds.mp4',
				'fear1-spooky.mp4',
				'fear2-thunder.mp4',
				'fear3-breaking.mp4',
				'fear4-beat.mp4',
				'happiness1-firework.mp4',
				'urgency1-ticktock.mp4',
				'urgency2-alarm.mp4'
			]
		}
	},
	props: {
		id: {
			type: String,
		},
		props: {
			type: Object,
			default() {
				return {}
			}
		}
	},
	computed: {
		zoneStyles() {
			return {}
		}
	},
	methods: {
		audioPlay() {
			this.$refs.audioFile.play()
		},
		audioPause() {
			this.$refs.audioFile.pause()
		},
		updateProp(event) {
			this.$emit('updateProp', {
				id: this.id,
				key: event.target.getAttribute('data-key'),
				value: event.target.value
			})
		}
	}
}
</script>