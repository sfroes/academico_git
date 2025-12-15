using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "CurriculoCursoOfertaViewModel_GrupoHoras", Size = SMCSize.Grid18_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "CurriculoCursoOfertaViewModel_GrupoCreditos", Size = SMCSize.Grid6_24)]
    public class CurriculoCursoOfertaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqCurriculo { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(CurriculoDynamicModel.CursosOfertaDataSource), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCUnique]
        public long SeqCursoOferta { get; set; }

        [SMCDependency(nameof(QuantidadeCreditoObrigatorio), nameof(CurriculoController.ValidarQuantidadeHoraAulaObrigatoria), "Curriculo", false)]
        [SMCHidden]
        public bool QuantidadeHoraAulaObrigatoriaRequerida => !QuantidadeCreditoObrigatorio.HasValue;

        [SMCConditionalRequired(nameof(QuantidadeHoraAulaObrigatoriaRequerida), true)]
        [SMCGroupedProperty("CurriculoCursoOfertaViewModel_GrupoHoras")]
        [SMCMask("9999")]
        [SMCMinValue(1)]
        [SMCSize(SMCSize.Grid6_24)]
        public short? QuantidadeHoraAulaObrigatoria { get; set; }

        [SMCDependency(nameof(QuantidadeHoraAulaObrigatoria), nameof(CurriculoController.CalcularQuantidadeHoraRelogioObrigatoria), "Curriculo", true)]
        [SMCGroupedProperty("CurriculoCursoOfertaViewModel_GrupoHoras")]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24)]
        public short? QuantidadeHoraRelogioObrigatoria { get; set; }

        [SMCGroupedProperty("CurriculoCursoOfertaViewModel_GrupoHoras")]
        [SMCMask("9999")]
        [SMCSize(SMCSize.Grid6_24)]
        public short? QuantidadeHoraAulaOptativa { get; set; }

        [SMCDependency(nameof(QuantidadeHoraAulaOptativa), nameof(CurriculoController.CalcularQuantidadeHoraRelogioOptativa), "Curriculo", true)]
        [SMCGroupedProperty("CurriculoCursoOfertaViewModel_GrupoHoras")]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24)]
        public short? QuantidadeHoraRelogioOptativa { get; set; }

        [SMCDependency(nameof(QuantidadeHoraAulaObrigatoria), nameof(CurriculoController.ValidarQuantidadeCreditoObrigatorio), "Curriculo", false)]
        [SMCHidden]
        public bool QuantidadeCreditoObrigatorioRequerido => !QuantidadeHoraAulaObrigatoria.HasValue;

        [SMCConditionalDisplay(nameof(CurriculoDynamicModel.PermiteCreditoComponenteCurricular), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(QuantidadeCreditoObrigatorioRequerido), true)]
        [SMCGroupedProperty("CurriculoCursoOfertaViewModel_GrupoCreditos")]
        [SMCMask("9999")]
        [SMCMinValue(1)]
        [SMCSize(SMCSize.Grid12_24)]
        public virtual short? QuantidadeCreditoObrigatorio { get; set; }

        [SMCConditionalDisplay(nameof(CurriculoDynamicModel.PermiteCreditoComponenteCurricular), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("CurriculoCursoOfertaViewModel_GrupoCreditos")]
        [SMCMask("9999")]
        [SMCSize(SMCSize.Grid12_24)]
        public virtual short? QuantidadeCreditoOptativo { get; set; }
    }
}