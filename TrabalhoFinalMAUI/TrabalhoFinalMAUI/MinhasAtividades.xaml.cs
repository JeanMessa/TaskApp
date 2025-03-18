using SQLite;
using TrabalhoFinalMAUI.Converter;

namespace TrabalhoFinalMAUI;

public partial class MinhasAtividades : ContentPage
{
    string caminhoBD;
    SQLiteConnection conexao;
    int cod_usuario;

    public MinhasAtividades(int cod_usuario)
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "atividades.db3"); 
        conexao = new SQLiteConnection(caminhoBD); 
        conexao.CreateTable<Atividade>();
        this.cod_usuario = cod_usuario;
        pickerFiltro.SelectedIndex = 0;
        Application.Current.RequestedThemeChanged += (sender, arguments) => 
        {
            listarAtividades();
        };

    }

    public Task listarAtividades() 
    {
        ConverterTerminoPrazo converter = new ConverterTerminoPrazo();
        var atividades = from registro in conexao.Table<Atividade>() 
                         orderby registro.situacao, 
                         registro.prioridade descending, 
                         registro.termino ascending 
                         where registro.cod_usuario == cod_usuario && 
                         (pickerFiltro.SelectedIndex == 0 || 
                         ((pickerFiltro.SelectedIndex == 1 && (registro.termino < DateTime.Now)) && (registro.situacao == false) || 
                         (pickerFiltro.SelectedIndex == 2 && (registro.termino >= DateTime.Now)) && (registro.situacao == false) || 
                         (pickerFiltro.SelectedIndex == 3 && registro.situacao == false)) || 
                         (pickerFiltro.SelectedIndex == 4 && registro.situacao == true)) 
                         select registro;
        collectionViewAtividades.ItemsSource = atividades;
#if ANDROID
        int tamanhoAtividades = 70;
        if (atividades.Any())
        {
            tamanhoAtividades = atividades.Count() * 70;
        }
        RefreshViewAtividades.HeightRequest = 50 + tamanhoAtividades;
#endif
        return Task.FromResult(true);
    }

    private void BtnAdd_Clicked(object sender, EventArgs e) 
    {
        InserirEditarAtividade inserirEditarAtividade = new InserirEditarAtividade(cod_usuario);
        Navigation.PushAsync(inserirEditarAtividade);
    }

    private void checkSituacao_CheckedChanged(object sender, CheckedChangedEventArgs e) 
    {
        CheckBox check = (CheckBox)sender;
        if (check.BindingContext is Atividade atividade)
        {
            atividade.situacao = check.IsChecked;
            conexao.Update(atividade);
        }
    }


    private void AtualizarCollectionViewAtividades(object sender, SwipedEventArgs e) 
    {
        RefreshViewAtividades.IsRefreshing = true;

    }

    private async void RefreshViewAtividades_Refreshing(object sender, EventArgs e) 
    {
        await Task.Delay(10);
        await listarAtividades();
        RefreshViewAtividades.IsRefreshing = false;
    }

    private void btnVerMais_Clicked(object sender, EventArgs e) 
    {
        listarAtividades();
        ImageButton btnVerMais = (ImageButton)sender;
        if (btnVerMais.BindingContext is Atividade atividade)
        {
            VisualizarAtividade visualizarAtividade = new VisualizarAtividade(atividade.cod_atividade, cod_usuario);
            Navigation.PushAsync(visualizarAtividade);
        }

    }

    private void pickerFiltro_SelectedIndexChanged(object sender, EventArgs e)
        listarAtividades();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        listarAtividades(); 
        NavigationPage navigationPage = (NavigationPage)(((FlyoutPage)(App.Current.MainPage)).Detail);
        if (navigationPage.RootPage == this) 
        {
            btnVoltar.IsVisible = false;
        }


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