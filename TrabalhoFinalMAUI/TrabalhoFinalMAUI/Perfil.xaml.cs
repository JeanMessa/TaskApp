using SQLite;
using TrabalhoFinalMAUI.Model;

namespace TrabalhoFinalMAUI;

public partial class Perfil : ContentPage
{
    string caminhoBD;
    SQLiteConnection conexao;
    int cod_usuario;
    public Perfil(int cod_usuario)
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "atividades.db3"); 
        conexao = new SQLiteConnection(caminhoBD); 
        this.cod_usuario = cod_usuario; 

    }

    public void listar() 
    {
        var usuarios = (from registro in conexao.Table<Usuario>() 
                        where registro.cod_usuario == cod_usuario
                        select registro);
        if (usuarios.Any()) 
        {
            Usuario usuario = usuarios.First();
            lblUsuario.Text = usuario.nome;
        }
        var atividades = (from registro in conexao.Table<Atividade>()
                          where registro.cod_usuario == cod_usuario
                          select registro);
        lblTotal.Text = atividades.Count().ToString();
        atividades = (from registro in conexao.Table<Atividade>()
                      where registro.cod_usuario == cod_usuario
                      && registro.situacao == true
                      select registro);
        lblConcluidas.Text = atividades.Count().ToString();
        atividades = (from registro in conexao.Table<Atividade>()
                      orderby registro.termino
                      where registro.cod_usuario == cod_usuario
                      && registro.situacao == false
                      select registro);
        lblAndamento.Text = atividades.Count().ToString();
        if (atividades.Any())
        {
            Atividade menorprazo = atividades.First();
            lblMenorPrazo.Text = menorprazo.titulo;
        }
        else
        {
            lblMenorPrazo.Text = "Não há atividades em andamento";
        }

        atividades = (from registro in conexao.Table<Atividade>()
                      where registro.cod_usuario == cod_usuario
                      && registro.termino < DateTime.Now
                      select registro);
        lblAtrasadas.Text = atividades.Count().ToString();
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