using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class OfertaMatrizCurricularOfertasLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCHidden]
        [SMCKey]
        [SMCReadOnly]
        public long Seq { get; set; }
                
        [SMCOrder(0)]
        public string Codigo { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string DescricaoUnidade { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string DescricaoLocalidade { get; set; }

        [SMCOrder(1)]
        public string DescricaoUnidadeLocalidade
        {
            get { return $"{DescricaoUnidade} / {DescricaoLocalidade}"; }
        }

        [SMCOrder(2)]
        public string DescricaoTurno { get; set; }

        [SMCOrder(3)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCOrder(4)]
        public DateTime? DataFinalVigencia { get; set; }

        [SMCOrder(5)]
        public SituacaoMatrizCurricularOferta? HistoricoSituacaoAtual { get; set; }
    }
}
