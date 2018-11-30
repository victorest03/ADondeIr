namespace ADondeIr.BusinessLogic
{
    using DataAccess;
    using Model;
    using System.Collections.Generic;
    using Common.Model;

    public class EmpresaBl
    {
        private readonly EmpresaDa _da = new EmpresaDa();
        private readonly ProductoDa _daProducto = new ProductoDa();
        public List<Empresa> GetAll()
        {
            var empresas = _da.GetAll(e => e.isDeleted == false);
            empresas.ForEach(e => { e.eTotalProductos = _daProducto.Count(e.pkEmpresa); });
            return empresas;
        }

        public Empresa Get(int id)
        {
            return _da.Get(id);
        }

        public Result Delete(int pkEmpresa, int fkUsuarioEdita)
        {
            return _da.Delete(new Empresa() { pkEmpresa = pkEmpresa, fkUsuarioEdita = fkUsuarioEdita });
        }

        public Result Save(Empresa entity)
        {
            return entity.pkEmpresa == 0 ? _da.Insert(entity) : _da.Update(entity);
        }
    }
}
