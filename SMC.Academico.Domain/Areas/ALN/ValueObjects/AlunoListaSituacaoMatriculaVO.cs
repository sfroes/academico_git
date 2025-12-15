using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoListaSituacaoMatriculaVO : ISMCMappable
    {
        public long Seq { get; set; }
        
        public long SeqSituacaoMatricula { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string AnoNumeroCicloLetivo { get; set; }
    }
}