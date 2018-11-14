namespace ADondeIr.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using FluentValidation;
    using FluentValidation.Attributes;
    using NPoco;

    [PrimaryKey("pkProducto")]
    [Validator(typeof(ProductoValidator))]
    public class Producto : BaseEntity
    {
        public int pkProducto { get; set; }

        [Display(Name = "Producto")]
        public string cProducto { get; set; }

        [Reference(ReferenceType.OneToOne, ColumnName = "fkTipoActividad", ReferenceMemberName = "pkTipoActividad")]
        public TipoActividad TipoActividad { get; set; }
        [Display(Name = "Tipo de Actividad")]
        public int fkTipoActividad { get; set; }

        [Display(Name = "Observacion Principal")]
        public string cObservacionPrincipal { get; set; }

        [Display(Name = "Observación General")]
        [AllowHtml]
        public string cObservacionGeneral { get; set; }

        [Reference(ReferenceType.OneToOne, ColumnName = "fkDistrito", ReferenceMemberName = "pkDistrito")]
        public Distrito Distrito { get; set; }
        [Display(Name = "Distrito")]
        public int fkDistrito { get; set; }

        [Display(Name = "Foto Principal")]
        [Reference(ReferenceType.Foreign, ColumnName = "fkFotoPrincipal", ReferenceMemberName = "pkFoto")]
        public Foto FotoPrincipal { get; set; }

        [Display(Name = "Tags")]
        public string cTags { get; set; }

        [Display(Name = "Precio")]
        public decimal dPrecio { get; set; }
    }

    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.cProducto).NotEmpty().WithMessage("Ingrese un nombre para la Tipo de Actividad!!!")
                .Matches(@"^(?=.*\S).*$").WithMessage("Ingrese un nombre de Tipo de Actividad valida!!!");

            RuleFor(x => x.fkTipoActividad).NotEmpty().WithMessage("Ingrese un nombre para la Tipo de Actividad!!!");
            RuleFor(x => x.cObservacionPrincipal).NotEmpty().WithMessage("Ingrese una observacion principal!!!");
            RuleFor(x => x.fkDistrito).NotEmpty().WithMessage("Ingrese un Distrito!!!");
            RuleFor(x => x.cTags).NotEmpty().WithMessage("Ingrese almenos un Tag!!!");
            RuleFor(x => x.dPrecio).NotEmpty().WithMessage("Ingrese un precio. Si es gratis ingrese 0.00!!!");
        }
    }
}
