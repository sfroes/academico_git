using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoEtapaProcessamentoListarVO : ISMCMappable
    {      
        public long SeqEscalonamento { get; set; }

        public string DescricaoEscalonamento { get { return $"{DataInicio.ToShortDateString()} - {DataFim.ToShortDateString()}"; } }

        public DateTime DataInicio { get; set; }
        
        public DateTime DataFim { get; set; }

        public List<long> SeqsGruposEscalonamento { get; set; }       

    }
}
