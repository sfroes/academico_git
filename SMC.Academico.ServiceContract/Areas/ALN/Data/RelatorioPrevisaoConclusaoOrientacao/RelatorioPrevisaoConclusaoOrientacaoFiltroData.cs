using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class RelatorioPrevisaoConclusaoOrientacaoFiltroData : ISMCMappable
    {
        [SMCMapProperty("SeqCicloLetivoIngresso")]
        public long Seq { get; set; }

        [SMCMapProperty("SeqEntidadesResponsaveis")]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long?> SeqsTipoVinculoAluno { get; set; }

        public DateTime? PrazoEncerrado { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}
