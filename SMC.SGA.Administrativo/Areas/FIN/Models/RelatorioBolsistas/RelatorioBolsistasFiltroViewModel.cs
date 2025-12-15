using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class RelatorioBolsistasFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        /// <summary>
        /// Seleção única
        /// NV01 - Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética
        /// </summary>
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> Beneficios { get; set; }

        public List<SMCDatasourceItem> TiposAtuacoes { get; set; }

        #endregion [ DataSources ]     

        #region Propriedades Auxiliares

        //[SMCHidden]
        //[SMCDependency(nameof(TipoAtuacoes), nameof(RelatorioBolsistasController.VerificarAlunoSelecionado), "RelatorioBolsistas", false)]
        //public bool? AlunoSelecionado { get; set; }

        #endregion

        [SMCConditional(SMCConditionalBehavior.Required, nameof(SituacaoBeneficio), SMCConditionalOperation.NotEqual, SituacaoChancelaBeneficio.AguardandoChancela)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataInicioReferencia { get; set; }

        [SMCConditional(SMCConditionalBehavior.Required, nameof(SituacaoBeneficio), SMCConditionalOperation.NotEqual, SituacaoChancelaBeneficio.AguardandoChancela)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataFimReferencia { get; set; }

        ///// <summary>
        ///// Tipo de atuação 
        ///// Seleção múltipla
        ///// Valores: "Aluno" e "Ingressante".
        ///// Por default o valor "Aluno" virá selecionado.
        ///// </summary>
        [SMCRequired]
        [SMCSelect(nameof(TiposAtuacoes), ForceMultiSelect = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public List<long> TipoAtuacoes { get; set; } = new List<long> { (long)TipoAtuacao.Aluno };

        /// <summary>
        /// Sequencial do ciclo letivo de ingresso do aluno
        /// Exibido quando o tipo de atuação for Aluno
        /// </summary>
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        //[SMCConditionalReadonly(nameof(AlunoSelecionado), SMCConditionalOperation.Equals, false)]
        public CicloLetivoLookupViewModel SeqCicloLetivoIngresso { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV01 - Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética
        /// </summary>
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSelect(nameof(EntidadesResponsaveis), ForceMultiSelect = true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        /// <summary>
        /// Benefício         
        /// Seleção múltipla
        /// NV03 Listar em ordem alfabética os benefícios associados ao nível de ensino da pessoa atuação em questão e,
        /// da instituição de ensino logada.
        /// NV04 Caso o campo tenha apenas um registro para seleção o mesmo deverá ser preenchido automaticamente.
        /// </summary>
        [SMCSelect(nameof(Beneficios), ForceMultiSelect = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid16_24, SMCSize.Grid6_24)]
        public List<long> SeqsBeneficios { get; set; }

        /// <summary>
        /// Situação do benefício
        /// Seleção única
        /// Valores:
        /// · Aguardando chancela
        /// · Deferido
        /// · Indeferido
        /// · Cancelado
        /// </summary> 
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public SituacaoChancelaBeneficio SituacaoBeneficio { get; set; }

        /// <summary>
        /// Exibir parcelas em aberto
        /// Por defaut deve vir desmarcado
        /// Seleção única 
        /// </summary>
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCRadioButtonList]
        public bool ExibirParcelasEmAberto { get; set; }

        /// <summary>
        /// Exibir referência do contrato no sistema financeiro
        /// Por defaut deve vir desmarcado
        /// Seleção única 
        /// </summary>
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool ExibirReferenciaContrato { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqNivelEnsino { get; set; }

    }
}