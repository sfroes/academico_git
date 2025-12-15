using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoBloqueioCompovanteData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacaoBloqueio { get; set; }

        public long SeqArquivoAnexado { get; set; }

        public DateTime DataAnexo { get; set; }

        [SMCMapProperty("DataAlteracao")]
        public DateTime? UltimaAtualizacao { get; set; }

        public string Descricao { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}