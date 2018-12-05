namespace ADondeIr.Model
{
    using NPoco;

    [PrimaryKey("pkDistrito")]
    public class Distrito
    {
        public int pkDistrito { get; set; }

        public string cDistrito { get; set; }

        [Ignore]
        public int eTotalProductos { get; set; }
    }
}
