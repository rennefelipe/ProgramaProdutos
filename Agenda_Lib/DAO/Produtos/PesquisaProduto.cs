
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;

namespace Agenda_Lib.DAO.Produtos
{
    public class PesquisaProduto
    {
        /// <summary>
        ///  Chave de Conexao ao banco de dados
        /// </summary>
        string ChaveConexao = "Data Source=10.39.45.44;" +
            "Initial Catalog=Senac2022;Persist Security Info=True;User " +
            "ID=Turma2022;Password=Turma2022@2022";

        /// <summary>
        /// Processo de consulta de Produto no banco de dados
        /// </summary>
        /// <param name="p_CEP">selecione 1 para ser Pesquisado</param>
        /// <returns> retorno sera o select da base de dados com o valor da consulta</returns>
        public DataSet List_Produto(string p_Produto)
        {
            DataSet DataSetProduto = new DataSet();
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                string wQuery = $"select * from Produto where Nome_produto = '{p_Produto}'";
                SqlDataAdapter adapter = new SqlDataAdapter(wQuery, Conexao);
                adapter.Fill(DataSetProduto);
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return DataSetProduto;
        }


        /// <summary>
        ///  Apagar registro de Produto na tabela Produto
        /// </summary>
        /// <param name="p_Produto">Informe o CEP que devera ser apaganda.</param>
        public void Apagar_Produto(string p_Produto)
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryDelete = $"delete from Produto where Nome_produto = '{p_Produto}' ";
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
               string p_Nome_produto,
               string p_Descricao,
               string p_Codigo_Interno,
               string p_Status
               
            )
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryUpdate = "UPDATE cep " +
                       $"  SET Nome_produto  = '{p_Nome_produto}'    " +
                       $"     ,Descricao = '{p_Descricao}'    " +
                       $"     ,Codigo_Intern   = '{p_Codigo_Interno}'    " +
                       $"     ,Status  = '{p_Status}'    " ;

                SqlCommand Cmd = new SqlCommand(oQueryUpdate, Conexao);
                Cmd.ExecuteNonQuery();
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Adicionar_Produto(
            string p_id_produto,
            string p_id_produto_grupo,
            string p_Nome_produto,
            string p_Descricao,
            string p_Codigo_Interno,
            string p_Data_Cadastro,
            string p_Status
            
         )
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryUpdate = "INSERT INTO Produto " +
                       $"        ([id_produto]           " +
                       $"        ,[id_produto_grupo]    " +
                       $"        ,[Nome_produto]   " +
                       $"        ,[Descricao]        " +
                       $"        ,[Codigo_Interno]    " +
                       $"        ,[Data_Cadastro]            " +
                       $"        ,[Status]          " +
                       
                       $"  VALUES " +
                       $"        ('{p_id_produto}'" +
                       $"        ,'{p_id_produto_grupo}'" +
                       $"        ,'{p_Nome_produto}'" +
                       $"        ,'{p_Descricao}'" +
                       $"        ,'{p_Codigo_Interno}'" +
                       $"        ,'{p_Data_Cadastro}'" +
                       $"        ,'{p_Status}'" ;

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
        //public DadosProduto PesquisaProduto(string p_Produto)
        //{
        //    PesquisaProduto oPesquisaProduto = new PesquisaProduto();

        //    Console.WriteLine(p_Produto);
        //    try
        //    {
        //        string oProduto = p_Produto;
        //        string oURL = "" + oProduto + "/json/";
        //        HttpClient _httpClient = new HttpClient();
        //        HttpResponseMessage result = _httpClient.GetAsync(oURL).Result;
        //        String JsonRetorno = result.Content.ReadAsStringAsync().Result;
        //        oviaCEP = JsonConvert.DeserializeObject<VIACep>(JsonRetorno);

        //        add_cep(oviaCEP);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return oviaCEP;
        //}

        //private void add_cep(VIACep oViaCEP)
        //{
        //    DataSet DataSetPesquisa = new DataSet();
        //    DataSetPesquisa = List_CEP(oViaCEP.cep);

        //    if (DataSetPesquisa.Tables[0].Rows.Count == 0)
        //    {
        //        Adicionar_CEP(
        //             oViaCEP.cep,
        //             oViaCEP.logradouro,
        //             oViaCEP.complemento,
        //             oViaCEP.bairro,
        //             oViaCEP.localidade,
        //             oViaCEP.uf,
        //             oViaCEP.ibge,
        //             oViaCEP.gia,
        //             oViaCEP.ddd,
        //             oViaCEP.siafi);
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Ja existe dados para este cep {oViaCEP.cep} " +
        //            $"quantidade de registro {DataSetPesquisa.Tables[0].Rows.Count.ToString()}");
    }
}
 

