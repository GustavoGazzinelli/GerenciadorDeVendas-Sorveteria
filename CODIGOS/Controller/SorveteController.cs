using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Sorveteria.Model;
using Dapper;

namespace Sorveteria.Controller
{
    internal class SorveteController
    {
        private string conexao = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sorveteria;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void salvarSorvete(float peso, decimal valor)
        {
            DateTime dataAtual = DateTime.Now;

            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string cmd = "INSERT INTO Sorvete (Peso, Valor, Data) VALUES (@Peso, @Valor, @Data)";
                con.Execute(cmd, new { Peso = peso, Valor = valor, Data = dataAtual });
            }
        }

        public void removerSorvete(Sorvete selecionado)
        {
            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string sql = "DELETE FROM Sorvete WHERE Id = @Id";
                con.Execute(sql, new { Id = selecionado.Id });
            }
        }

        public void atualizarSorvete(Sorvete selecionado, float peso, decimal valor)
        {
            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string sql = "UPDATE Sorvete SET Peso = @Peso, Valor = @Valor WHERE Id = @Id";
                con.Execute(sql, new { Peso = peso, Valor = valor, Id = selecionado.Id });
            }
        }

        public List<Sorvete> listarSorvete()
        {
            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string cmd = "SELECT Id, Peso, Valor, Data FROM Sorvete";
                var listaSorvete = con.Query<Sorvete>(cmd).AsList();
                return listaSorvete;
            }
        }
    }
}
