<template>

	<div 
		class="annotation annotation--zone"
		:style="{
			'border-color': annotation.props.borderColor,
			'border-style': annotation.props.borderStyle
		}"
	></div>

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