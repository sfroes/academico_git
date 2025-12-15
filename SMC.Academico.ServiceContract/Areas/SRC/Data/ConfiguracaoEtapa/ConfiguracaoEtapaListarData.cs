using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoEtapaListarData : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public string DescricaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public List<ConfiguracaoEtapaListarItemData> ConfiguracoesEtapa { get; set; }
    }
}
