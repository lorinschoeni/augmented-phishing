<template>
  
  <div class="admin">
    
    <div class="admin__sidebar">
      
      <ul class="admin__sidebar-tabs">
        <li @click="switchTab('project')" :class="{ 'active': tab === 'project' }">Project</li>
        <li @click="switchTab('slides')" :class="{ 'active': tab === 'slides' }">Slides</li>
        <li @click="switchTab('elements')" :class="{ 'active': tab === 'elements' }">Elements</li>
      </ul>

      <div class="project" v-if="tab === 'project'">

        <div class="form form--column">
          
          <label class="form__label">Scene</label>
          <select v-model="ui.selectedJson" class="form__select">
            <option v-for="json in ui.jsons" :value="json">{{ json }}</option>
            <option :value="null">&rarr; New config</option>
          </select>

        </div>

        <div class="project__actions" v-if="ui.slide">
          <button v-if="ui.activeJson !== ui.selectedJson" @click="setActive" class="button">Activate</button>
          <button v-else @click="openWeb" class="button button--alt"><span class="active"></span> Preview</button>
          <br><br>

          &nbsp; 
          <button class="button" @click="saveJSON">Save project</button>
        </div>

        <!-- <pre style="background:white;">{{ activeSlide }}</pre> -->

      </div>
      <div class="slides" v-if="tab === 'slides'">

        <div class="slides__new">
          <button class="button" @click="addSlide">Add slide</button>
        </div>

        <div class="slides__list">

          <div class="slide" v-for="(slide, iter) in content" :class="{ 'slide--selected': ui.slide === slide.id }">
            <div class="slide__header">
              <div class="slide__header-title" @click="selectSlide(slide.id)">
                Slide {{ iter + 1 }}
              </div>
              <div class="slide__header-action">
                <img src="@/assets/IconTrash.png" alt="Trash" @click="deleteSlide(slide.id)">
              </div>
            </div>
            <div class="slide__image" @click="selectSlide(slide.id)">
              <img v-if="slide.target.image" :src="slide.target.image">
              <p v-else>No image</p>
            </div>
            <div class="slide__settings">
              
              <label class="form">
                <!-- <div class="form__label">URL</div> -->
                <input type="text" class="form__input" v-model="slide.target.url" placeholder="URL">
              </label>

            </div>
          </div>

        </div>
        
      </div>
      <div class="elements" v-if="ui.slide && tab === 'elements'">

        <div class="elements__new">
          <select v-model="ui.newElement" class="select">
            <option value="text">Text</option>
            <option value="zone">Zone</option>
            <option value="audio">Audio</option>
            <option value="image">Image</option>
          </select>
          <button class="button" @click="elementAdd">Add element</button>
        </div>

        <div class="elements__list" v-if="activeSlide.elements">
          
          <div class="element" v-for="element in activeSlide.elements">
            
            <div class="element__header">
              <div class="element__header-id" @click="elementCopyId">{{ element.id }}</div>
              <a href="#" class="element__header-action" @click.prevent="elementDelete(element.id)">
                <span class="material-symbols-outlined">cancel</span>
              </a>
            </div>

            <element-zone v-if="element.type === 'zone'" :props="element.props" :id="element.id" @updateProp="updateElementProp" :key="element.id" />
            <element-text v-if="element.type === 'text'" :props="element.props" :id="element.id" @updateProp="updateElementProp" :key="element.id" />
            <element-audio v-if="element.type === 'audio'" :props="element.props" :id="element.id" @updateProp="updateElementProp" :key="element.id" />
            <element-image v-if="element.type === 'image'" :props="element.props" :id="element.id" @updateProp="updateElementProp" :key="element.id" />

            <element-events 
              :key="`ev_${element.id}`" 
              :events="element.events" 
              :id="element.id" 

              @newEvent="newElementEvent" 
              @updateEvent="updateElementEvent" 
              @deleteEvent="deleteElementEvent" 
            />

          </div>

        </div>

      </div>

    </div>
    <div class="admin__main">

      <div class="admin__content">
        
        <browser 
          v-if="ui.slide"

          :url="activeSlide.target.url"
          @dragover="e => e.preventDefault()"
          @drop="e => e.preventDefault()"
        >
          
          <div 
            v-if="!activeSlide.target.image" 

            class="admin__dropzone"
            :class="{ 'admin__dropzone--hover': ui.file.over }"

            @dragover="fileOver"
            @dragleave="fileLeave"

            @drop="fileDrop"
          >
            <p @click="fileSelect">Drag 'n' drop or <span>select</span> image to preview</p>

            <input
              class="admin__dropzone-input"
              @change="fileChange"
              ref="file"
              type="file"
              name="file"
              id="fileInput"
              accept=".jpg,.jpeg,.png"
            />

          </div>

          <div v-else class="admin__annotations" ref="annotationArea">
            
            <img :src="activeSlide.target.image" alt="Image marker" ref="image">

            <Vue3DraggableResizable
              v-for="annotation in activeSlide.elements.filter(an => an.draw)"
              :key="annotation.id"

              :parent="true"

              :style="{
                left: `${annotation.offset[0]}%`,
                top: `${annotation.offset[1]}%`,
                width: `${annotation.dimension[0]}%`,
                height: `${annotation.dimension[1]}%`
              }"

              @resizing="({ x, y, w, h }) => onResizing(annotation, w, h)"
              @dragging="({ x, y }) => onDragging(annotation, x, y)"
            >
              <annotation-zone v-if="annotation.type === 'zone'" :annotation="annotation" />
              <annotation-text v-if="annotation.type === 'text'" :annotation="annotation" />
              <annotation-image v-if="annotation.type === 'image'" :annotation="annotation" />
            </Vue3DraggableResizable>

          </div>

        </browser>

      </div>

      <notifications position="bottom left" />
    </div>

  </div>

