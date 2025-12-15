using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoNivelResponsavel { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public long? Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string Sigla { get; set; }

        public int? CodigoComponenteLegado { get; set; }

        public string BancoLegado { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public long? SeqEntidade { get; set; }

        public bool? Ativo { get; set; }

        [SMCKeyModel]
        public long[] SeqComponentesCurriculares { get; set; }

        [SMCKeyModel]
        public long[] SeqTipoComponentesCurriculares { get; set; }

        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqGrupoCurricular { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        /// <summary>
        /// Quando informado junto com a SeqMatrizCurricular, retorna apenas os assuntos deste componente
        /// </summary>
        public long? SeqComponenteCurricular { get; set; }

    }
}