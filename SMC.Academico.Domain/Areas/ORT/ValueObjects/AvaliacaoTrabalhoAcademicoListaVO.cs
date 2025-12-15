using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class AvaliacaoTrabalhoAcademicoListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqNivelEnsino { get; set; }
        
        public List<long> SeqsAutores { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public long? SeqCriterioAprovacao { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public bool PublicacaoBiblioteca { get; set; }

        public DateTime? DataAutorizacaoSegundoDeposito { get; set; }

        public List<AvaliacaoTrabalhoAcademicoAvaliacaoVO> Avaliacoes { get; set; }
    }
}
