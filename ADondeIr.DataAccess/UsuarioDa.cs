namespace ADondeIr.DataAccess
{
    using Model;
    using NPoco;
    using Repository;
    using System;

    public class UsuarioDa : Repository<Usuario>
    {

        public Usuario ValidarLogin(string cUsuario, string cPassword)
        {
            Usuario result = null;

            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    result = db.SingleOrDefault<Usuario>(@"
                        SELECT 
                          u.pkUsuario
                          ,u.cNombres
                          ,u.cApellidos
                          ,u.cEmail
                          ,u.cUsuario
                          ,u.cPassword
                        FROM Usuario u
                          WHERE u.isDeleted = 0 AND u.cUsuario = @0 AND u.cPassword = @1", cUsuario, cPassword);
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
