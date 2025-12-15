using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class InstituicaoNivelTipoComponenteCurricularData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("InstituicaoNivel.SeqInstituicaoEnsino")]
        public long SeqInstituicaoEnsino { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public bool EntidadeResponsavelObrigatoria { get; set; }

        public short? QuantidadeMaximaEntidadeResponsavel { get; set; }

        public long? TipoEntidadeResponsavel { get; set; }

        public bool PermiteConfiguracaoComponente { get; set; }

        public bool PermiteCadastroRequisito { get; set; }

        public bool PermiteCadastroDispensa { get; set; }

        public bool PermiteAssuntoComponente { get; set; }

        public bool PermiteSubdivisaoOrganizacao { get; set; }

        public bool NomeReduzidoObrigatorio { get; set; }

        public bool SiglaObrigatoria { get; set; }

        public bool QuantidadeVagasPrevistasObrigatorio { get; set; }

        public bool CriterioAprovacaoObrigatorio { get; set; }

        public long? SeqCriterioAprovacaoPadrao { get; set; }

        public bool PermiteEmenta { get; set; }

        public short? QuantidadeMinimaCaracteresEmenta { get; set; }

        public bool ExigeCargaHoraria { get; set; }

        public bool ExigeCredito { get; set; }

        public bool ExibeCargaHoraria { get; set; }
         
        public bool ExibeCredito { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public short? QuantidadeHorasPorCredito { get; set; }

        public IList<InstituicaoNivelTipoDivisaoComponenteData> TiposDivisaoComponente { get; set; }

        public List<SMCDatasourceItem> TiposDivisao { get; set; }

        [SMCMapProperty("InstituicaoNivel.TipoOrgaoRegulador")]
        public TipoOrgaoRegulador TipoOrgaoRegulador { get; set; }

        [SMCMapProperty("InstituicaoNivel.PermiteCreditoComponenteCurricular")]
        public bool InstituicaoNivelExigeCredito { get; set; }
    }
}