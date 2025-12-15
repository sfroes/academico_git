using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string Sigla { get; set; }

        public int? CodigoComponenteLegado { get; set; }

        public string BancoLegado { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqEntidade { get; set; }

        public long? SeqInstituicaoNivelResponsavel { get; set; }

        public long[] SeqComponentesCurriculares { get; set; }

        public long[] SeqTipoComponentesCurriculares { get; set; }

        public long? SeqAluno { get; set; }

        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqGrupoCurricular { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        /// <summary>
        /// Quando informado junto com a SeqMatrizCurricular, retorna apenas os assuntos deste componente
        /// </summary>
        public long? SeqComponenteCurricular { get; set; }
    }
}