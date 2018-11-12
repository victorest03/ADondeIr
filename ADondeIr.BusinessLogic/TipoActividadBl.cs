namespace ADondeIr.BusinessLogic
{
    using System.Collections.Generic;
    using Common.Model;
    using DataAccess;
    using Model;

    public class TipoActividadBl
    {
        private readonly TipoActividadDa _da = new TipoActividadDa();
        public List<TipoActividad> GetAll()
        {
            return _da.GetAll(e => e.isDeleted == false);
        }

        public TipoActividad Get(int id)
        {
            return _da.Get(id);
        }

        public Result Delete(int pkTipoActividad, int fkUsuarioEdita)
        {
            return _da.Delete(new TipoActividad() { pkTipoActividad = pkTipoActividad, fkUsuarioEdita = fkUsuarioEdita });
        }

        public Result Save(TipoActividad entity)
        {
            return entity.pkTipoActividad == 0 ? _da.Insert(entity) : _da.Update(entity);
        }
    }
}
