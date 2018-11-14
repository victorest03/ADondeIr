﻿namespace ADondeIr.BusinessLogic
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Model;
    using DataAccess;
    using Model;

    public class ProductoBl
    {
        private readonly ProductoDa _da = new ProductoDa();

        public List<Producto> GetAll()
        {
            return _da.GetAll(e => e.isDeleted == false, query =>
            {
                query.Include(p=>p.TipoActividad);
                query.Include(p=>p.Distrito);
                query.Include(p=>p.FotoPrincipal);
            });
        }

        public Producto Get(int id)
        {
            return _da.GetAll(e => e.isDeleted == false && e.pkProducto == id, query =>
            {
                query.Include(p => p.TipoActividad);
                query.Include(p => p.Distrito);
                query.Include(p => p.FotoPrincipal);
            }).FirstOrDefault();
        }

        public Result Delete(int pkProducto, int fkUsuarioEdita)
        {
            return _da.Delete(new Producto { pkProducto = pkProducto, fkUsuarioEdita = fkUsuarioEdita });
        }

        public Result Save(Producto entity)
        {
            return entity.pkProducto == 0 ? _da.Insert(entity) : _da.Update(entity);
        }
    }
}
