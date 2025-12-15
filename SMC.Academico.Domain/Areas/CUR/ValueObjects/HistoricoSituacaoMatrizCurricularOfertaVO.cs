using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class HistoricoSituacaoMatrizCurricularOfertaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public long SeqMatrizCurricularOferta { get; set; }

        public SituacaoMatrizCurricularOferta SituacaoMatrizCurricularOferta { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public bool PrimeiroRegistro { get; set; }
    }
}
