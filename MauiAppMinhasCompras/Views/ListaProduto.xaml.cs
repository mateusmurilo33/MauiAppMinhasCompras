using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> produtos = new ObservableCollection<Produto>();

        public ListaProduto()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var lista = await App.Database.GetAll();

            produtos.Clear();

            foreach (var produto in lista)
            {
                produtos.Add(produto);
            }

            lista_produtos.ItemsSource = produtos;
        }

        private async void OnBuscaTextChanged(object sender, TextChangedEventArgs e)
        {
            string busca = e.NewTextValue;

            var lista = string.IsNullOrWhiteSpace(busca)
                ? await App.Database.GetAll()
                : await App.Database.Search(busca);

            produtos.Clear();

            foreach (var produto in lista)
            {
                produtos.Add(produto);
            }
        }

        private async void OnProdutoSelecionado(object sender, SelectionChangedEventArgs e)
        {
            Produto produtoSelecionado = e.CurrentSelection.FirstOrDefault() as Produto;

            if (produtoSelecionado != null)
            {
                string acao = await DisplayActionSheet(
                    "Escolha uma opção",
                    "Cancelar",
                    null,
                    "Editar",
                    "Excluir"
                );

                if (acao == "Editar")
                {
                    await Navigation.PushAsync(
                        new NovoProduto(produtoSelecionado)
                    );
                }

                if (acao == "Excluir")
                {
                    await App.Database.Delete(produtoSelecionado.Id);

                    produtos.Remove(produtoSelecionado);

                    await DisplayAlert(
                        "Sucesso",
                        "Produto excluído com sucesso!",
                        "OK"
                    );
                }

                lista_produtos.SelectedItem = null;
            }
        }
    }
}