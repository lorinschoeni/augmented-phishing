<template>

  <div class="app">
    <router-view />
  </div>

</template>
<style lang="scss">
  * {
    box-sizing: border-box;
    font-family: sans-serif;
  }
  body, html {
    width: 100vw;
    height: 100vh;
    margin: 0;
    padding: 0;
  }
  .app {
    width: 100vw;
    height: 100vh;
    background: black;
  }
</style>