</template>
<script>
import { Guid } from 'js-guid'
import axios from 'axios'
import Browser from '@/components/Browser'

import ElementZone from '@/components/elements/ElementZone'
import ElementText from '@/components/elements/ElementText'
import ElementAudio from '@/components/elements/ElementAudio'
import ElementImage from '@/components/elements/ElementImage'

import ElementEvents from '@/components/elements/ElementEvents'

import AnnotationText from '@/components/annotations/AnnotationText'
import AnnotationZone from '@/components/annotations/AnnotationZone'
import AnnotationImage from '@/components/annotations/AnnotationImage'

const PATH = `${window.location.protocol}//${window.location.hostname}:3000`
const ANNOTATION_STATES = { onStart: 'onStart', onClick: 'onClick', onToggle: 'onToggle' }
const TABS = { project: 'project', elements: 'elements', slides: 'slides' }
export default {
  components: {
    Browser,
    ElementZone,
    ElementText,
    ElementAudio,
    ElementImage,
    ElementEvents,
    AnnotationText,
    AnnotationZone,
    AnnotationImage
  },
  data() {
    return {
      tab: TABS.project,
      ui: {
        annotationStates: ANNOTATION_STATES,
        slide: null,
        file: {
          over: false,
          src: null,
        },
        annotation: {
          start: [null, null],
          end: [null, null]
        },
        jsons: [],
        selectedJson: 0,
        newJson: '',
        activeJson: null,
        newElement: 'text'
      },
      emptyElements: {
        zone: {
          draw: true,
          type: 'zone',
          props: {
            borderStyle: 'solid',
            borderColor: '#FF0000'
          }
        },
        text: {
          draw: true,
          type: 'text',
          props: {
            textColor: '#000000',
            value: ''
          }
        },
        audio: {
          draw: false,
          type: 'audio',
          props: {
            value: 'NEWballoon1.mp4'
          }
        },
        image: {
          draw: true,
          type: 'image',
          props: {
            value: 'NEWballoon1.gif'
          }
        }
      },
      content: []
    }
  },
  computed: {
    activeSlide() {
      return this.content.find(x => x.id === this.ui.slide)
    },
    annotationLeft() {
      return this.ui.annotation.start[0] < this.ui.annotation.end[0] ? this.ui.annotation.start[0] : this.ui.annotation.end[0]
    },
    annotationTop() {
      return this.ui.annotation.start[1] < this.ui.annotation.end[1] ? this.ui.annotation.start[1] : this.ui.annotation.end[1]
    },
    annotationWidth() {
      return Math.abs(this.ui.annotation.end[0] - this.ui.annotation.start[0])
    },
    annotationHeight() {
      return Math.abs(this.ui.annotation.end[1] - this.ui.annotation.start[1])
    },
    annotationLeftPerc() {
      return this.ui.annotation.start[0] < this.ui.annotation.end[0] 
        ? (this.ui.annotation.start[0] / this.$refs.image.offsetWidth * 100).toFixed(2)
        : (this.ui.annotation.end[0] / this.$refs.image.offsetWidth * 100).toFixed(2)
    },
    annotationTopPerc() {
      return this.ui.annotation.start[1] < this.ui.annotation.end[1] 
        ? (this.ui.annotation.start[1] / this.$refs.image.offsetHeight * 100).toFixed(2)
        : (this.ui.annotation.end[1] / this.$refs.image.offsetHeight * 100).toFixed(2)
    },
    annotationWidthPerc() {
      return (Math.abs(this.ui.annotation.end[0] - this.ui.annotation.start[0]) / this.$refs.image.offsetWidth * 100).toFixed(2)
    },
    annotationHeightPerc() {
      return (Math.abs(this.ui.annotation.end[1] - this.ui.annotation.start[1]) / this.$refs.image.offsetHeight * 100).toFixed(2)
    },
    annotationProperties() {
      return { 
        'left': `${this.annotationLeft}px`, 
        'top': `${this.annotationTop}px`, 
        'width': `${this.annotationWidth}px`,
        'height': `${this.annotationHeight}px`
      }
    },
    annotationPropertiesPerc() {
      return { 
        'left': `${this.annotationLeftPerc}%`, 
        'top': `${this.annotationTopPerc}%`, 
        'width': `${this.annotationWidthPerc}%`,
        'height': `${this.annotationHeightPerc}%`
      }
    }
  },
  methods: {
    updateElementProp(prop) {
      this.activeSlide.elements.find(el => el.id === prop.id).props[prop.key] = prop.value
    },
    newElementEvent(event) {
      this.activeSlide.elements.find(el => el.id === event.id).events[event.eventType].push({ 
        action: 'show',
        bind: ''
      })
    },
    updateElementEvent(event) {
      this.activeSlide.elements.find(el => el.id === event.id).events[event.eventType][event.index][event.key] = event.value
    },
    deleteElementEvent(event) {
      this.activeSlide.elements.find(el => el.id === event.id).events[event.eventType].splice(event.index, 1)
    },
    switchTab(tab) {
      this.tab = TABS[tab] || TABS.project
    },
    elementCopyId(event) {
      const storage = document.createElement('textarea')
      storage.value = event.target.innerHTML.replace('#', '')
      event.target.appendChild(storage)

      storage.select()
      storage.setSelectionRange(0, 99999)
      document.execCommand('copy')

      event.target.removeChild(storage)
    },
    elementAdd() {
      if (!Object.keys(this.emptyElements).includes(this.ui.newElement)) return false

      const newElementID = Guid.newGuid().toString()
      let newElement = JSON.parse(JSON.stringify(this.emptyElements[this.ui.newElement]))
          newElement.id = newElementID
          newElement.offset = [10, 10]
          newElement.dimension = [10, 10]
          newElement.events = {
            onStart: [{ action: 'show', bind: newElementID }],
            onInteract: [],
            onHover: []
          }

      this.activeSlide.elements.push(newElement)
    },
    elementDelete(id) {
      this.activeSlide.elements = [...this.activeSlide.elements.filter(el => el.id !== id)]
    },
    fileChange() {
      const fileReader = new FileReader()
      fileReader.onload = () => {
        this.activeSlide.target.image = fileReader.result
      }
      fileReader.readAsDataURL(this.$refs.file.files[0])
    },
    fileSelect() {
      this.$refs.file.click()
    },
    fileOver(e) {
      e.preventDefault()
      this.ui.file.over = true
    },
    fileLeave() {
      this.ui.file.over = false
    },
    fileDrop(e) {
      e.preventDefault()
      this.$refs.file.files = e.dataTransfer.files;
      this.ui.file.over = false
      this.fileChange()
    },
    saveJSON() {
      axios.post(`${PATH}/api/update`, { filename: this.ui.selectedJson, data: this.content })
      this.$notify({ type: 'success', text: 'Changes sucessfully saved!' })
    },
    async newJSON() {
      const filename = window.prompt('Specify filename without extension:')
      if (filename === null) {
        await this.getActive()
        return false
      }

      const response = await axios.post(`${PATH}/api/new`, { filename, data: [] })
      await this.loadJSONs()
      this.ui.selectedJson = response.data.filename
    },
    async loadJSONs() {
      axios.get(`${PATH}/api/config`).then(response => {
        if (response && response.data && response.data.files) {
          this.ui.jsons = [... response.data.files]
        }
      })
    },
    loadJSON() {
      axios.get(`${PATH}/api/config/${this.ui.selectedJson}`).then(response => {
        if (response && response.data) {
          this.content = [ ...response.data ]

          if (this.content.length > 0) {
            this.selectSlide(this.content[0].id)
          } else {
            this.addSlide()
          }
        }
      })
    },
    deleteSlide(id) {

      if (this.content.length === 1) {
        this.ui.slide = null
        this.content = []
        return false
      }

      if (this.activeSlide.id === id) {
        this.selectSlide(this.content.find(slide => slide.id !== id).id)
      }

      this.content = this.content.filter(slide => slide.id !== id)

    },
    addSlide() {
      const id = Guid.newGuid().toString()
      this.content.push({
        id,
        target: {
          image: null,
          url: ''
        },
        elements: []
      })

      this.selectSlide(id)
    },
    selectSlide(id) {
      this.ui.slide = id
    },
    setActive() {
      axios.post(`${PATH}/api/active/config`, { filename: this.ui.selectedJson })
      this.ui.activeJson = this.ui.selectedJson
    },
    async getActive() {
      const response = await axios.get(`${PATH}/api/active/config`)
      if (!response.data.active) return false
      if (this.ui.jsons.includes(response.data.active)) {
        this.ui.activeJson = response.data.active
        this.ui.selectedJson = response.data.active
      }
    },
    openWeb() {
      const route = this.$router.resolve({ name: 'web' })
      window.open(route.href, '_blank')
    },
    onDragging(annotation, left, top) {
      let x = (left / this.$refs.annotationArea.offsetWidth) * 100;
      let y = (top / this.$refs.annotationArea.offsetHeight) * 100;

      annotation.offset[0] = x;
      annotation.offset[1] = y;
    },
    onResizing(annotation, width, height) {
      console.log(width, height)
      let w = (width / this.$refs.annotationArea.offsetWidth) * 100;
      let h = (height / this.$refs.annotationArea.offsetHeight) * 100;

      console.log('resizing to ', w)

      annotation.dimension[0] = w;
      annotation.dimension[1] = h;
    }
  },
  watch: {
    'ui.selectedJson': {
      handler() {
        if (this.ui.selectedJson === null) {
          this.newJSON()
        } else {
          this.loadJSON()
        }
      }
    }
  },
  async mounted() {
    await this.loadJSONs()
    await this.getActive()
  }
}
</script>