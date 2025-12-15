using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ComprovanteProcessosData : ISMCMappable
    {
        public List<ComprovanteProcessosDadosData> DadosPessoais { get; set; }

        public List<ComprovanteProcessosTurmaData> Turmas { get; set; }

        public List<ComprovanteProcessosAtividadeData> Atividades { get; set; }
        
    }
}
