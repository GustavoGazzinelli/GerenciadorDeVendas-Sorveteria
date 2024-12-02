using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Dapper;
using Microsoft.Data.SqlClient;
using Sorveteria.Model;

namespace Sorveteria.Controller
{
    internal class PicoleController
    {
        private string conexao = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sorveteria;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void salvarPicole(int sabor, int quantidade)
        {
            DateTime dataAtual = DateTime.Now;

            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string cmd = "INSERT INTO Picole (SaborID, Quantidade, Data) VALUES (@SaborID, @Quantidade, @Data)";
                con.Execute(cmd, new { SaborID = sabor, Quantidade = quantidade, Data = dataAtual });
            }
        }

        public void removerPicole(PicoleComSabor selecionado)
        {
            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string sql = "DELETE FROM Picole WHERE Id = @Id";
                con.Execute(sql, new { Id = selecionado.Id });
            }
        }

        public void atualizarPicole(PicoleComSabor selecionado, int sabor, float valor, int quantidade)
        {
            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string sql = "UPDATE Picole SET SaborID = @SaborID, Quantidade = @Quantidade WHERE Id = @Id";
                con.Execute(sql, new { SaborID = sabor, Quantidade = quantidade, Id = selecionado.Id });
            }
        }

        public List<PicoleComSabor> listarPicole()
        {
            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string cmd = "SELECT p.Id, s.Nome, s.Valor, p.Quantidade, p.Data " +
                             "FROM Picole p " +
                             "INNER JOIN Sabores s ON p.SaborID = s.Id";

                var listaPicole = con.Query<PicoleComSabor>(cmd).AsList();
                return listaPicole;
            }
        }
    }
}
