using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ProcessoSeletivoListaData : ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public string TipoProcessoSeletivo { get; set; }

        public List<string> NiveisEnsino { get; set; }

        public long SeqCampanha { get; set; }

        public bool IngressoDireto { get; set; }
    }
}