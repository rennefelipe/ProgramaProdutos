using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;

namespace Agenda_Lib.DAO.CEP
{
    public class DadosCEP
    {
        /// <summary>
        ///  Chave de Conexao ao banco de dados
        /// </summary>
        string ChaveConexao = "Data Source=10.39.45.44;" +
            "Initial Catalog=Senac2022;Persist Security Info=True;User " +
            "ID=Turma2022;Password=Turma2022@2022";

        /// <summary>
        /// Processo de consulta de CEP no banco de dados
        /// </summary>
        /// <param name="p_CEP">Informe CEP a ser Pesquisado</param>
        /// <returns> retorno sera o select da base de dados com o valor da consulta</returns>
        public DataSet List_CEP(string p_CEP)
        {
            DataSet DataSetCEP = new DataSet();
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                string wQuery = $"select * from cep where cep = '{p_CEP}'";
                SqlDataAdapter adapter = new SqlDataAdapter(wQuery, Conexao);
                adapter.Fill(DataSetCEP);
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return DataSetCEP;
        }


        /// <summary>
        ///  Apagar registro de CEP na tabela CEP
        /// </summary>
        /// <param name="p_CEP">Informe o CEP que devera ser apaganda.</param>
        public void Apagar_CEP(string p_CEP)
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryDelete = $"delete from cep where cep = '{p_CEP}' ";
                SqlCommand Cmd = new SqlCommand(oQueryDelete, Conexao);
                Cmd.ExecuteNonQuery();
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_cep"></param>
        /// <param name="p_logradouro"></param>
        /// <param name="p_complemento"></param>
        /// <param name="p_bairro"></param>
        /// <param name="p_localidade"></param>
        /// <param name="p_uf"></param>
        /// <param name="p_ibge"></param>
        /// <param name="p_gia"></param>
        /// <param name="p_ddd"></param>
        /// <param name="p_siafi"></param>
        public void Alterar_CEP(
               string p_cep,
               string p_logradouro,
               string p_complemento,
               string p_bairro,
               string p_localidade,
               string p_uf,
               string p_ibge,
               string p_gia,
               string p_ddd,
               string p_siafi
            )
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryUpdate = "UPDATE cep " +
                       $"  SET logradouro  = '{p_logradouro}'    " +
                       $"     ,complemento = '{p_complemento}'    " +
                       $"     ,bairro      = '{p_bairro}'    " +
                       $"     ,localidade  = '{p_localidade}'    " +
                       $"     ,uf          = '{p_uf}'    " +
                       $"     ,ibge        = '{p_ibge}'    " +
                       $"     ,gia         = '{p_gia}'    " +
                       $"     ,ddd         = '{p_ddd}'    " +
                       $"     ,siafi       = '{p_siafi}'    " +
                       $" WHERE cep        = '{p_cep}' ";

                SqlCommand Cmd = new SqlCommand(oQueryUpdate, Conexao);
                Cmd.ExecuteNonQuery();
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Adicionar_CEP(
            string p_cep,
            string p_logradouro,
            string p_complemento,
            string p_bairro,
            string p_localidade,
            string p_uf,
            string p_ibge,
            string p_gia,
            string p_ddd,
            string p_siafi
         )
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryUpdate = "INSERT INTO cep " +
                       $"        ([cep]           " +
                       $"        ,[logradouro]    " +
                       $"        ,[complemento]   " +
                       $"        ,[bairro]        " +
                       $"        ,[localidade]    " +
                       $"        ,[uf]            " +
                       $"        ,[ibge]          " +
                       $"        ,[gia]           " +
                       $"        ,[ddd]           " +
                       $"        ,[siafi])        " +
                       $"  VALUES " +
                       $"        ('{p_cep}'" +
                       $"        ,'{p_logradouro}'" +
                       $"        ,'{p_complemento}'" +
                       $"        ,'{p_bairro}'" +
                       $"        ,'{p_localidade}'" +
                       $"        ,'{p_uf}'" +
                       $"        ,'{p_ibge}'" +
                       $"        ,'{p_gia}'" +
                       $"        ,'{p_ddd}'" +
                       $"        ,'{p_siafi}' )";

                SqlCommand Cmd = new SqlCommand(oQueryUpdate, Conexao);
                Cmd.ExecuteNonQuery();
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        ///  Consulta de CEP no Site Via CEP
        /// </summary>
        /// <param name="p_CEP"> informe CEP a ser pesquisado. </param>
        /// <returns></returns>
        public VIACep PesquisaCEP(string p_CEP)
        {
            VIACep oviaCEP = new VIACep();

            Console.WriteLine(p_CEP);
            try
            {
                string oCEP = p_CEP;
                string oURL = "https://viacep.com.br/ws/" + oCEP + "/json/";
                HttpClient _httpClient = new HttpClient();
                HttpResponseMessage result = _httpClient.GetAsync(oURL).Result;
                String JsonRetorno = result.Content.ReadAsStringAsync().Result;
                oviaCEP = JsonConvert.DeserializeObject<VIACep>(JsonRetorno);

                add_cep(oviaCEP);
            }
            catch (Exception)
            {
                throw;
            }
            return oviaCEP;
        }

        private void add_cep(VIACep oViaCEP)
        {
            DataSet DataSetPesquisa = new DataSet();
            DataSetPesquisa = List_CEP(oViaCEP.cep);

            if (DataSetPesquisa.Tables[0].Rows.Count ==0)
            {
                Adicionar_CEP(
                     oViaCEP.cep,
                     oViaCEP.logradouro,
                     oViaCEP.complemento,
                     oViaCEP.bairro,
                     oViaCEP.localidade,
                     oViaCEP.uf,
                     oViaCEP.ibge,
                     oViaCEP.gia,
                     oViaCEP.ddd,
                     oViaCEP.siafi);
            }
            else
            {
                Console.WriteLine($"Ja existe dados para este cep {oViaCEP.cep} " +
                    $"quantidade de registro {DataSetPesquisa.Tables[0].Rows.Count.ToString()}");   
            }
        }
    }
}
