<template>
  <v-app>
    <v-app-bar v-if="!isDesktop" app color="#000">
      <v-btn icon @click="drawer = !drawer">
        <v-icon>mdi-menu</v-icon>
      </v-btn>
      <v-toolbar-title>Módulo Acadêmico</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" :permanent="isDesktop" :temporary="!isDesktop" :rail="isDesktop"
      :expand-on-hover="isDesktop" app color="#000" style="border-right: 1px solid white;">
      <v-list-item :to="{ name: 'home' }" class="logo-item d-flex justify-center mb-3">
        <v-img :width="150" aspect-ratio="16/9" cover src="../assets/logo-mais-a-educacao-negativo.svg"></v-img>
      </v-list-item>

      <v-list-item class="rounded-s-pill" prepend-icon="mdi-magnify" :to="{ name: 'list' }"
        :class="{ 'v-list-item--active': $route.name === 'list' }">
        <v-list-item-title>Consulta de alunos</v-list-item-title>
      </v-list-item>

      <v-list-item class="rounded-s-pill" prepend-icon="mdi-account-plus" :to="{ name: 'create' }"
        :class="{ 'v-list-item--active': $route.name === 'create' }">
        <v-list-item-title>Cadastro de alunos</v-list-item-title>
      </v-list-item>
    </v-navigation-drawer>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      drawer: true, 
    };
  },
  computed: {
    isDesktop() {
      return this.$vuetify.display.mdAndUp; 
    },
  },
  watch: {
    isDesktop(newVal) {
      this.drawer = newVal; 
    },
  },
};
</script>

<style scoped>
.v-app-bar {
  z-index: 10;
}

.v-list-item--active {
  background: white;
  color: #000;
}

.logo-item{ 
  background: black !important;
}
</style>
