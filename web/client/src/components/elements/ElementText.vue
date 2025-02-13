<template>

	<div class="element__wrapper element-text">
		
    <div class="element-text__properties">

      <div class="form">
        <div class="form__label">Text color</div>
        <div class="color-wrapper">
          <input type="color" class="colorpicker" :value="props.textColor" @change="updateProp" data-key="textColor">
          <span class="color-wrapper-color">{{ props.textColor }}</span>
        </div>
      </div>

      <div class="form">
        <div class="form__label">Message</div>
        <textarea class="form__textarea" :value="props.value" @change="updateProp" data-key="value"></textarea>
      </div>

    </div>

	</div>

</template>
<script>
export default {
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