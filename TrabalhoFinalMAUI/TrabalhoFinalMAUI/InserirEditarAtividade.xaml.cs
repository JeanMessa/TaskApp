using SQLite;

namespace TrabalhoFinalMAUI;

public partial class InserirEditarAtividade : ContentPage
{
    string caminhoBD;
    SQLiteConnection conexao;
    int cod_atividade;
    bool situacao;
    bool cadastro;
    int cod_usuario;
    public InserirEditarAtividade(int cod_usuario)
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "atividades.db3");
        conexao = new SQLiteConnection(caminhoBD); 
        conexao.CreateTable<Atividade>();
        cadastro = true; 
        this.cod_usuario = cod_usuario;
        pickerPrioridade.SelectedIndex = 0; 
        pickerClassificacao.SelectedIndex = 0; 
        timeInicio.Time = DateTime.Now.TimeOfDay; 
        dateTermino.Date = dateTermino.Date.AddDays(7); 
        timeTermino.Time = new TimeSpan(0, 23, 59, 0); 
    }

    public InserirEditarAtividade(int cod_atividade, int cod_usuario)
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "atividades.db3"); 
        conexao = new SQLiteConnection(caminhoBD); 
        conexao.CreateTable<Atividade>();
        this.cod_atividade = cod_atividade;
        cadastro = false; 
        this.cod_usuario = cod_usuario;
        var atividades = (from registro in conexao.Table<Atividade>() 
                          where registro.cod_atividade == cod_atividade
                          select registro);
        if (atividades.Any())
        {
            Atividade atividade = atividades.First();
            txtTitulo.Text = atividade.titulo;
            txtDescricao.Text = atividade.descricao.ToString();
            pickerPrioridade.SelectedIndex = atividade.prioridade;
            pickerClassificacao.SelectedItem = atividade.classificacao;
            dateInicio.Date = atividade.inicio.Date;
            timeInicio.Time = atividade.inicio.TimeOfDay;
            dateTermino.Date = atividade.termino.Date;
            timeTermino.Time = atividade.termino.TimeOfDay;
            situacao = atividade.situacao;
            lblTitulo.Text = "Editar Atividade";
            btnInserir.Text = "Editar";
        }
    }

    private void btnInserir_Clicked(object sender, EventArgs e) 
    {
        if ((txtTitulo.Text != "" && txtTitulo.Text != null) && (txtDescricao.Text != "" && txtDescricao.Text != null))
        {
            Atividade atividade = new Atividade();
            atividade.titulo = txtTitulo.Text;
            atividade.descricao = txtDescricao.Text;
            atividade.prioridade = pickerPrioridade.SelectedIndex;
            atividade.classificacao = pickerClassificacao.SelectedItem.ToString();
            atividade.inicio = dateInicio.Date;
            atividade.inicio = atividade.inicio.AddHours(timeInicio.Time.Hours);
            atividade.inicio = atividade.inicio.AddMinutes(timeInicio.Time.Minutes);
            atividade.termino = dateTermino.Date;
            atividade.termino = atividade.termino.AddHours(timeTermino.Time.Hours);
            atividade.termino = atividade.termino.AddMinutes(timeTermino.Time.Minutes);
            atividade.cod_usuario = cod_usuario;
            if (cadastro)
            { 
                conexao.Insert(atividade);
                Navigation.PopAsync();
            }
            else
            { 
                atividade.cod_atividade = cod_atividade;
                atividade.situacao = situacao;
                conexao.Update(atividade);
                Navigation.PopAsync();
            }
        }
        else if ((txtTitulo.Text == "" || txtTitulo.Text == null) && (txtDescricao.Text == "" || txtDescricao.Text == null))
        { 
            if (cadastro)
            {
                DisplayAlert("Falha Ao Inserir Atividade", "Os campos de título e descrição devem ser preenchidos.", "OK");
            }
            else
            {
                DisplayAlert("Falha Ao Editar Atividade", "Os campos de título e descrição devem ser preenchidos.", "OK");
            }

            txtTitulo.Focus();
        }
        else if (txtTitulo.Text == "" || txtTitulo.Text == null)
        {
            if (cadastro)
            {
                DisplayAlert("Falha Ao Inserir Atividade", "O campo de título deve ser preenchido.", "OK");
            }
            else
            {
                DisplayAlert("Falha Ao Editar Atividade", "O campo de título deve ser preenchido.", "OK");
            }

            txtTitulo.Focus();
        }
        else if (txtDescricao.Text == "" || txtDescricao.Text == null)
        {
            if (cadastro)
            {
                DisplayAlert("Falha Ao Inserir Atividade", "O campo de descrição deve ser preenchido.", "OK");
            }
            else
            {
                DisplayAlert("Falha Ao Editar Atividade", "O campo de descrição deve ser preenchido.", "OK");
            }
            txtDescricao.Focus();
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

