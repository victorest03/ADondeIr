namespace ADondeIr.Common.Model
{
    using System;

    public class Auditoria
    {
        
        public int? fkUsuarioCrea { get; set; }
        
        public DateTime? fFechaCrea { get; set; }

        public int? fkUsuarioEdita { get; set; }

        public DateTime? fFechaEdita { get; set; }

        public bool isDeleted { get; set; }
    }

}
