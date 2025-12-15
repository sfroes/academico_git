using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoApostilamentoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public long SeqTipoApostilamento { get; set; }

        public long? SeqAlunoFormacao { get; set; }

        public string SeqAlunoFormacaoDescription { get; set; }

        public string Descricao { get; set; }

        public string Justificativa { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public bool CamposReadyOnly { get; set; }
    }
}
