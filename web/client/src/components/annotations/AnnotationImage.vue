<template>

	<div class="annotation annotation--image">
		<img :src="`image/${annotation.props.value}`" alt="img">
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