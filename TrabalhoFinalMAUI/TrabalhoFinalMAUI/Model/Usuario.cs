using SQLite;

namespace TrabalhoFinalMAUI.Model
{
    [Table("usuario")]
    internal class Usuario
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int cod_usuario { get; set; } 
        [MaxLength(50), NotNull, Unique]
        public string nome { get; set; }
        [MaxLength(50), NotNull]
        public string pin { get; set; } 
    }
}
