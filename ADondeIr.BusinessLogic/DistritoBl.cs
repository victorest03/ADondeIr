namespace ADondeIr.BusinessLogic
{
    using System.Collections.Generic;
    using DataAccess;
    using Model;

    public class DistritoBl
    {
        private readonly DistritoDa _da = new DistritoDa();
        private readonly ProductoDa _daProducto = new ProductoDa();
        public List<Distrito> GetAll()
        {
            var distritos = _da.GetAll();
            distritos.ForEach(d => d.eTotalProductos = _daProducto.Count(null, null, d.pkDistrito));
            return distritos;
        }
    }
}
