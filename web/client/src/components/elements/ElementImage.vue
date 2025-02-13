<template>

	<div class="element__wrapper element-image">
		
    <div class="element-zone__properties">

      <div class="form">
        <div class="form__label">Image</div>
        <select class="form__select" v-model="props.value">
        	<option v-for="image in images" :value="image">{{ image }}</option>
        </select>
      </div>

    </div>

	</div>

</template>
<script>
export default {
	data() {
		return {
			images: [
				'NEWballoon1.gif',
				'NEWballoon2.gif',
				'NEWballoon3.gif',
				'NEWchest1.gif',
				'NEWchest2.gif',
				'NEWchest3.gif',
				'NEWchest4.gif',
				'NEWchest5.gif',
				'NEWchest6.gif',
				'NEWcloud1.gif',
				'NEWconfusion1.gif',
				'NEWconfusion2.gif',
				'NEWghost1.gif',
				'NEWghost2.gif',
				'NEWhappiness2smiley2.gif',
				'NEWoverloading1.gif',
				'NEWoverloading2.gif',
				'NEWsmiley1.gif',
				'NEWsmiley2.gif',
				'NEWsmiley3.gif',
				'NEWsmiley4.gif',
				'NEWspider1.gif',
				'NEWspider2.gif',
				'NEWspider3.gif',
				'NEWtea1.gif',
				'NEWtea2.gif',
				'NEWtime1.gif',
				'NEWtime2.gif',
				'NEWtime3.gif',
				'NEWurgency3emptyshel2.gif',
				'alarm.gif',
				'blackbar1.gif',
				'blackbar2.gif',
				'clock.gif',
				'fear1-ghost.gif',
				'fear10cagedbird.gif',
				'fear10freedbird.gif',
				'fear1scaredycat.gif',
				'fear2-spider.gif',
				'fear4-clouds.gif',
				'fear4-clouds2.gif',
				'fear5-brokenglass1.gif',
				'fear5-brokenglass2.gif',
				'fear6-trembling.gif',
				'fear8alert.gif',
				'fear9drowning.gif',
				'happiness1fireworks.gif',
				'happiness1sun.gif',
				'happiness1sunfake.gif',
				'happiness2smiley.gif',
				'happiness2sun.gif',
				'happiness3gifts.gif',
				'overloading1-person.gif',
				'overloading2smiley.gif',
				'overloading2smiley2.gif',
				'overloading2smiley3.gif',
				'overloading3glass.gif',
				'overloading3mess1.gif',
				'overloading3mess2.gif',
				'overloading3mess3.gif',
				'overloading3mess4.gif',
				'smoke.gif',
				'urgency1stress.gif',
				'urgency2running1.gif',
				'urgency2running2.gif',
				'urgency2running3.gif',
				'urgency2running4.gif',
				'urgency3emptyshelf.gif',
				'watering.gif'
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
	}
}
</script>