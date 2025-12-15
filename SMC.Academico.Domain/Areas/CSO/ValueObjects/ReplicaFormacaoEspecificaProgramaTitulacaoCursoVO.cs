using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ReplicaFormacaoEspecificaProgramaTitulacaoCursoVO : ISMCMappable
    {
        public long SeqCurso { get; set; }

        public List<ReplicaFormacaoEspecificaProgramaTitulacaoVO> Titulacoes { get; set; }
    }
}