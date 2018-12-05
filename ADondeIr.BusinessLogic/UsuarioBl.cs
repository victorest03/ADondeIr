namespace ADondeIr.BusinessLogic
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Methods;
    using Common.Model;
    using DataAccess;
    using Model;

    public class UsuarioBl
    {
        private readonly UsuarioDa _da = new UsuarioDa();
        public List<Usuario> GetAll()
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

        public Result Forgot(string email)
        {
            var result = new  Result();
            var usuario = _da.GetAll(u => u.cEmail.Equals(email)).FirstOrDefault();
            if (usuario != null)
            {
                SendMailHelper.SendMail(new []{ usuario.cEmail },"Recuperar Contraseña", $"Su Contraseña es : {usuario.cPassword}");
                result.Success = true;
            }
            else
            {
                result.Message = "No se cuenta con una cuenta registrada con el email indicado!!";
            }

            return result;
        }
    }
}
