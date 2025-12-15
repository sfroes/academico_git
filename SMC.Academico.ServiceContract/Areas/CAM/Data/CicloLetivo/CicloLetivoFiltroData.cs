using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqRegimeLetivo { get; set; }

        public long? SeqNivel { get; set; }

        public short? Ano { get; set; }

        public short? Numero { get; set; }
        
        public List<long> SeqsNiveisEnsino { get; set; }
    }
}