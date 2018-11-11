namespace ADondeIr.Model
{
    using System.Collections.Generic;
    using NPoco;

    [PrimaryKey("pkUsuario")]
    public class Usuario : BaseEntity
    {
        public int pkUsuario { get; set; }

        public string cNombres { get; set; }

        public string cApellidos { get; set; }

        public string cEmail { get; set; }

        public string cUsuario { get; set; }

        public string cPassword { get; set; }

    }
}