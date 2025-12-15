using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqInstituicaoNivelResponsavel { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        [SMCMapProperty("TipoComponente.Descricao")]
        public string DescricaoTipoComponenteCurricular { get; set; }

        [SMCMapProperty("TipoComponente.Sigla")]
        public string SiglaTipoComponenteCurricular { get; set; }

        public string DescricaoCompleta { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoReduzida { get; set; }

        public string Sigla { get; set; }

        public short? CargaHoraria { get; set; }

        public short? QuantidadeSemanas { get; set; }

        public short? QuantidadeHorasCredito { get; set; }

        public short? Credito { get; set; }

        public bool Ativo { get; set; }

        public string Observacao { get; set; }

        public bool ExigeAssuntoComponente { get; set; }

        public List<ComponenteCurricularOrgaoRegulador> OrgaosReguladores { get; set; }

        public TipoOrgaoRegulador RegistroTipoOrgaoRegulador { get; set; }

        public List<ComponenteCurricularEmenta> Ementas { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public List<ComponenteCurricularOrganizacao> Organizacoes { get; set; }

        public List<ComponenteCurricularNivelEnsino> NiveisEnsino { get; set; }

        public List<ComponenteCurricularEntidade> EntidadesResponsaveis { get; set; }

        public bool DescricaoReduzidaObrigatorio { get; set; }

        public bool SiglaObrigatorio { get; set; }

        public bool CargaHorariaDisplay { get; set; }

        public bool CargaHorariaObrigatorio { get; set; }

        public bool CreditoDisplay { get; set; }

        public bool CreditoObrigatorio { get; set; }

        public bool EmentaDisplay { get; set; }

        public bool EmentaObrigatorio { get; set; }

        public bool TipoOrganizacaoDisplay { get; set; }

        public bool OrganizacoesDisplay { get; set; }

        public bool OrgaoReguladorDisplay { get; set; }

        public bool EntidadesResponsaveisObrigatorio { get; set; }

        public bool PermiteAssuntoComponente { get; set; }

        public List<ConfiguracaoComponente> Configuracoes { get; set; }

        public List<SMCDatasourceItem> EntidadesResponsavel { get; set; }

        public bool PossuiAssociacaoDivisaoMatrizes { get; set; }
        public bool PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz { get; set; }

        public InstituicaoNivelTipoComponenteCurricular ConfiguracaoTipoComponenteCurricular { get; set; }

        [SMCMapMethod("RecuperarTipoGestaoDivisaoComponente")]
        public List<TipoGestaoDivisaoComponente> TipoGestaoDivisoesComponente { get; set; }

        public short? NumeroVersaoCarga { get; set; }
    }
}