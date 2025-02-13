<template>

	<div 
		class="annotation annotation--text"
		:style="{
			'color': annotation.props.textColor
		}"
	>
    {{ annotation.props.value }}
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
	}
}
</script>