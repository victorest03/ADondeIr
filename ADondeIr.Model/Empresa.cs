namespace ADondeIr.Model
{
    using System.ComponentModel.DataAnnotations;
    using FluentValidation;
    using FluentValidation.Attributes;
    using NPoco;

    [PrimaryKey("pkEmpresa")]
    [Validator(typeof(EmpresaValidator))]
    public class Empresa : BaseEntity
    {
        public int pkEmpresa { get; set; }

        [Display(Name = "Empresa")]
        public string cEmpresa { get; set; }

        [Display(Name = "Direccion")]
        public string cDireccion { get; set; }

        [Display(Name = "Telefono")]
        public string cTelefono { get; set; }

        [Ignore]
        public int eTotalProductos { get; set; }

    }

    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            RuleFor(x => x.cEmpresa).NotEmpty().WithMessage("Ingrese un nombre para la Empresa!!!")
                .Matches(@"^(?=.*\S).*$").WithMessage("Ingrese un nombre de Empresa valida!!!");

            RuleFor(x => x.cDireccion).Matches(@"^(?=.*\S).*$").WithMessage("Ingrese una direccion de Empresa valida!!!");

            RuleFor(x => x.cTelefono).Matches(@"^([+]\d{3} )?\d{7,9}").WithMessage("Ingrese un Teléfono valido!!!");
        }
    }
}
