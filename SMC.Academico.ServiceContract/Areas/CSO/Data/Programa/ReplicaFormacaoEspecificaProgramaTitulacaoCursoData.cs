using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ReplicaFormacaoEspecificaProgramaTitulacaoCursoData : ISMCMappable
    {
        public long SeqCurso { get; set; }

        public List<ReplicaFormacaoEspecificaProgramaTitulacaoData> Titulacoes { get; set; }
    }
}