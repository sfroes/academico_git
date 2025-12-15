using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.UI.Mvc.Areas.CUR.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class SelecaoTurmaOfertaViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long SeqSolicitacaoMatricula { get; set; }

        public long Seq { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        [SMCHidden]
        public string CodigoFormatado { get; set; }

        public string TurmaFormatado { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public bool Habilitar { get; set; }

        public List<SelecaoTurmaOfertaDivisaoViewModel> TurmaMatriculaDivisoes { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }

        [SMCHidden]
        public bool? ExigirCurso { get; set; }

        [SMCHidden]
        public bool? ExigirMatrizCurricularOferta { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        [SMCHidden]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public AssociacaoOfertaMatriz AssociacaoOfertaMatrizTipoTurma { get; set; }

        [SMCHidden]
        public bool PreRequisito { get; set; }

        [SMCHidden]
        public bool ObrigatorioOrientador { get; set; }

        [SMCHidden]
        public List<long> SeqsTurmaCoRequisitos { get; set; }

        [SMCHidden]
        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        [SMCHidden]
        public ComponenteCurricularViewModel ComponenteCurricular { get; set; }
        
        [SMCHidden]
        public long SeqTurma { get; set; }
    }
}