<template>

  <div v-if="!connected" class="web web--qr">
    <img :src="QRImage" alt="QR image" @click="closeQR" ref="QR">
  </div>
  <browser v-else :ssl="activeSlide.target.ssl" :url="activeSlide.target.url">
    <img :src="activeSlide.target.image" alt="demo" />


    <div 
      class="annotation" 
      
      v-for="element in activeSlide.elements"

      :style="{
        'left': `${element.offset[0]}%`,
        'top': `${element.offset[1]}%`,
        'width': `${element.dimension[0]}%`,
        'height': `${element.dimension[1]}%`,
        'display': element.events.onStart[0].action === 'show' ? 'block' : 'none'
      }"

      @click="handleEvents(element, 'onInteract')"
      @mouseover="handleEvents(element, 'onHover')"
    >
    
      <annotation-zone v-if="element.type === 'zone'" :annotation="element" />
      <annotation-text v-if="element.type === 'text'" :annotation="element" />
      <annotation-audio v-if="element.type === 'audio'" :annotation="element" />
      <annotation-image v-if="element.type === 'image'" :annotation="element" />

    </div>

  </browser>

</template>
<style lang="scss">
  .annotation {
    position: absolute;
    z-index: 20;
    background: transparent;
  }
  .app {
    overflow: hidden;
  }
  .web {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 100vw;
    height: 100vh;
    overflow: hidden;

    &--qr { 
      img {
        cursor: pointer;
      }
      p {
        text-align: center;
        color: #ffffff;
        font-size: .9rem;
        padding-top: 20px;
      }
    }
  }
</style>
<script>
import axios from 'axios' 
import QRious from 'qrious'
import Browser from '@/components/Browser.vue'

import AnnotationText from '@/components/annotations/AnnotationText'
import AnnotationAudio from '@/components/annotations/AnnotationAudio'
import AnnotationZone from '@/components/annotations/AnnotationZone'
import AnnotationImage from '@/components/annotations/AnnotationImage'

const PATH = `${window.location.protocol}//${window.location.hostname}:3000`
// const PATH = window.location.origin
const END_URL = 'https://www.descil.ethz.ch/apps/qs/phishar?sid=SV_5BGSfDBz7Vq4ZzU'

import '@/assets/scss/web.scss'
export default {
  data() {
    return {
      connected: false,
      activeJson: null,
      content: null,
      slide: 0,
      annotations: false
    }
  },
  components: {
    Browser,
    AnnotationText,
    AnnotationZone,
    AnnotationAudio,
    AnnotationImage
  },
  computed: {
    isHeadset() {
      return this.$route.meta.show === 'headset'
    },
    isWeb() {
      return this.$route.meta.show === 'web'
    },
    annotationStart() {
      return this.annotationEvent === 'onStart'
    },
    annotationToggle() {
      return this.annotationEvent === 'onToggle'
    },
    annotationHover() {
      return this.annotationEvent === 'onClick'
    },
    annotationEvent() {
      return this.activeSlide.target.annotate
    },
    activeSlide() {
      return this.content[this.slide]
    },
    QRImage() {

      const QRSize = 600

      const data = {
        width: (QRSize / document.body.offsetWidth * 100).toFixed(2),
        height: (QRSize / document.body.offsetHeight * 100).toFixed(2),
        api: `${PATH}/api/headset`
      }

      const qr = new QRious({
        level: 'L',
        size: QRSize,
        value: JSON.stringify(data)
      })

      return qr.toDataURL()
    }
  },
  methods: {
    closeQR() {
      this.connected = true
    },
    async getJson() {
      const response = await axios.get(`${PATH}/api/web`)
      this.content = response.data
    },
    updateJson() {
      axios.post(`${PATH}/api/active/slide`, { slide: this.slide })
    },
    toggleAnnotations() {
      this.annotations = !this.annotations
    },
    prevSlide() {
      this.slide = (this.slide === 0) ? 0 : this.slide - 1

      if (this.isHeadset) {
        this.updateJson()
      }
    },
    nextSlide() {
      if (this.content.length === this.slide + 1) {
        window.location.href = END_URL
        return true
      }

      this.slide = this.slide + 1

      if (this.isHeadset) {
        this.updateJson()
      }
    },
    toggleElement(id, show) {
      let element = this.activeSlide.elements.find(el => el.id === id)

      if (element) {        
        element.events.onStart.find(el => el.bind === id).action = show
      } else {
        console.error('Action bound to non-existing element id')
      }
    },
    handleEvents(element, type) {
      for(let event of element.events[type]) {

        if (['show', 'hide'].includes(event.action)) {
          this.toggleElement(event.bind, event.action)
        }

        if (event.action === 'next') {
          this.nextSlide()
        }

      }
    }
  },
  async mounted() {
    await this.getJson()

    if (this.isWeb) {
      this.closeQR()
    }

    document.addEventListener('keyup', event => {

      if (this.content.length > 1) {
        if (event.keyCode === 79) {
          this.prevSlide()
        }
        if (event.keyCode === 80) {
          this.nextSlide()
        }
      }
    })
  }
}
</script>