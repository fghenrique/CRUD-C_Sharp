using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Drawing;

namespace Cadastro
{
    class DAO
    {
        static string serverName = "192.168.0.130";
        static string porta = "5432";
        static string userName = "fgh";
        static string senha = "fgh4531";
        static string database = "banco";
        NpgsqlConnection pgConnection = null;
        string conexaoString = null;

        public DAO()
        {
            conexaoString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4}",
                serverName, porta, userName, senha, database);
        }


        public void salvarCadastro(string nome, string email, int idade)
        {

            try
            {
                using (pgConnection = new NpgsqlConnection(conexaoString))
                {
                    pgConnection.Open();

                    string sql = String.Format("insert into cadastro(nome, email, idade) values('{0}', '{1}', {2})",
                        nome, email, idade);

                    using (NpgsqlCommand pgComando = new NpgsqlCommand(sql, pgConnection))
                    {
                        pgComando.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                this.pgConnection.Close();
            }

        }

        public DataTable listarTodosRegistros()
        {
            DataTable dt = new DataTable();
            try
            {
                using(pgConnection = new NpgsqlConnection(conexaoString))
                {
                    pgConnection.Open();
                    string sql = "SELECT * FROM cadastro ORDER BY nome";

                    using(NpgsqlDataAdapter adpt = new NpgsqlDataAdapter(sql, pgConnection))
                    {
                        adpt.Fill(dt);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgConnection.Close();
            }
            return dt;
        }

        public void alterarCadastro(int id, string nome, string email, int idade)
        {
            try
            {
                using(pgConnection = new NpgsqlConnection(conexaoString))
                {
                    pgConnection.Open();

                    string sqlAlteracao = String.Format("UPDATE cadastro SET nome = '{0}', email = '{1}', idade = {2} " +
                        "WHERE id = {3}", nome, email, idade, id);
                    using(NpgsqlCommand pgsqlCommand = new NpgsqlCommand(sqlAlteracao, pgConnection))
                    {
                        pgsqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                pgConnection.Close();
            }
        }

        public void excluirRegistro(int id)
        {
            try
            {
                using(pgConnection = new NpgsqlConnection(conexaoString))
                {
                    pgConnection.Open();

                    String sqlExcluir = String.Format("DELETE FROM cadastro WHERE id = {0}", id);

                    using(NpgsqlCommand pgsqlComando = new NpgsqlCommand(sqlExcluir, pgConnection))
                    {
                        pgsqlComando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                pgConnection.Close();
            }
        }
    }
}
