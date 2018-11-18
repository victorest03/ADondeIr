namespace ADondeIr.Common.Model
{
    public class ProductoFilter
    {
        public int start { get; set; }
        public int length { get; set; }
        public string search { get; set; }
        public int? distrito { get; set; }
        public int? tipoActividad { get; set; }
    }
}
