namespace ADondeIr.BusinessLogic
{
    using System.Collections.Generic;
    using Common.Model;
    using DataAccess;
    using Model;

    public class UsuarioBl
    {
        private readonly UsuarioDa _da = new UsuarioDa();
        public List<Usuario> GetAll(int pkPais)
        {
            return _da.GetAll(usuario => usuario.isDeleted==false);
        }

        public Usuario ValidarUser(string cEmail, string cPassword)
        {
            return _da.ValidarLogin(cEmail, cPassword);
        }

        public Usuario Get(int id)
        {
            return _da.Get(id);
        }

        public Result Delete(int pkUsuario, int fkUsuarioEdita)
        {
            return _da.Delete(new Usuario(){ pkUsuario = pkUsuario, fkUsuarioEdita = fkUsuarioEdita});
        }

        public Result Save(Usuario entity)
        {
            return entity.pkUsuario == 0 ? _da.Insert(entity) : _da.Update(entity);
        }

        public Result SaveChangePassword(Usuario entity)
        {
            return _da.Update(entity, new[] {"cPassword", "isFirstSession", "fkUsuarioEdita", "fFechaEdita"});
        }
    }
}
