using SQLite;
using TrabalhoFinalMAUI.Model;

namespace TrabalhoFinalMAUI;

public partial class Login : ContentPage

{
    string caminhoBD;
    SQLiteConnection conexao;
    public Login()
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "atividades.db3");
        conexao = new SQLiteConnection(caminhoBD); 
        conexao.CreateTable<Usuario>();
    }

    private void btnEntrar_Clicked(object sender, EventArgs e) 
    {
        if ((txtUsuario.Text != "" && txtUsuario.Text != null) &&
            (txtPin.Text != "" && txtPin.Text != null))
        { 
            var usuarios = (from registro in conexao.Table<Usuario>() 
                            where registro.nome == txtUsuario.Text &&
                            registro.pin == txtPin.Text
                            select registro);
            if (usuarios.Any())
            { 
                Usuario usuario = usuarios.First();
                Application.Current.MainPage = new FlyoutPrincipal(usuario.cod_usuario);
            }
            else 
            {
                DisplayAlert("Falha na tentativa de login", "Usuário ou PIN inválido", "OK");
            }
        }
        else if ((txtUsuario.Text == "" || txtUsuario.Text == null) &&
            (txtPin.Text != "" && txtPin.Text != null) &&
            (txtPin.Text != "" && txtPin.Text != null))
        {
            DisplayAlert("Campo em branco", "Preencha o campo de usuário com o seu nome de usuário.", "OK");
        }
        else if ((txtUsuario.Text != "" && txtUsuario.Text != null) &&
            (txtPin.Text == "" || txtPin.Text == null))
        {
            DisplayAlert("Campo em branco", "Preencha o campo de PIN com o seu PIN.", "OK");
        }
        else
        {
            DisplayAlert("Campos em branco", "Preencha os campos de usuário e PIN.", "OK");
        }
    }

    private void btnCriarConta_Clicked(object sender, EventArgs e) 
    {
        Navigation.PushAsync(new CadastroUsuario());
    }
}
