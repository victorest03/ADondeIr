namespace ADondeIr.DataAccess
{
    using System;
    using Model;
    using NPoco;
    using Repository;

    public class FotoDa : Repository<Foto>
    {
        public Foto GetFotoPrincipal(int pkProducto)
        {
            Foto result = null;

            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    result = db.Single<Foto>(@"
                        SELECT 
                            f.pkFoto
                            ,f.cfileName
                        FROM Producto p
                        INNER JOIN Foto f ON p.fkFotoPrincipal = f.pkFoto
                        WHERE p.pkProducto = @0", pkProducto);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return result;
        }
    }
}
