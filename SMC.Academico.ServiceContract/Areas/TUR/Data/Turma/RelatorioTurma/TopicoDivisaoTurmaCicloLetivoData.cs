using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Data
{
    public class TopicoDivisaoTurmaCicloLetivoData : ISMCMappable
    {
        public long? SeqDivisaoTurmaOrganizacao { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public long? SeqDivisaoComponenteOrganizacao { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public string TipoOrganizacaoComponente { get; set; }

        public List<TopicoDivisaoTurmaData> TopicosDivisaoColaboradoresTurma { get; set; }

    }
}