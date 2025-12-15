using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoRegistroDocumentoData : ISMCMappable
    {
        public List<SMCDatasourceItem> SituacoesEntregaDocumento { get; set; }

        public long SeqPessoaAtuacao { get; set; }
        public long SeqTipoDocumento { get; set; }

        public bool PermiteVarios { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public List<long> SeqsSolicitacoesServico { get; set; }

        public List<PessoaAtuacaoRegistroDocumentoItemData> Documentos { get; set; }
    }
}