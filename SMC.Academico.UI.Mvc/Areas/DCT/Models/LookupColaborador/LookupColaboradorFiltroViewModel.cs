using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Models
{
    public class LookupColaboradorFiltroViewModel : SMCLookupFilterViewModel
	{
        public long? Seq { get; set; }

        public long? SeqEntidadeVinculo { get; set; }

        public long? SeqTipoVinculoColaborador { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public SituacaoColaborador? SituacaoColaboradorNaInstituicao { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public long? SeqTurma { get; set; }
    }
}