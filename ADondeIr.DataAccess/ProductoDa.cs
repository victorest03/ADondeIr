namespace ADondeIr.DataAccess
{
    using System;
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
                    var fotoOld = new FotoDa().GetFotoPrincipal(entity.pkProducto);
                    if (columns == null || columns.Contains("fkFotoPrincipal"))
                    {
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
    }
}
