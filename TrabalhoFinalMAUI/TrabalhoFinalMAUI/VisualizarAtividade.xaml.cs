using SQLite;
using TrabalhoFinalMAUI.Converter;

namespace TrabalhoFinalMAUI;

public partial class VisualizarAtividade : ContentPage
{
    string caminhoBD;
    SQLiteConnection conexao;
    int cod_atividade;
    int cod_usuario;
    Atividade atividade;
    public VisualizarAtividade(int cod_atividade, int cod_usuario)
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "atividades.db3");
        conexao = new SQLiteConnection(caminhoBD); 
        this.cod_atividade = cod_atividade;
        this.cod_usuario = cod_usuario;
        conexao.CreateTable<Atividade>(); 
        listar();
    }

    public void listar() 
    {
        var atividades = (from registro in conexao.Table<Atividade>() 
                          where registro.cod_atividade == cod_atividade
                          select registro);
        if (atividades.Any()) 
        {
            atividade = atividades.First();
            lblTitulo.Text = atividade.titulo;
            if (atividade.prioridade == 0)
            {
                lblPrioridade.Text = "Baixa";
            }
            else if (atividade.prioridade == 1)
            {
                lblPrioridade.Text = "Média";
            }
            else
            {
                lblPrioridade.Text = "Alta";
            }
            lblClassificacao.Text = atividade.classificacao;
            lblInicio.Text = atividade.inicio.ToString();
            lblTermino.Text = atividade.termino.ToString();
            ConverterTerminoPrazo converterTerminoPrazo = new ConverterTerminoPrazo();
            lblPrazo.Text = (converterTerminoPrazo.Convert([atividade.situacao, atividade.termino], null, null, null)).ToString();
            lblDescricao.Text = atividade.descricao.ToString();
            checkSituacao.IsChecked = atividade.situacao;
            if (checkSituacao.IsChecked == true)
            {
                lblSituacao.Text = "Concluída";
            }
            else
            {
                lblSituacao.Text = "Em andamento";
            }
        }
    }

    private void checkSituacao_CheckedChanged(object sender, CheckedChangedEventArgs e) 
    {

        if (atividade.situacao != checkSituacao.IsChecked)
        {
            if (checkSituacao.IsChecked == true)
            {
                lblSituacao.Text = "Concluida";
            }
            else
            {
                lblSituacao.Text = "Em andamento";
            }
            atividade.situacao = checkSituacao.IsChecked;
            conexao.Update(atividade);
            ConverterTerminoPrazo converterTerminoPrazo = new ConverterTerminoPrazo();
            lblPrazo.Text = (converterTerminoPrazo.Convert([atividade.situacao, atividade.termino], null, null, null)).ToString();
        }
    }

    private void btnEditar_Clicked(object sender, EventArgs e) 
    {
        InserirEditarAtividade inserirEditarAtividade = new InserirEditarAtividade(cod_atividade, cod_usuario);
        Navigation.PushAsync(inserirEditarAtividade);
    }

    private async void btnExcluir_Clicked(object sender, EventArgs e) 
    {
        bool resposta = await DisplayAlert("Exclusão", "Deseja mesmo excluir essa atividade", "Sim", "Não");
        if (resposta)
        {
            conexao.Delete(atividade);
            Navigation.PopAsync();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        listar();
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
        FlyoutPage flyoutPage = ((FlyoutPage)App.Current.MainPage);
        flyoutPage.IsPresented = true;
    }

    private void btnVoltar_Clicked(object sender, EventArgs e) 
    {
        Navigation.PopAsync();
    }
}