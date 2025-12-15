using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ReplicaFormacaoEspecificaProgramaTitulacaoVO : ISMCMappable
    {
        public long SeqTitulacao { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public bool Ativo { get; set; }
    }
}