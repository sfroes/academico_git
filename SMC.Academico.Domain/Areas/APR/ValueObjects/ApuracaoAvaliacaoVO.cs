using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class ApuracaoAvaliacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public decimal? Nota { get; set; }
        
        public long? SeqEscalaApuracaoItem { get; set; }

        public bool Comparecimento { get; set; }

        public long? SeqArquivoAnexadoAtaDefesa { get; set; }
        
        public string ComentarioApuracao { get; set; }

        public TipoAvaliacao TipoAvaliacao { get; set; }

        //public EscalaApuracaoItem EscalaApuracaoItem { get; set; }

    }
}