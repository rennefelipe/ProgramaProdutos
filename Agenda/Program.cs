using System;
using System.Data;
using Agenda_Lib.DAO.CEP;
using Agenda_Lib.DAO.Produtos;
namespace Agenda
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Digite 1 para lista de produtos: ");
            Console.WriteLine("Digite 2 para Cadastra produtos: ");
            Console.WriteLine("Digite 3 para alterar produtos: ");
            Console.WriteLine("Digite 4 para excluir produtos: ");
            List_Produto();
        }

        static void List_Produto()
        {
            string ItemSelecionado = Console.ReadLine();
            PesquisaProduto OProduto = new PesquisaProduto();
            DataSet OListProduto = new DataSet();

            switch (ItemSelecionado.ToUpper())
            {
                case "1":
                    break;
                    
                default:
                    Console.WriteLine("Lista de Produtos: ");
                    break;
            }
            try
            {
                

            }
            catch (Exception)
            {

                throw;
            }

            //switch (ItemSelecionado.ToUpper())
            //{
            //    case "2":
            //        break;

            //    default:
            //        Console.WriteLine("Cadastro de Produtos: ");
            //        break;
            //}

            //switch (ItemSelecionado.ToUpper())
            //{
            //    case "3":
            //        break;

            //    default:
            //        Console.WriteLine("Editar Produtos: ");
            //        break;
            //}

            //switch (ItemSelecionado.ToUpper())
            //{
            //    case "4":
            //        break;

            //    default:
            //        Console.WriteLine("Excluir Produtos: ");
            //        break;
            //}


        }
    }
}

