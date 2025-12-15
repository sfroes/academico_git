using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class RelatorioPrevisaoConclusaoOrientacaoFiltroVO : ISMCMappable
    {
        [SMCMapProperty("SeqCicloLetivoIngresso")]
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long?> SeqsTipoVinculoAluno { get; set; }

        public DateTime? PrazoEncerrado { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}