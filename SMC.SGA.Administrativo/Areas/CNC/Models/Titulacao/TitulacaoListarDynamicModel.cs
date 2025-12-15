using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class TitulacaoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public virtual string DescricaoFeminino { get; set; }

        [SMCOrder(2)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public virtual string DescricaoMasculino { get; set; }

        [SMCOrder(3)]
        [SMCSortable(true)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public virtual string DescricaoAbreviada { get; set; }

        [SMCOrder(4)]
        [SMCMapProperty("GrauAcademico.Descricao")]
        [SMCInclude("GrauAcademico")]
        [SMCSortable(true, false)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string DescricaoGrauAcademico { get; set; }

        [SMCOrder(5)]
        [SMCMapProperty("Curso.Nome")]
        [SMCInclude("Curso")]
        [SMCSortable(true, false)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string DescricaoCurso { get; set; }

        [SMCOrder(6)]
        [SMCSortable(true)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public bool Ativo { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        public string Descricao { get; set; }
    }
}