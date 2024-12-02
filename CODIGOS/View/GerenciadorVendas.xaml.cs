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
using System.Windows.Shapes;
using Dapper;
using Microsoft.Data.SqlClient;
using Sorveteria.Controller;
using Sorveteria.Model;

namespace Sorveteria.View
{
    /// <summary>
    /// Lógica interna para GerenciadorVendas.xaml
    /// </summary>
    public partial class GerenciadorVendas : Window
    {
        PicoleController conPicole;
        SorveteController conSorvete;
        private string conexao = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sorveteria;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public GerenciadorVendas()
        {
            InitializeComponent();
            conPicole = new PicoleController();
            conSorvete = new SorveteController();
            carregarSabores();
        }

        public void listar()
        {
            dataPicole.ItemsSource = conPicole.listarPicole();
            dataSorvete.ItemsSource = conSorvete.listarSorvete();
        }

        public void carregarSabores()
        {
            using (var con = new SqlConnection(conexao))
            {
                string query = "SELECT Id, Nome, Valor FROM Sabores";
                var listaSabor = con.Query<Sabores>(query).AsList();

                boxSabor.ItemsSource = listaSabor;
                boxSabor.SelectedIndex = 0;
            }
        }

        private void botCadastro_Click_1(object sender, RoutedEventArgs e)
        {
            var selecionadoBox = (boxEscolha.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selecionadoBox == "Sorvete")
            {
                if ((string.IsNullOrEmpty(boxPeso.Text) || string.IsNullOrEmpty(boxValor.Text)))
                {
                    MessageBox.Show("Preencha todos os campos para inserir uma venda", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                conSorvete.salvarSorvete(float.Parse(boxPeso.Text), decimal.Parse(boxValor.Text));
                MessageBox.Show("Venda inserida com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (boxSabor.SelectedItem is Sabores saborSelecionado)
            {
                if (selecionadoBox == "Picolé")
                {
                    if (string.IsNullOrEmpty(boxSabor.Text) || string.IsNullOrEmpty(boxQuantidade.Text))
                    {
                        MessageBox.Show("Preencha todos os campos para inserir uma venda", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    conPicole.salvarPicole(saborSelecionado.Id, int.Parse(boxQuantidade.Text));
                    MessageBox.Show("Venda inserida com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            boxPeso.Clear();
            boxValor.Clear();
            boxQuantidade.Clear();
            listar();
        }

        private void botCadastro_Copiar_Click(object sender, RoutedEventArgs e)
        {
            AnaliseVendas analise = new AnaliseVendas();
            analise.Show();
            this.Close();
        }

        private void botRemover_Click(object sender, RoutedEventArgs e)
        {
            var selecionadoPicole = (PicoleComSabor)dataPicole.SelectedItem;
            var selecionadoSorvete = (Sorvete)dataSorvete.SelectedItem;
            var selecionadoBox = (boxEscolha.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selecionadoPicole != null || selecionadoSorvete != null)
            {
                if (selecionadoBox == "Sorvete")
                {
                    conSorvete.removerSorvete(selecionadoSorvete);
                    MessageBox.Show("Venda removida com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (selecionadoBox == "Picolé")
                {
                    conPicole.removerPicole(selecionadoPicole);
                    MessageBox.Show("Venda removida com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Erro na Remoção!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                listar();
            }
            else
            {
                MessageBox.Show("Selecione uma venda!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void botAtualizar_Click(object sender, RoutedEventArgs e)
        {
            var selecionadoBox = (boxEscolha.SelectedItem as ComboBoxItem)?.Content.ToString();
            var selecionadoPicole = (PicoleComSabor)dataPicole.SelectedItem;
            var selecionadoSorvete = (Sorvete)dataSorvete.SelectedItem;

            if (selecionadoBox == "Sorvete")
            {
                if (string.IsNullOrEmpty(boxPeso.Text) && (string.IsNullOrEmpty(boxValor.Text)))
                {
                    MessageBox.Show("Preencha todos os campos para atualizar uma venda", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                conSorvete.atualizarSorvete(selecionadoSorvete, float.Parse(boxPeso.Text), decimal.Parse(boxValor.Text));
                MessageBox.Show("Venda atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (boxSabor.SelectedItem is Sabores saborSelecionado)
            {
                if (selecionadoBox == "Picolé")
                {
                    if (string.IsNullOrEmpty(boxSabor.Text) && (string.IsNullOrEmpty(boxQuantidade.Text)))
                    {
                        MessageBox.Show("Preencha todos os campos para atualizar uma venda", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    conPicole.atualizarPicole(selecionadoPicole, saborSelecionado.Id, saborSelecionado.Valor, int.Parse(boxQuantidade.Text));
                    MessageBox.Show("Venda atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            boxPeso.Clear();
            boxValor.Clear();
            boxQuantidade.Clear();
            listar();
        }

        private void boxEscolha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecionado = (boxEscolha.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selecionado == "Sorvete")
            {
                label1.Content = "Peso:";
                boxPeso.Visibility = Visibility.Visible;
                boxSabor.Visibility = Visibility.Collapsed;
                dataSorvete.Visibility = Visibility.Visible;
                dataPicole.Visibility = Visibility.Collapsed;
                boxValor.Visibility = Visibility.Visible;
                labelValor.Visibility = Visibility.Visible;
                boxQuantidade.Visibility = Visibility.Collapsed;
                label2.Visibility = Visibility.Collapsed;
                boxValor.Clear();
                listar();
            }
            if (selecionado == "Picolé")
            {
                label1.Content = "Sabor:";
                boxPeso.Visibility = Visibility.Collapsed;
                boxSabor.Visibility = Visibility.Visible;
                dataSorvete.Visibility = Visibility.Collapsed;
                dataPicole.Visibility = Visibility.Visible;
                boxValor.Visibility = Visibility.Collapsed;
                labelValor.Visibility = Visibility.Collapsed;
                boxQuantidade.Visibility = Visibility.Visible;
                label2.Visibility = Visibility.Visible;
                boxPeso.Clear();
                boxValor.Clear();
                boxQuantidade.Clear();
                listar();
            }
        }
    }
}
