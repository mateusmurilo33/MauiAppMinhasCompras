using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        public ListaProduto()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            lista_produtos.ItemsSource = await App.Database.GetAll();
        }

        private async void OnBuscaTextChanged(object sender, TextChangedEventArgs e)
        {
            string busca = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(busca))
            {
                lista_produtos.ItemsSource = await App.Database.GetAll();
            }
            else
            {
                lista_produtos.ItemsSource = await App.Database.Search(busca);
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

                    lista_produtos.ItemsSource = await App.Database.GetAll();

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