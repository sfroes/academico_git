using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Data
{
    public class ItemDivisaoComponenteData : ISMCMappable
    {
        public long? SeqDivisaoTurma { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqDivisaoTurmaOrganizacao { get; set; }

        public long? SeqDivisaoComponenteOrganizacao { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public string CodificacaoDivisao { get; set; }

        public string LocalidadeDivisao { get; set; }

        public short VagasDivisao { get; set; }

        /// <summary>
        /// Espaço físico
        /// </summary>
        public string InformacoesComplementares { get; set; }

        public string Colaborador { get; set; }

        public List<TopicoDivisaoTurmaCicloLetivoData> TopicosDivisao { get; set; }
    }
}