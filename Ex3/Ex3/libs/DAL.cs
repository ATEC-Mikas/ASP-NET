using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3.libs
{
    public class DAL
    {
        private OleDbConnection _conexao;
        private string _tabela;
        //private Log _logs;
        //private Log _querylogs;

        /// <summary>
        /// Função para dar escape a alguns caracteres
        /// </summary>
        /// <param name="s">String a dar escape</param>
        /// <returns>String escapada</returns>
        private static string EscapeSQL(string s)
        {
            return s.Replace("'", "[']");
        }

        /// <summary>
        /// Construtor para a DAL
        /// </summary>
        /// <param name="tabela">Nome da tablea</param>
        public DAL(string tabela) {
            _conexao = new OleDbConnection();
            _conexao.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\t0114411\Desktop\Asp.Net\Ex3\Ex3\DB\DB.mdb;";
            _tabela = tabela;
            //_logs = new Log($"logs/Erros/{_tabela}.txt");
            //_querylogs = new Log($"logs/Queries/{_tabela}.txt");
        }

        /// <summary>
        /// Função equivalente a um select sem parametros adicionais
        /// </summary>
        /// <param name="s">String com o que quer ir buscar à tabela</param>
        /// <returns>OleDbDataReader com os dados</returns>
        public OleDbDataReader get(string s)
        {
            if (_conexao.State != System.Data.ConnectionState.Open)
            {
                bool connected;
                do
                {
                    connected = false;
                    try
                    {
                        _conexao.Open();
                    }
                    catch (Exception e)
                    {
                        //_logs.EscreverLog(e.Message);
                        connected = true;
                    }
                    if (connected)
                        System.Threading.Thread.Sleep(100);
                } while (connected);
            }


            OleDbCommand comando = new OleDbCommand();
            comando.CommandText = $"SELECT {s} FROM [{_tabela}]";
            //_querylogs.EscreverLog(comando.CommandText);

            comando.Connection = _conexao;
            OleDbDataReader t = comando.ExecuteReader();
            //_conexao.Close();
            return t;
        }

        /// <summary>
        /// Função equivalente a um select com parametros adicionais
        /// </summary>
        /// <param name="s">String com o que quer ir buscar à tabela</param>
        /// <param name="a">String com os parametros adicionais assim como um "where"</param>
        /// <returns>OleDbDataReader com os dados</returns>
        public OleDbDataReader find(string s,string a)
        {
            if (_conexao.State != System.Data.ConnectionState.Open)
            {
                bool connected;
                do
                {
                    connected = false;
                    try
                    {
                        _conexao.Open();
                    }
                    catch (Exception e)
                    {
                        //_logs.EscreverLog(e.Message);
                        connected = true;
                    }
                    if (connected)
                        System.Threading.Thread.Sleep(100);
                } while (connected);
            }

            OleDbCommand comando = new OleDbCommand();
            comando.CommandText = $"SELECT {s} FROM [{_tabela}] {a}";
            //_querylogs.EscreverLog(comando.CommandText);

            comando.Connection = _conexao;
            OleDbDataReader t = comando.ExecuteReader();
            //_conexao.Close();
            return t;
        }

        /// <summary>
        /// Função equivalente a um insert
        /// </summary>
        /// <param name="kv">Lista de KeyValuePair em que a Key corresponde à coluna e o Value ao valor a adicionar</param>
        /// <returns>Boolean do Sucesso da operação</returns>
        public bool insert(List<KeyValuePair<string,object>> kv)
        {
            bool falhou = false;
            if (_conexao.State != System.Data.ConnectionState.Open)
            {
                bool connected;
                do
                {
                    connected = false;
                    try
                    {
                        _conexao.Open();
                    }
                    catch (Exception e)
                    {
                        //_logs.EscreverLog(e.Message);
                        connected = true;
                    }
                    if (connected)
                        System.Threading.Thread.Sleep(100);
                } while (connected);
            }

            string id = string.Empty;
            string valor = string.Empty;
            for (int i = 0; i < kv.Count; i++)
            {
                id += $"[{kv[i].Key}]";
                if(kv[i].Value.GetType() == typeof(string))
                     valor += $"'{kv[i].Value}'";
                else
                     valor += kv[i].Value;

                if (i != kv.Count - 1)
                {
                    id += ",";
                    valor += ",";
                }

            }


            OleDbCommand comando = new OleDbCommand();
            comando.Connection = _conexao;
            comando.CommandText = $"INSERT INTO [{_tabela}]({id}) VALUES ({valor})";
            //_querylogs.EscreverLog(comando.CommandText);
            try {
                comando.ExecuteNonQuery();     
            }
            catch(Exception e)
            {
                falhou = true;
                //_logs.EscreverLog(e.Message);
            }
            _conexao.Close();

            return !falhou;
        }

        /// <summary>
        /// Função equivalente a um update
        /// </summary>
        /// <param name="kv">Lista de KeyValuePair em que a Key corresponde à coluna e o Value ao valor a adicionar</param>
        /// <param name="a">Query que determina o que vai ser afetado Exemplo: "where id==1"</param>
        /// <returns>Boolean do Sucesso da operação</returns>
        public bool update(List<KeyValuePair<string, object>> kv, string a)
        {
            bool falhou = false;
            if (_conexao.State != System.Data.ConnectionState.Open)
            {
                bool connected;
                do
                {
                    connected = false;
                    try
                    {
                        _conexao.Open();
                    }
                    catch (Exception e)
                    {
                        //_logs.EscreverLog(e.Message);
                        connected = true;
                    }
                    if (connected)
                        System.Threading.Thread.Sleep(100);
                } while (connected);
            }

            string set = string.Empty;
            for (int i = 0; i < kv.Count; i++)
            {
                if (kv[i].Value.GetType() == typeof(string))
                    set += $"[{kv[i].Key}] = '{kv[i].Value}'";
                else
                    set += $"[{kv[i].Key}] = {kv[i].Value}";

                if (i != kv.Count - 1)
                {
                    set += ",";
                }
            }


            OleDbCommand comando = new OleDbCommand();
            comando.Connection = _conexao;
            comando.CommandText = $"UPDATE [{_tabela}] SET {set} {a}";
            //_querylogs.EscreverLog(comando.CommandText);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                falhou = true;
                //_logs.EscreverLog(e.Message);
            }
            _conexao.Close();

            return !falhou;

        }

        /// <summary>
        /// Função equivalente a um delete
        /// </summary>
        /// <param name="a">Query que determina o que vai ser apagado Exemplo: "where id==1"</param>
        /// <returns>Boolean do Sucesso da operação</returns>
        public bool delete(string a)
        {
            bool falhou = false;
            if (_conexao.State != System.Data.ConnectionState.Open)
            {
                bool connected;
                do
                {
                    connected = false;
                    try
                    {
                        _conexao.Open();
                    }
                    catch (Exception e)
                    {
                        //_logs.EscreverLog(e.Message);
                        connected = true;
                    }
                    if (connected)
                        System.Threading.Thread.Sleep(100);
                } while (connected);
            }

            OleDbCommand comando = new OleDbCommand();
            comando.Connection = _conexao;
            comando.CommandText = $"delete * from [{_tabela}] {a}";
            //_querylogs.EscreverLog(comando.CommandText);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                falhou = true;
                //_logs.EscreverLog(e.Message);
            }
            _conexao.Close();

            return !falhou;

        }

        /// <summary>
        /// Função que verifica se existe na base de dados
        /// </summary>
        /// <param name="a">Query que determina o que é verificado Exemplo: "where id==1"</param>
        /// <returns>Boolean de existência</returns>
        public bool exists(string a)
        {
            if (_conexao.State != System.Data.ConnectionState.Open)
            {
                bool connected;
                do
                {
                    connected = false;
                    try
                    {
                        _conexao.Open();
                    }
                    catch (Exception e)
                    {
                        //_logs.EscreverLog(e.Message);
                        connected = true;
                    }
                    if (connected)
                        System.Threading.Thread.Sleep(100);
                } while (connected);
            }


            OleDbCommand comando = new OleDbCommand();
            comando.Connection = _conexao;
            comando.CommandText = $"SELECT * FROM [{_tabela}] {a}";
            //_querylogs.EscreverLog(comando.CommandText);
            OleDbDataReader t = comando.ExecuteReader();
            if(t.HasRows)
            {
                _conexao.Close();
                return true;
            }
            else
            {
                _conexao.Close();
                return false;
            }

        }




    }
}
