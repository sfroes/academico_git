using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponenteListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        /// <summary>
        /// Descrição formatada segundo a regra RN_CUR_042
        /// </summary>
        public string DescricaoFormatada { get; set; }

        public short? ComponenteCurricularCargaHoraria { get; set; }

        public short? ComponenteCurricularCredito { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public List<ConfiguracaoComponenteDivisaoListarData> DivisoesComponente { get; set; }

        public string ConfiguracaoComponenteDescricaoCompleta { get; set; }

        public bool PreRequisito { get; set; }

        public bool ObrigatorioOrientador { get; set; }

        public List<long> SeqsAtividadeCoRequisitos { get; set; }

        public long? SeqSolicitacaoMatriculaItem { get; set; }
    }
}