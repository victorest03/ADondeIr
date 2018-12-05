namespace ADondeIr.BusinessLogic
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Model;
    using DataAccess;
    using Model;

    public class RutaBl
    {
        private readonly RutaDa _da = new RutaDa();
        private readonly RutaProductoDa _daRutaProducto = new RutaProductoDa();

        public List<Ruta> GetAll(int fkUsuario)
        {
            return _da.GetAll(s => !s.isDeleted && s.fkUsuario == fkUsuario,
                includes =>
                {
                    includes.IncludeMany(i => i.RutaProducto);
                    includes.Include(i => i.RutaProducto[0].Producto);
                });
        }

        public Ruta Get(int pkRuta)
        {
            return _da.GetAll(s => !s.isDeleted && s.pkRuta == pkRuta,
                includes =>
                {
                    includes.IncludeMany(i => i.RutaProducto);
                    includes.Include(i => i.RutaProducto[0].Producto);
                }).FirstOrDefault();
        }

        public Ruta GetActiva(int fkUsuario)
        {
            return _da.GetAll(s => !s.isDeleted && s.bActivo && s.fkUsuario == fkUsuario,
                includes =>
                {
                    includes.IncludeMany(i => i.RutaProducto);
                    includes.Include(i => i.RutaProducto[0].Producto);
                }).FirstOrDefault();
        }

        public Result AddProduct(RutaProducto producto)
        {
            var rutaCurrent = _da.Get(producto.fkRuta);
            producto.eOrden = rutaCurrent.RutaProducto?.Count + 1 ?? 1;
            return _daRutaProducto.Insert(producto);
        }

        public Result UpdateProduct(RutaProducto producto)
        {
            return _daRutaProducto.Update(producto);
        }

        public Result DeleteProducto(int pkRutaProducto)
        {
            return _daRutaProducto.Delete(new RutaProducto { pkRutaProducto = pkRutaProducto });
        }

        public Result GuardarRuta(int pkRuta)
        {
            return _da.Update(new Ruta
            {
                pkRuta = pkRuta,
                bActivo = false
            }, new[] {"pkRuta", "bActivo"});
        }

        public Result CreateRuta(Ruta ruta)
        {
            return _da.Insert(ruta);
        }

        public Result Delete(int pkRuta)
        {
            return _da.Delete(new Ruta{ pkRuta = pkRuta });
        }
    }
}
