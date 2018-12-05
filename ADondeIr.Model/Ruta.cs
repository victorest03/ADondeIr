using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADondeIr.Model
{
    using NPoco;

    [PrimaryKey("pkRuta")]
    public class Ruta : BaseEntity
    {
        public int pkRuta { get; set; }

        public string cRuta { get; set; }

        public string cDescripcion { get; set; }

        public bool bActivo { get; set; }

        public int fkUsuario { get; set; }

        [Reference(ReferenceType.Many, ColumnName = "pkRuta", ReferenceMemberName = "fkRuta")]
        public List<RutaProducto> RutaProducto { get; set; }
    }
}
