<template>
    <v-app-bar app>
      <v-btn icon @click="drawer = !drawer">
        <v-icon>mdi-menu</v-icon>
      </v-btn>
      <v-toolbar-title>Módulo Acadêmico</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-app-bar>
  
    <v-navigation-drawer v-model="drawer" permanent app>
      <v-list-item 
        :to="{ name: 'list' }"
        :class="{ 'v-list-item--active': $route.name === 'list' }"
      >
        <v-list-item-title>Consulta de alunos</v-list-item-title>
      </v-list-item>
      <v-list-item
        :to="{ name: 'create' }"
        :class="{ 'v-list-item--active': $route.name === 'create' }"
      >
        <v-list-item-title>Cadastro de alunos</v-list-item-title>
      </v-list-item>
    </v-navigation-drawer>
  </template>
  
  <script>
  export default {
    data() {
      return {
        drawer: false, 
      };
    },
  };
  </script>
  
  <style scoped>
  .v-app-bar {
    z-index: 10; 
  }
  </style>
  