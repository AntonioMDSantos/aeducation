<template>
    <div class="d-flex justify-center mt-10">
        <v-card max-width="800" elevation="1" color="grey-lighten-3" class="pa-10">
            <div class="d-flex justify-center my-8 mt-5">
                <h1 class="font-weight-medium">Cadastro de Alunos</h1>
            </div>
            <div class="d-flex justify-center">
                <v-form style="width: 40rem;" @submit.prevent="saveStudent">
                    <v-text-field prepend-inner-icon="mdi-account-circle" variant="outlined" label="Nome"
                        v-model="aluno.name" :rules="[rules.required]" required />
                    <v-text-field prepend-inner-icon="mdi-at" variant="outlined" label="Email" v-model="aluno.email"
                        :rules="[rules.required]" required />
                    <v-text-field type="number" prepend-inner-icon="mdi-text-box-outline" variant="outlined" label="RA"
                        v-model="aluno.ra" :rules="[rules.required]" required />
                    <v-text-field @input="limitCPF" type="number" prepend-inner-icon="mdi-text-box-search-outline"
                        variant="outlined" label="CPF" v-model="aluno.cpf" :rules="[rules.required]" required
                        @blur="validateCPF" />

                    <div class="d-flex">
                        <div class="w-50">
                            <v-btn class="w-50 py-6 d-flex justify-center" @click="cancel"
                                color="#fa0000">Cancelar</v-btn>
                        </div>
                        <div class="w-50 d-flex justify-end">
                            <v-btn class="w-50 py-6 d-flex justify-center" type="submit" color="#0013fa">Salvar</v-btn>
                        </div>
                    </div>

                </v-form>
            </div>
        </v-card>
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
        limitCPF() {
            if (this.aluno.cpf.length > 11) {
                this.aluno.cpf = this.aluno.cpf.slice(0, 11); 
            }
        },
        validateCPF() {
            const cpf = this.aluno.cpf.replace(/[^\d]/g, '');

            if (cpf.length !== 11) {
                this.$toast.error('CPF inválido! Deve ter 11 dígitos.');
                return false;
            }

            if (/^(\d)\1{10}$/.test(cpf)) {
                this.$toast.error('CPF inválido! Não pode ser uma sequência de números repetidos.');
                return false;
            }
            let sum = 0;
            let multiplier = 10;
            for (let i = 0; i < 9; i++) {
                sum += cpf[i] * multiplier--;
            }
            let firstCheckDigit = 11 - (sum % 11);
            if (firstCheckDigit === 10 || firstCheckDigit === 11) {
                firstCheckDigit = 0;
            }
            if (cpf[9] != firstCheckDigit) {
                this.$toast.error('CPF inválido! Primeiro dígito verificador inválido.');
                return false;
            }
            sum = 0;
            multiplier = 11;
            for (let i = 0; i < 10; i++) {
                sum += cpf[i] * multiplier--;
            }
            let secondCheckDigit = 11 - (sum % 11);
            if (secondCheckDigit === 10 || secondCheckDigit === 11) {
                secondCheckDigit = 0;
            }
            if (cpf[10] != secondCheckDigit) {
                this.$toast.error('CPF inválido! Segundo dígito verificador inválido.');
                return false;
            }

            this.$toast.success('CPF válido!');
            return true;
        },

        async saveStudent() {
            if (!(await this.validateCPF())) return;

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


        cancel() {
            this.$router.push({ name: 'list' });
        },
    },

};

</script>

<style>
.v-field.v-field--prepended {
    background: white !important;
}

input[type='number'] {
    -moz-appearance: textfield;
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none;
}
</style>