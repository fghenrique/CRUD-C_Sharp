using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro
{
    public partial class Form1 : Form
    {
        int id;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string email = txtEmail.Text;
            int idade = int.Parse(txtIdade.Text);

            DAO dao = new DAO();
            dao.salvarCadastro(nome, email, idade);
            MessageBox.Show("Salvo com sucesso!!");
            atualizaGrid();
            limparcampos();

        }

           private void dataGrid_DoubleClick(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGrid.CurrentRow.Cells[0].Value);
            txtNome.Text = Convert.ToString(dataGrid.CurrentRow.Cells[1].Value);
            txtIdade.Text = Convert.ToString(dataGrid.CurrentRow.Cells[2].Value);
            txtEmail.Text = Convert.ToString(dataGrid.CurrentRow.Cells[3].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DAO dao = new DAO();
            dao.alterarCadastro(id, txtNome.Text, txtEmail.Text, int.Parse(txtIdade.Text));
            MessageBox.Show("Alterado com sucesso!!");
            atualizaGrid();
            limparcampos();
        }

        public void limparcampos()
        {
            txtEmail.Text = "";
            txtIdade.Text = "";
            txtNome.Text = "";
        }

        public void atualizaGrid()
        {
            DAO dao = new DAO();
            dataGrid.DataSource = dao.listarTodosRegistros();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            atualizaGrid();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DAO dao = new DAO();
            dao.excluirRegistro(id);
            MessageBox.Show("Excluido com sucesso!!");
            atualizaGrid();
            limparcampos();
        }
    }
}
