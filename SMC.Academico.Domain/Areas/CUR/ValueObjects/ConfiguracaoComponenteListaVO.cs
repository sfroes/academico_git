using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConfiguracaoComponenteListaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        [SMCMapProperty("ComponenteCurricular.SeqTipoComponenteCurricular")]
        public long SeqTipoComponenteCurricular { get; set; }

        [SMCMapMethod("ComponenteCurricular.RecuperarSeqNivelEnsinoResponsavel")]
        public long SeqNivelEnsino { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        /// <summary>
        /// Descrição formatada segundo a regra RN_CUR_042
        /// </summary>
        public string DescricaoFormatada { get; set; }

        public bool Ativo { get; set; }

        public IList<DivisaoComponenteListaVO> DivisoesComponente { get; set; }

        public bool ExibirItensOrganizacao { get; set; }

        [SMCMapProperty("ComponenteCurricular.CargaHoraria")]
        public short? ComponenteCurricularCargaHoraria { get; set; }

        [SMCMapProperty("ComponenteCurricular.Credito")]
        public short? ComponenteCurricularCredito { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public IEnumerable<string> ComponenteCurricularEntidadesSigla { get; set; }

        public string ConfiguracaoComponenteDescricaoCompleta
        {
            get
            {
                var result = $"{Codigo}";

                if (!string.IsNullOrEmpty(Descricao))
                    result += $" - {Descricao}";

                if (!string.IsNullOrEmpty(DescricaoComplementar))
                    result += $" - {DescricaoComplementar}";

                if (ComponenteCurricularCargaHoraria.HasValue)
                    result += $" - {ComponenteCurricularCargaHoraria.Value}";

                if (FormatoCargaHoraria.HasValue && FormatoCargaHoraria.Value != Common.Areas.CUR.Enums.FormatoCargaHoraria.Nenhum)
                    result += $" {FormatoCargaHoraria.Value}";

                if (ComponenteCurricularCredito.HasValue)
                    result += $" - {ComponenteCurricularCredito.Value} Crédito";

                if (ComponenteCurricularEntidadesSigla != null && ComponenteCurricularEntidadesSigla.Any(x => x != null))
                    result += $" - {string.Join(" / ", ComponenteCurricularEntidadesSigla)}";

                return result;
            }
        }

        public bool PreRequisito { get; set; }

        public bool ObrigatorioOrientador { get; set; }

        public List<long> SeqsAtividadeCoRequisitos { get; set; }

        public long? SeqSolicitacaoMatriculaItem { get; set; }
    }
}