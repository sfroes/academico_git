using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class PlanoEstudoAlunoJaMatriculadoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqAlunoHistoricoCicloLetivo { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public long SeqPlanoEstudoAtual { get; set; }

        public IList<PlanoEstudoItem> Itens { get; set; }
    }
}