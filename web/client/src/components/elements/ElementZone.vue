<template>

	<div class="element__wrapper element-zone">
		
    <!-- <div class="element-zone__line" :style="zoneStyles"></div> -->

    <div class="element-zone__properties">

      <div class="form">
        <div class="form__label">Border color</div>
        <div class="color-wrapper">
          <input type="color" class="colorpicker" :value="props.borderColor" @change="updateProp" data-key="borderColor">
          <span class="color-wrapper-color">{{ props.borderColor }}</span>
        </div>
      </div>

      <div class="form">
        <div class="form__label">Type</div>
        <select class="form__select" :value="props.borderStyle" @change="updateProp" data-key="borderStyle">
        	<option value="solid">Solid</option>
        	<option value="dashed">Dashed</option>
        </select>
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
			return {
				'border-width': '3px',
				'border-color': this.props.borderColor,
				'border-style': this.props.borderStyle
			}
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