using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        private Produto produtoEdicao;

        public NovoProduto()
        {
            InitializeComponent();
        }

        public NovoProduto(Produto produto)
        {
            InitializeComponent();

            produtoEdicao = produto;

            txt_descricao.Text = produto.Descricao;
            txt_quantidade.Text = produto.Quantidade.ToString();
            txt_preco.Text = produto.Preco.ToString();
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (produtoEdicao == null)
            {
                Produto produto = new Produto
                {
                    Descricao = txt_descricao.Text,
                    Quantidade = double.Parse(txt_quantidade.Text),
                    Preco = double.Parse(txt_preco.Text)
                };

                await App.Database.Insert(produto);

                await DisplayAlert(
                    "Sucesso",
                    "Produto cadastrado com sucesso!",
                    "OK"
                );
            }
            else
            {
                produtoEdicao.Descricao = txt_descricao.Text;
                produtoEdicao.Quantidade = double.Parse(txt_quantidade.Text);
                produtoEdicao.Preco = double.Parse(txt_preco.Text);

                await App.Database.Update(produtoEdicao);

                await DisplayAlert(
                    "Sucesso",
                    "Produto atualizado com sucesso!",
                    "OK"
                );
            }

            txt_descricao.Text = "";
            txt_quantidade.Text = "";
            txt_preco.Text = "";
        }
    }
}