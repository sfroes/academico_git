using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.MAT.Controllers;
using System.Collections.Generic;


namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioAlunosPorSituacaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        /// <summary>
        /// Seleção única
        /// NV01 - Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética
        /// </summary>
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV03 - Listar os tipos de situação cadastrados no sistema, em ordem alfabética.
        /// </summary>
        public List<SMCDatasourceItem> TiposSituacaoMatricula { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV05 - Listar os vínculos cadastrados no sistema em ordem alfabética.
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        #endregion [ DataSources ]

        [SMCRequired]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        /// <summary>
        /// Seleção única
        /// NV02 - O campo "Turno" será filtrado de acordo com o valor selecionado no lookup 
        /// "Oferta de Curso por Localidade". Se o valor não for selecionado, listar em ordem 
        /// alfabética todos os turnos cadastrados no sistema.
        /// </summary>
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(RelatorioAlunosPorSituacaoController.BuscarTurnosCursoOfertaLocalidadeSelect), "RelatorioAlunosPorSituacao", "MAT", false)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public long? SeqTurno { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV05 - Listar os vínculos cadastrados no sistema em ordem alfabética.
        /// </summary>
        //[SMCFilter(true, true)]
        [SMCSelect(nameof(TiposVinculoAluno))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public List<long> SeqsTipoVinculoAluno { get; set; }



        /// <summary>
        /// Seleção única
        /// Valores: "Aluno" e "Ingressante". Por default "Aluno" vem selecionado.
        /// </summary>
        [SMCRequired]
        [SMCSelect(IgnoredEnumItems = new object[] { TipoAtuacao.Colaborador })]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public TipoAtuacao TipoAtuacao { get; set; } = TipoAtuacao.Aluno;

        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCConditionalReadonly(nameof(TipoAtuacao), SMCConditionalOperation.NotEqual, TipoAtuacao.Aluno)]
        public CicloLetivoLookupViewModel SeqCicloLetivoIngresso { get; set; }

        /// <summary>
        /// NV07 Os campos "Tipo de Situação" e "Situação de Matrícula" ficarão visíveis somente se o 
        /// valor selecionado no · campo "Tipo de atuação" for "Aluno". 
        /// Seleção múltipla
        /// NV03 - Listar os tipos de situação cadastrados no sistema, em ordem alfabética.
        /// </summary>
        [SMCSelect(nameof(TiposSituacaoMatricula))]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public List<long> SeqsTipoSituacaoMatricula { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV07 Os campos "Tipo de Situação" e "Situação de Matrícula" ficarão visíveis somente se o 
        /// valor selecionado no · campo "Tipo de atuação" for "Aluno". 
        /// NV04 - O campo "Situação de matrícula" será filtrado de acordo com o 
        /// valor selecionado no campo "Tipo de situação". Se o valor não for selecionado, 
        /// listar todas as situações de matrícula cadastradas no sistema, em ordem alfabética.
        /// </summary>
        [SMCDependency(nameof(SeqsTipoSituacaoMatricula), nameof(RelatorioAlunosPorSituacaoController.BuscarSituacoesMatricula), "RelatorioAlunosPorSituacao", "MAT", false)]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        public List<long> SeqsSituacaoMatricula { get; set; }

        /// <summary>
        /// O campo "Situação" ficará visível somente se o valor selecionado no campo "Tipo de atuação" for "Ingressante".
        /// Seleção múltipla
        /// NV07 - Valores: 
        ///     "Aguardando liberação para matrícula", 
        ///     "Apto para matrícula", 
        ///     "Cancelado (Prouni)", 
        ///     "Desistente" e 
        ///     "Matriculado".
        /// </summary>
        [SMCSelect(IncludedEnumItems = new object[] { SituacaoIngressante.AguardandoLiberacaMatricula
                                                    , SituacaoIngressante.AptoMatricula
                                                    , SituacaoIngressante.Cancelado
                                                    , SituacaoIngressante.Desistente
                                                    , SituacaoIngressante.Matriculado
        })]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Ingressante)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public List<SituacaoIngressante> SituacoesIngressante { get; set; }

        /// <summary>
        /// Seleção única
        /// NV01 - Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética
        /// </summary>
        [SMCRequired]
        [SMCSelect(nameof(EntidadesResponsaveis), ForceMultiSelect = true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid9_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCHidden]
        public List<long> SelectedValues { get; set; }

        #region Botão Gerar
        /// <summary>
        /// Parâmetro de filtro para informar o comando de gerar arquivo de texto.
        /// </summary>
        public bool GerarArquivo { get; set; }

        /// <summary>
        /// Parametro de filtro de exibição do botão para gerar arquivo de texto.
        /// </summary>
        public bool ExibirBotaoGerarArquivo { get; set; }
        #endregion
    }
}