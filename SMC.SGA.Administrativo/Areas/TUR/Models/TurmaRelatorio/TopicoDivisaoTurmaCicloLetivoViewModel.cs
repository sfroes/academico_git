using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TopicoDivisaoTurmaCicloLetivoViewModel : ISMCMappable
    {
        public long? SeqDivisaoTurmaOrganizacao { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public long? SeqDivisaoComponenteOrganizacao { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public string TipoOrganizacaoComponente { get; set; }

        public List<TopicoDivisaoTurmaViewModel> TopicosDivisaoColaboradoresTurma { get; set; }
    }
}