using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class FormacaoEspecificaHierarquiaVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public List<FormacaoEspecificaListaVO> Hierarquia { get; set; }
    }
}