using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class ApuracaoFormacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAlunoFormacao { get; set; }

        public SituacaoAlunoFormacao SituacaoAlunoFormacao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataInclusao { get; set; }
    }
}
