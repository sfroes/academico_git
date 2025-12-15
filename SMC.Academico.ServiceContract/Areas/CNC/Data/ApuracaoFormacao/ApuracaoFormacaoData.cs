using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class ApuracaoFormacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAlunoFormacao { get; set; }

        public SituacaoAlunoFormacao SituacaoAlunoFormacao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataInclusao { get; set; }
    }
}
