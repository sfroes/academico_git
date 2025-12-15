using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ReplicaFormacaoEspecificaProgramaTitulacaoData : ISMCMappable
    {
        public long SeqTitulacao { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public bool Ativo { get; set; }
    }
}