namespace ADondeIr.Model
{
    using NPoco;

    [PrimaryKey("pkRutaProducto")]
    public class RutaProducto
    {
        public int pkRutaProducto { get; set; }

        public int fkRuta { get; set; }

        [Reference(ReferenceType.OneToOne, ColumnName = "fkProducto", ReferenceMemberName = "pkProducto")]
        public Producto Producto { get; set; }
        public int fkProducto { get; set; }

        public int eOrden { get; set; }
    }
}
