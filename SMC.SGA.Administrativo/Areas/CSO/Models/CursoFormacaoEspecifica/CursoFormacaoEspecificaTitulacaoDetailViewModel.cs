using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoFormacaoEspecificaTitulacaoDetailViewModel : SMCViewModelBase
    {
        [SMCIgnoreProp]
        public string DescricaoTitulacao { get; set; }

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid14_24)]
        [SMCDependency(nameof(CursoFormacaoEspecificaDynamicModel.SeqCursoOferta), nameof(CursoFormacaoEspecificaController.BuscarTitulacoesSelect), "CursoFormacaoEspecifica", "CSO", false, includedProperties: new[] { nameof(CursoFormacaoEspecificaDynamicModel.CursoTipoFormacao), nameof(CursoFormacaoEspecificaDynamicModel.SeqCurso), nameof(CursoFormacaoEspecificaDynamicModel.SeqCursoFormacaoEspecifica) })]
        [SMCDependency(nameof(CursoFormacaoEspecificaDynamicModel.SeqGrauAcademico), nameof(CursoFormacaoEspecificaController.BuscarTitulacoesSelect), "CursoFormacaoEspecifica", "CSO", false, includedProperties: new[] { nameof(CursoFormacaoEspecificaDynamicModel.CursoTipoFormacao), nameof(CursoFormacaoEspecificaDynamicModel.SeqCurso), nameof(CursoFormacaoEspecificaDynamicModel.SeqCursoFormacaoEspecifica) })]
        [SMCSelect(nameof(CursoFormacaoEspecificaDynamicModel.TitulacoesDatasource), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoTitulacao))]
        [SMCRequired]
        public long? SeqTitulacao { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public bool Ativo { get; set; }
    }
}