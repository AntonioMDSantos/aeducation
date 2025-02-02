<template>

  <div class="d-flex justify-center my-8">
    <h1>Lista de Alunos</h1>
</div>
  <v-container>
    <v-row>
      <v-col>
        <v-text-field
          v-model="search"
          label="Pesquisar Alunos"
          append-icon="mdi-magnify"
          clearable
        />
      </v-col>
    </v-row>

    <v-data-table
      :headers="headers"
      :items="students"
      item-key="id"
      :items-per-page="5"
      class="elevation-1"
      :search="search"
      dense
    >
      <template v-slot:item="{ item }">
        <tr>
          <td>{{ item.ra }}</td> 
          <td>{{ item.name }}</td> 
          <td>{{ item.cpf }}</td> 
          <td class="d-flex justify-center">
            <v-btn @click="editarAluno(item)" color="blue" icon>
              <v-icon>mdi-pencil</v-icon>
            </v-btn>
            <v-btn @click="excluirAluno(item.id)" color="red" icon>
              <v-icon>mdi-delete</v-icon>
            </v-btn>
          </td>
        </tr>
      </template>
    </v-data-table>
  </v-container>
</template>

<script>
import axios from 'axios';

export default {
  name: 'StudentList',
  data() {
    return {
      students: [],
      search: '',
      headers: [
        { title: 'RA', align: 'start', key: 'ra', sortable: true },
        { title: 'Nome', align: 'start', key: 'name', sortable: true },
        { title: 'CPF', align: 'start', key: 'cpf', sortable: true },
        { title: 'Ações', align: 'center', key: 'actions' },
      ],
    };
  },
  mounted() {
    this.loadStudents();
  },
  methods: {
    loadStudents() {
      const apiUrl = process.env.VUE_APP_BACKEND_URL;
      axios
        .get(`${apiUrl}/api/student`)
        .then(response => {
          this.students = response.data;
        })
        .catch(error => {
          console.error('Erro ao carregar alunos:', error);
        });
    },
    editarAluno(aluno) {
      this.$router.push({ name: 'editar', params: { id: aluno.id } });
    },
    excluirAluno(id) {
      if (confirm('Tem certeza que deseja excluir este aluno?')) {
        const apiUrl = process.env.VUE_APP_BACKEND_URL;
        axios
          .delete(`${apiUrl}/api/student/${id}`)
          .then(response => {
            console.log(response)
            this.loadStudents();
            alert('Aluno excluído com sucesso!');
          })
          .catch(error => {
            console.error('Erro ao excluir aluno:', error);
            alert('Erro ao excluir aluno.');
          });
      }
    },
  },
};
</script>

<style scoped>
.v-data-table th {
  background-color: #f5f5f5;
  font-weight: bold;
}

.v-data-table td {
  font-size: 14px;
}

.v-btn {
  margin-right: 5px;
}

.v-text-field {
  margin-bottom: 20px;
}
</style>
