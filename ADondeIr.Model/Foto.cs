namespace ADondeIr.Model
{
    using NPoco;

    [PrimaryKey("pkFoto")]
    public class Foto
    {
        public int pkFoto { get; set; }

        public string cFileName { get; set; }
    }
}
