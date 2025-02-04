<template>
    <div class="d-flex justify-center my-8 mt-16">
        <h1>Cadastro de Alunos</h1>
    </div>
    <div class="d-flex justify-center">
        <v-form style="width: 40rem;" @submit.prevent="salvarAluno">
            <v-text-field variant="outlined" label="Nome" v-model="aluno.name" :rules="[rules.required]" required />
            <v-text-field variant="outlined" label="Email" v-model="aluno.email" :rules="[rules.required]" required />
            <v-text-field variant="outlined" label="RA" v-model="aluno.ra" :rules="[rules.required]" required />
            <v-text-field variant="outlined" label="CPF" v-model="aluno.cpf" :rules="[rules.required]" required />
            <div class="d-flex">
                <div class="w-50">
                    <v-btn class="w-50 py-6 d-flex justify-center" @click="cancelar" color="#fa0000">Cancelar</v-btn>
                </div>
                <div class="w-50 d-flex justify-end">
                    <v-btn class="w-50 py-6 d-flex justify-center" type="submit" color="#0013fa">Salvar</v-btn>
                </div>
            </div>

        </v-form>
    </div>
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