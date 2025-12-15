using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularConfiguracaoComponenteDivisaoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.DivisoesMatrizCurricular))]
        [SMCSize(SMCSize.Grid6_24)]
        public long SeqDivisaoMatrizCurricular { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        public string DescricaoDivisaoMatrizCurricular { get; set; }

        [SMCConditionalDisplay(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.Itens))]
        [SMCConditionalRequired(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.Itens))]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCMinValue(1)]
        public short? QuantidadeItens { get; set; }

        [SMCConditionalDisplay(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.CargaHoraria))]
        [SMCConditionalRequired(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.CargaHoraria))]
        [SMCDependency(nameof(QuantidadeHoraAula), nameof(GrupoCurricularController.CalcularHorasRelogio), "GrupoCurricular", false)]
        [SMCSize(SMCSize.Grid6_24)]
        public short? QuantidadeHoraRelogio { get; set; }

        [SMCConditionalDisplay(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.CargaHoraria))]
        [SMCConditionalRequired(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.CargaHoraria))]
        [SMCDependency(nameof(QuantidadeHoraRelogio), nameof(GrupoCurricularController.CalcularHorasAula), "GrupoCurricular", false)]
        [SMCSize(SMCSize.Grid6_24)]
        public short? QuantidadeHoraAula { get; set; }

        [SMCConditionalDisplay(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.Credito))]
        [SMCConditionalRequired(nameof(MatrizCurricularConfiguracaoComponenteDynamicModel.FormatoConfiguracaoGrupoGrupoCurricular), nameof(FormatoConfiguracaoGrupo.Credito))]
        [SMCSize(SMCSize.Grid6_24)]
        public short? QuantidadeCreditos { get; set; }
    }
}