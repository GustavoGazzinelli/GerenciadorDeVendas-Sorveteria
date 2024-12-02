using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dapper;
using Microsoft.Data.SqlClient;
using Sorveteria.Controller;
using Sorveteria.Model;

namespace Sorveteria.View
{
    /// <summary>
    /// Lógica interna para AnaliseVendas.xaml
    /// </summary>
    public partial class AnaliseVendas : Window
    {
        private string conexao = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sorveteria;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        int ttVendaPicole = 0;
        int ttVendaSorvete = 0;
        float fatSorvete;
        float fatPicole;
        int ttVenda;
        float ttFat;

        public AnaliseVendas()
        {
            InitializeComponent();
            ttVenda = calculoVendaTotal();
            ttFat = calculoFaturamentoTotal();
        }

        private void botCadastro_Copiar_Click(object sender, RoutedEventArgs e)
        {
            GerenciadorVendas ge = new GerenciadorVendas();
            ge.Show();
            this.Close();
        }

        public int calculoVendaTotal()
        {
            PicoleController control = new PicoleController();
            SorveteController control1 = new SorveteController();

            foreach (var tot in control.listarPicole())
            {
                ttVendaPicole++;   
            }

            foreach (var tot in control1.listarSorvete())
            {
                ttVendaSorvete++;
            }
            return ttVendaSorvete + ttVendaPicole;
        }

        public float calculoFaturamentoTotal()
        {
            using (var con = new SqlConnection(conexao))
            {
                con.Open();
                string sql = "SELECT SUM(p.Quantidade * s.Valor) " +
                    "FROM Picole p " +
                    "INNER JOIN Sabores s ON p.SaborID = s.ID";

                float somaPic = con.ExecuteScalar<float>(sql);

                string sql1 = "SELECT SUM(Valor) FROM Sorvete";
                float somaSor = con.ExecuteScalar<float>(sql1);

                fatPicole = somaPic;
                fatSorvete = somaSor;

                return somaPic + somaSor;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecionadoBox = (boxEscolha.SelectedItem as ComboBoxItem)?.Content.ToString();
            

            if (selecionadoBox == "Tudo")
            {
                totVenda.Content = ttVenda;
                totFat.Content = "R$" + ttFat.ToString("F2");
            }
            if (selecionadoBox == "Sorvete")
            {
                totVenda.Content = ttVendaSorvete;
                totFat.Content = "R$" + fatSorvete.ToString("F2");
            }
            if (selecionadoBox == "Picolé")
            {
                totVenda.Content = ttVendaPicole;
                totFat.Content = "R$" + fatPicole.ToString("F2");
            }
        }
    }
}
