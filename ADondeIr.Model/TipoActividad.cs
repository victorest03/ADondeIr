namespace ADondeIr.Model
{
    using System.ComponentModel.DataAnnotations;
    using FluentValidation;
    using FluentValidation.Attributes;
    using NPoco;

    [PrimaryKey("pkTipoActividad")]
    [Validator(typeof(TipoActividadValidator))]
    public class TipoActividad : BaseEntity
    {
        public int pkTipoActividad { get; set; }

        [Display(Name = "Tipo de Actividad")]
        public string cTipoActividad { get; set; }

        [Display(Name = "Icono(FontAwesome)")]
        public string cIcon { get; set; }
    }

    public class TipoActividadValidator : AbstractValidator<TipoActividad>
    {
        public TipoActividadValidator()
        {
            RuleFor(x => x.cTipoActividad).NotEmpty().WithMessage("Ingrese un nombre para la Tipo de Actividad!!!")
                .Matches(@"^(?=.*\S).*$").WithMessage("Ingrese un nombre de Tipo de Actividad valida!!!");

            RuleFor(x => x.cIcon).NotEmpty().WithMessage("Ingrese un nombre para la Tipo de Actividad!!!")
                .Matches(@"^(?=.*\S).*$").WithMessage("Ingrese un nombre de Tipo de Actividad valida!!!");
        }
    }
}