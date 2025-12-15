using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.TUR.Data
{
    public class TopicoDivisaoTurmaData : ISMCMappable
    {
        public long? SeqDivisaoTurmaColaboradorOrganizacao { get; set; }

        public long? SeqDivisaoTurmaOrganizacao { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqDivisaoComponenteOrganizacao { get; set; }

        public string Colaborador { get; set; }

        public short CargaHoraria { get; set; }

        public string TipoOrganizacaoComponente { get; set; }
    }
}