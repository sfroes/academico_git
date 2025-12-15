using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Lookups
{
    public class TurmaLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {

        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCSortable(true)]
        public string DescricaoCicloLetivoInicio { get; set; }

        [SMCHidden]
        public int? Codigo { get; set; }

        [SMCHidden]
        public short? Numero { get; set; }

        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        [SMCDescription]
        public string DescricaoConfiguracaoComponente { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public DateTime? InicioPeriodoLetivo { get; set; }

        public DateTime? FimPeriodoLetivo { get; set; }
    }
}
