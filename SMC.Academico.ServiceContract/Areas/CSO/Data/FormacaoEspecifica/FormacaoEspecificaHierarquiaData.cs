using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class FormacaoEspecificaHierarquiaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public List<FormacaoEspecificaListaData> Hierarquia { get; set; }
    }
}