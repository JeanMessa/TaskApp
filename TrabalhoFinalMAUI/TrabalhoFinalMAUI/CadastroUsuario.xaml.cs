using SQLite;
using TrabalhoFinalMAUI.Model;

namespace TrabalhoFinalMAUI;

public partial class CadastroUsuario : ContentPage
{
    string caminhoBD;
    SQLiteConnection conexao;
    bool permissaoCadastro;
    public CadastroUsuario()
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "atividades.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Usuario>();
    }

    private void btnCadastrar_Clicked(object sender, EventArgs e)
    {
        if ((txtUsuario.Text != "" && txtUsuario.Text != null) && 
            (txtPin.Text != "" && txtPin.Text != null))
        {
            permissaoCadastro = true; 
            var usuario = (from registro in conexao.Table<Usuario>() 
                           where registro.nome == txtUsuario.Text
                           select registro);
            if (usuario.Any()) 
            {
                permissaoCadastro = false;
                DisplayAlert("Nome de Usuário Inválido", "Este nome de usuário já está cadastrado.", "OK");
            }
            if (txtPin.Text != txtConfirmarPin.Text) 
            {
                permissaoCadastro = false;
                DisplayAlert("Falha na Confirmação de PIN", "O campo de PIN e confirmar PIN devem ser iguais.", "OK");
            }
            if (permissaoCadastro)
            {
                Usuario usario = new Usuario();
                usario.nome = txtUsuario.Text;
                usario.pin = txtPin.Text;
                conexao.Insert(usario);
                Navigation.PopAsync(); 
            }
        }
        else if ((txtUsuario.Text == "" || txtUsuario.Text == null) &&
            (txtPin.Text != "" && txtPin.Text != null) &&
            (txtPin.Text != "" && txtPin.Text != null))
        { 
            DisplayAlert("Campo em branco", "Para finalizar o cadastro o campo de nome de usuário deve ser preenchido.", "OK");
        }
        else if ((txtUsuario.Text != "" && txtUsuario.Text != null) &&
            (txtPin.Text == "" || txtPin.Text == null))
        { 
            DisplayAlert("Campo em branco", "Para finalizar o cadastro o campo de PIN deve ser preenchido.", "OK");
        }
        else 
        {
            DisplayAlert("Campos em branco", "Para finalizar o cadastro todos os campos devem ser preenchidos.", "OK");
        }
    }
    private void btnVoltar_Clicked(object sender, EventArgs e) 
    {
        Navigation.PopAsync();
    }
}