using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosModalSolicitacaoVO
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }
        public long SeqServico { get; set; }

        public string NumeroProtocolo { get; set; }

        public string Processo { get; set; }
        
        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public string TokenTipoServico { get; set; }

        public long? SeqProcessoEtapaAtual { get; set; }

    }
}