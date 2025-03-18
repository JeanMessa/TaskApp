namespace TrabalhoFinalMAUI;

public partial class FlyoutPrincipal : FlyoutPage
{
    int cod_usuario;
    public FlyoutPrincipal(int cod_usuario)
    {
        InitializeComponent();
        this.cod_usuario = cod_usuario;
        this.Detail = new NavigationPage(new MinhasAtividades(cod_usuario));
        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            imgPerfil.Source = "profile_dark.png";
            imgAtividades.Source = "clipboard_dark.png";
            imgAdd.Source = "add_dark.png";
            imgSair.Source = "logout_dark.png";
        }
        Application.Current.RequestedThemeChanged += (sender, arguments) => 
        {
            if (Application.Current.RequestedTheme == AppTheme.Dark) 
            {
                imgPerfil.Source = "profile_dark.png";
                imgAtividades.Source = "clipboard_dark.png";
                imgAdd.Source = "add_dark.png";
                imgSair.Source = "logout_dark.png";
            }
            else
            {
                imgPerfil.Source = "profile.png";
                imgAtividades.Source = "clipboard.png";
                imgAdd.Source = "add.png";
                imgSair.Source = "logout.png";
            }

        };
    }

    private void Perfil_Tapped(object sender, TappedEventArgs e)
    {
        NavigationPage navigationPage = (NavigationPage)this.Detail;
        navigationPage.PushAsync(new Perfil(cod_usuario));
        this.IsPresented = false;
    }

    private void Atividades_Tapped(object sender, TappedEventArgs e)
    {
        NavigationPage navigationPage = (NavigationPage)this.Detail;
        navigationPage.PushAsync(new MinhasAtividades(cod_usuario));
        this.IsPresented = false;
    }

    private void Inserir_Tapped(object sender, TappedEventArgs e)
    {
        NavigationPage navigationPage = (NavigationPage)this.Detail;
        InserirEditarAtividade inserirEditarAtividade = new InserirEditarAtividade(cod_usuario);
        navigationPage.PushAsync(inserirEditarAtividade);
        this.IsPresented = false;
    }

    private void Sair_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Login());
    }


}