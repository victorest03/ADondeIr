namespace ADondeIr.Model
{
    using System.ComponentModel.DataAnnotations;
    using FluentValidation;
    using FluentValidation.Attributes;
    using NPoco;

    [PrimaryKey("pkUsuario")]
    [Validator(typeof(UsuarioValidator))]
    public class Usuario : BaseEntity
    {
        public int pkUsuario { get; set; }

        [Display(Name = "Nombres")]
        public string cNombres { get; set; }

        [Display(Name = "Apellidos")]
        public string cApellidos { get; set; }

        [Display(Name = "Email")]
        public string cEmail { get; set; }

        [Display(Name = "Password")]
        public string cPassword { get; set; }

        public bool isAdmin { get; set; }

    }

    [Validator(typeof(LoginValidator))]
    public class Login
    {
        [Display(Name = "Email")]
        public string cEmail { get; set; }

        [Display(Name = "Contraseña")]
        public string cPassword { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool isRememberMe { get; set; }
    }

    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.cNombres).NotEmpty().WithMessage("Ingrese un Nombre!!!");
            RuleFor(x => x.cApellidos).NotEmpty().WithMessage("Ingrese un Apellido!!!");
            RuleFor(x => x.cEmail).NotEmpty().WithMessage("Ingrese un Email!!!");
            RuleFor(x => x.cPassword).NotEmpty().WithMessage("Ingrese un Password!!!");
        }
    }

    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(x => x.cEmail).NotEmpty().WithMessage("Ingrese un Email!!!");
            RuleFor(x => x.cPassword).NotEmpty().WithMessage("Ingrese un Password!!!");
        }
    }
}