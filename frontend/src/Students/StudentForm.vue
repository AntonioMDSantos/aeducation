<template>
    <div class="d-flex justify-center my-8">
        <h1>Cadastro de Alunos</h1>
    </div>
    <v-form @submit.prevent="salvarAluno">
        <v-text-field label="Nome" v-model="aluno.name" :rules="[rules.required]" required />
        <v-text-field label="Email" v-model="aluno.email" :rules="[rules.required]" required />
        <v-text-field label="RA" v-model="aluno.ra" required />
        <v-text-field label="CPF" v-model="aluno.cpf" required />
        <div class="d-flex justify-center">
            <v-btn class="mr-5" @click="cancelar" color="red">Cancelar</v-btn>
            <v-btn class="ml-5" type="submit" color="primary">Salvar</v-btn>
        </div>
    </v-form>
</template>
  
<script>
import axios from 'axios'; 

export default {
    name: 'StudentForm',
    data() {
        return {
            aluno: {
                name: '',
                email: '',
                ra: '', 
                cpf: '', 
            },
            rules: {
                required: value => !!value || 'Este campo é obrigatório',
            },
        };
    },
    methods: {
        salvarAluno() {
            const apiUrl = process.env.VUE_APP_BACKEND_URL; 
            axios.post(`${apiUrl}/api/student`, this.aluno)

                .then(response => {
                    console.log('Aluno cadastrado:', response.data);
                    this.$router.push('/alunos');
                })
                .catch(error => {
                    this.$toast.error('Erro ao salvar aluno:', error);
                });
        },
        cancelar() {
            this.$router.push({ name: 'list' });
        },
    },
};

</script>
  