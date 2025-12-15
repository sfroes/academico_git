using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularGridOfertaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCOrder(1)]
        public string Codigo { get; set; }

        [SMCHidden]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        [SMCKey]
        public long SeqDivisao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        public string DescricaoUnidade { get; set; }

        [SMCHidden]
        public string DescricaoLocalidade { get; set; }

        [SMCOrder(2)]
        public string DescricaoUnidadeLocalidade
        {
            get { return $"{DescricaoUnidade} / {DescricaoLocalidade}"; }
        }

        
        [SMCOrder(3)]
        public string DescricaoTurno { get; set; }

        [SMCOrder(4)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCOrder(5)]
        public DateTime? DataFinalVigencia { get; set; }

        [SMCOrder(6)]
        public short? NumeroPeriodoAtivo { get; set; }

        [SMCOrder(7)]
        public SituacaoMatrizCurricularOferta? HistoricoSituacaoAtual { get; set; }

    }
}