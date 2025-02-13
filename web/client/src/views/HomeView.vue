<template>
  
  <div class="home">
    
    <button @click="openWeb">Open WEB interface</button>
    <button @click="openHeadset">Open HEADSET interface</button>
    <button @click="openAdmin">Open ADMIN interface</button>

  </div>

</template>
<style lang="scss">
  .home {
    width: 100vw;
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: #232835;
  }

  button {
    background-color: #FFFFFF;
    border: 0;
    outline: 0;
    padding: 10px 20px;
    border-radius: 30px;

    &:not(:last-child) {
      margin-right: 20px;
    }
    &:hover {
      background-color: #EFEFEF;
      cursor: pointer;
    }
  }
</style>
<script>
export default {
  methods: {
    openWeb() { 
      document.body.requestFullscreen()
        .then(() => {
          this.$router.push({ name: 'web' })
        })
        .catch(err => {
          console.error('Could not request fullscreen')
        })
    },
    openHeadset() { 
      document.body.requestFullscreen()
        .then(() => {
          this.$router.push({ name: 'headset' })
        })
        .catch(err => {
          console.error('Could not request fullscreen')
        })
    },
    openAdmin() {
      this.$router.push({ name: 'admin' })
    },
    openApi() {
      window.location = `${window.location.origin}/api.json`
    },
  }
}
</script>