namespace ADondeIr.Model
{
    using System;
    using NPoco;

    public abstract class BaseEntity
    {
        [ComputedColumn(ComputedColumnType.ComputedOnUpdate)]
        public virtual int? fkUsuarioCrea { get; set; }

        [Reference(ReferenceType.OneToOne, ColumnName = "fkUsuarioCrea", ReferenceMemberName = "pkUsuario")]
        public Usuario UsuarioCrea { get; set; }

        [ComputedColumn(ComputedColumnType.Always)]
        public virtual DateTime? fFechaCrea { get; set; }

        [ComputedColumn(ComputedColumnType.ComputedOnInsert)]
        public virtual int? fkUsuarioEdita { get; set; }

        [Reference(ReferenceType.OneToOne, ColumnName = "fkUsuarioEdita", ReferenceMemberName = "pkUsuario")]
        public Usuario UsuarioEdita { get; set; }

        [ComputedColumn(ComputedColumnType.ComputedOnInsert)]
        public virtual DateTime? fFechaEdita { get; set; }

        [ComputedColumn(ComputedColumnType.ComputedOnInsert)]
        public virtual bool isDeleted { get; set; }
    }

}
