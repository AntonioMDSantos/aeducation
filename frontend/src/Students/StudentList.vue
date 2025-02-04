<template>
  <div class="d-flex justify-center my-8 mt-16">
    <h1>Lista de Alunos</h1>
  </div>
  <v-container class="d-flex justify-center">
    <div>
      <v-row>
        <v-col>
          <v-text-field
            style="width: 40rem;"
            v-model="search"
            label="Pesquisar Alunos"
            append-icon="mdi-magnify"
            clearable
          />
        </v-col>
      </v-row>
  
      <v-data-table
        style="width: 40rem;"
        :headers="headers"
        :items="students"
        item-key="id"
        :items-per-page="5"
        class="elevation-1 "
        :search="search"
        dense
      >
        <template v-slot:item="{ item }">
          <tr>
            <td>{{ item.ra }}</td> 
            <td>{{ item.name }}</td> 
            <td>{{ item.cpf }}</td> 
            <td class="d-flex justify-center">
              <v-btn @click="editStudents(item)" color="blue" icon>
                <v-icon>mdi-pencil</v-icon>
              </v-btn>
              <v-btn @click="openDeleteDialog(item.id)" color="red" icon>
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </td>
          </tr>
        </template>
      </v-data-table>
  
      <v-dialog v-model="deleteDialogVisible" max-width="500px">
        <v-card>
          <v-card-title>
            <span class="headline">Confirmar Exclusão</span>
          </v-card-title>
          <v-card-text>
            Tem certeza que deseja excluir este aluno?
          </v-card-text>
          <v-card-actions>
            <v-btn @click="deleteStudentConfirmed" color="red">Excluir</v-btn>
            <v-btn @click="deleteDialogVisible = false" color="secondary">Cancelar</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
  
      <v-dialog v-model="dialogVisible" max-width="500px">
        <v-card>
          <v-card-title>
            <span class="headline">Editar Aluno</span>
          </v-card-title>
          <v-card-text>
            <v-text-field v-model="currentStudent.name" label="Nome"></v-text-field>
            <v-text-field v-model="currentStudent.ra" label="RA" disabled></v-text-field>
            <v-text-field v-model="currentStudent.cpf" label="CPF" disabled></v-text-field>
            <v-text-field v-model="currentStudent.email" label="Email"></v-text-field>
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" @click="salvarAlteracoes">Salvar</v-btn>
            <v-btn @click="dialogVisible = false">Cancelar</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

    </div>
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
      dialogVisible: false, 
      deleteDialogVisible: false, 
      currentStudent: {}, 
      studentToDeleteId: null, 
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
    editStudents(studentSelect) {
      this.currentStudent = { ...studentSelect }; 
      this.dialogVisible = true; 
    },
    openDeleteDialog(id) {
      this.studentToDeleteId = id;  
      this.deleteDialogVisible = true;
    },
      deleteStudentConfirmed() {
      const apiUrl = process.env.VUE_APP_BACKEND_URL;
      axios
        .delete(`${apiUrl}/api/student/${this.studentToDeleteId}`)
        .then(response => {
          console.log(response)
          this.loadStudents(); 
          this.deleteDialogVisible = false;  
          this.$toast.success('Aluno excluído com sucesso!');
        })
        .catch(error => {
          console.error('Erro ao excluir aluno:', error);
          this.$toast.error('Erro ao excluir aluno.');
        });
    },
    salvarAlteracoes() {
      const apiUrl = process.env.VUE_APP_BACKEND_URL;
      axios
        .put(`${apiUrl}/api/student/${this.currentStudent.id}`, this.currentStudent)
        .then(response => {
          console.log(response)
          this.dialogVisible = false;
          this.loadStudents();
          this.$toast.success('Aluno atualizado com sucesso!');
        })
        .catch(error => {
          console.error('Erro ao salvar as alterações:', error);
          this.$toast.error('Erro ao salvar as alterações.');
        });
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
