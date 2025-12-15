using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoEtapaData : ISMCMappable
    {       
        public long Seq { get; set; }
      
        public string Descricao { get; set; }
      
        public long SeqProcessoEtapa { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }
      
        public string OrientacaoEtapa { get; set; }

        public string DescricaoTermoEntregaDocumentacao { get; set; }

        public bool CamposReadyOnly { get; set; }

        public List<ConfiguracaoEtapaPaginaData> ConfiguracoesPagina { get; set; }

        public ProcessoEtapaData ProcessoEtapa { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.Processo.Servico")]
        public ServicoData Servico { get; set; }
    }
}
