<template>

	<div class="element__events form">
		
		<div class="element__events-start">

			<div class="form__label">On start</div>
			<select class="form__select" v-model="onStartSelf.action">
				<option value="show">Show</option>
				<option value="hide">Hide</option>
			</select>

		</div>

		<div class="form__label">On click</div>
		<div class="event" v-for="(item, index) of events.onInteract">
			
			<select class="event__action form__select" v-model="item.action">
				<option value="show">Show</option>
				<option value="hide">Hide</option>
				<option value="next">Next slide</option>
			</select>
			<div class="event__bind" v-if="item.action !== 'next'">
				<input type="text" class="form__input" placeholder="Element ID" v-model="item.bind" />
			</div>
			<div class="event__control">
				<a href="#" @click.prevent="deleteEvent(index, 'onInteract')" class="material-symbols-outlined">cancel</a>
			</div>

		</div>

		<button class="button button--alt" @click="newEvent('onInteract')">New trigger</button>

		<br><br>
		<div class="form__label">On hover</div>
		<div class="event" v-for="(item, index) of events.onHover">
			
			<select class="event__action form__select" v-model="item.action">
				<option value="show">Show</option>
				<option value="hide">Hide</option>
				<option value="next">Next slide</option>
			</select>
			<div class="event__bind" v-if="item.action !== 'next'">
				<input type="text" class="form__input" placeholder="Element ID without #" v-model="item.bind" />
			</div>
			<div class="event__control">
				<a href="#" @click.prevent="deleteEvent(index, 'onHover')" class="material-symbols-outlined">cancel</a>
			</div>

		</div>

		<button class="button button--alt" @click="newEvent('onHover')">New trigger</button>

	</div>

</template>
<script>
export default {
	props: {
		id: {
			type: String,
		},
		events: {
			type: Object,
			default() {
				return { onInteract: [], onHover: [] }
			}
		}
	},
	computed: {
		onStartSelf() {
			return this.events.onStart.find(el => el.bind === this.id)
		}
	},
	methods: {
		newEvent(type) {
			this.$emit('newEvent', {
				id: this.id,
				eventType: type,
			})
		},
		deleteEvent(index, type) {
			this.$emit('deleteEvent', {
				id: this.id,
				eventType: type,
				index
			})
		}
	}
}
</script>