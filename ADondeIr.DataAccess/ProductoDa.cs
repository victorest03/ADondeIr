namespace ADondeIr.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Model;
    using Model;
    using NPoco;
    using Repository;

    public class ProductoDa : Repository<Producto>
    {
        public override Result Insert(Producto entity)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    db.BeginTransaction();

                    db.Insert(entity.FotoPrincipal);
                    db.Insert(entity);
                    
                    db.CompleteTransaction();
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }

        public override Result Update(Producto entity, string[] columns = null)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    db.BeginTransaction();
                    if (columns == null || columns.Contains("fkFotoPrincipal"))
                    {
                        var fotoOld = new FotoDa().GetFotoPrincipal(entity.pkProducto);
                        if (entity.FotoPrincipal != null)
                        {
                            entity.FotoPrincipal.pkFoto = fotoOld.pkFoto;
                            db.Update(entity.FotoPrincipal);
                        }
                        else
                        {
                            entity.FotoPrincipal = fotoOld;
                        }
                    }
                    
                    db.Update(entity, columns);

                    db.CompleteTransaction();
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }

        public int Count(int? fkEmpresa = null, int? fkTipoActividad = null)
        {
            var result = 0;
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    var query = db.Query<Producto>().Where(p => !p.isDeleted);
                    if (fkEmpresa != null) query.Where(p => p.fkEmpresa == fkEmpresa);
                    if (fkTipoActividad != null) query.Where(p => p.fkTipoActividad == fkTipoActividad);
                    result = query.Count();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return result;
        }

        public List<Producto> GetAllLazy(ProductoFilter filter,out int filteredResultsCount, out int totalResultsCount)
        {
            var result = new List<Producto>();
            var outtotalResultsCount = 0;
            var outfilteredResultsCount = 0;
            filter.search = filter.search ?? "";
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    #region Select All

                    var query = db.Query<Producto>();
                    query.Include(p => p.TipoActividad);
                    query.Include(p => p.Distrito);
                    query.Include(p => p.FotoPrincipal);
                    query.Where(s => !s.isDeleted);

                    if (filter.distrito != null && filter.distrito != 0)
                        query.Where(p => p.fkDistrito == filter.distrito);

                    if (filter.tipoActividad != null && filter.tipoActividad != 0)
                        query.Where(p => p.fkTipoActividad == filter.tipoActividad);

                    query.Where(w =>
                        w.cProducto.ToLower().Contains(filter.search) ||
                        w.cTags.ToLower().Contains(filter.search));

                    query.OrderBy(p => p.cProducto);

                    var t = query.ToPage((filter.start * filter.length) / filter.length, filter.length);

                    outtotalResultsCount = db.Query<Producto>().Count(c => !c.isDeleted);
                    outfilteredResultsCount = (int)t.TotalItems;
                    result = t.Items;
                    #endregion
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }


            totalResultsCount = outtotalResultsCount;
            filteredResultsCount = outfilteredResultsCount;
            return result;
        }
    }
}
