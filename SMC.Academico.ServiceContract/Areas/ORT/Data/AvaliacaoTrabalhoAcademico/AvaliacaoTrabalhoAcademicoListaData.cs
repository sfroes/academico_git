using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class AvaliacaoTrabalhoAcademicoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public bool PublicacaoBiblioteca { get; set; }

        public bool PossuiSegundoDeposito { get; set; }

        public DateTime? DataAutorizacaoSegundoDeposito { get; set; }

        public List<AvaliacaoTrabalhoAcademicoAvaliacaoData> Avaliacoes { get; set; }
    }
}