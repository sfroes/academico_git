using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ReplicaFormacaoEspecificaProgramaConfirmacaoVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqPai { get; set; }
        public string Descricao { get; set; }
        
    }
}