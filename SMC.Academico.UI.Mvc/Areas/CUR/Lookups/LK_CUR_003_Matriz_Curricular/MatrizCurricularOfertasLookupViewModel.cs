using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class MatrizCurricularOfertasLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCHidden]
        [SMCKey]
        [SMCReadOnly]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid4_24)]
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
        public SituacaoMatrizCurricularOferta? HistoricoSituacaoAtual { get; set; }
    }
}
