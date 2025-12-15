using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ReplicaFormacaoEspecificaProgramaConfirmacaoData : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqPai { get; set; }
        public string Descricao { get; set; }
        
    }
}