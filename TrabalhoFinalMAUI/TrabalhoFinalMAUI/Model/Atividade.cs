using SQLite;


namespace TrabalhoFinalMAUI
{

    [Table("atividade")]
    internal class Atividade
    {
        public Atividade()
        {
            situacao = false;
        }

        [PrimaryKey, AutoIncrement, NotNull]
        public int cod_atividade { get; set; } 
        [MaxLength(50), NotNull]
        public string titulo { get; set; } 
        [MaxLength(1000), NotNull]
        public string descricao { get; set; } 
        [NotNull]
        public int prioridade { get; set; } 
        [MaxLength(12), NotNull]
        public string classificacao { get; set; } 
        [NotNull]
        public DateTime inicio { get; set; } 
        [NotNull]
        public DateTime termino { get; set; } 
        [NotNull]
        public bool situacao { get; set; }
        public int cod_usuario { get; set; }
    }
}
