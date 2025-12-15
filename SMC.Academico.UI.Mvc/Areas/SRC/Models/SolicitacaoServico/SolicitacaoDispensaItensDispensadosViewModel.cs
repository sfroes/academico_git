using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDispensaItensDispensadosViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region [ DataSource ]

        [SMCDataSource(SMCStorageType.Session)]
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        #endregion [ DataSource ]

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_DISPENSA_ITENS_SEREM_DISPENSADOS;

        [SMCHidden]
        public long? SeqInstituicaoNivelResponsavel { get; set; }

        [SMCHidden]
        public bool ExibirGrupoComponente { get; set; }

        [SMCHidden]
        public long? SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        public bool DesconsiderarItensQueNaoPermitemCadastroDispensa { get { return true; } }

        [SMCHidden]
        public bool DesconsiderarItensCursadosAprovacaoOuDispensadosAluno { get { return true; } }

        [SMCHidden]
        public bool DesconsiderarGruposTodosItensObrigatorios { get { return true; } }

        [SMCHidden]
        public bool DesconsiderarItensVinculadosAoCurriculoCursoOferta { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public bool FiltrarFormacoesEspecificasAluno { get; set; } = true;

        [SMCHidden]
        public bool BloquearCicloLetivo { get; set; }

        [SMCHidden]
        public bool PermitirSelecionarGruposComComponentes { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCRequired]
        [SMCSelect(nameof(CiclosLetivos))]
        [SMCConditionalReadonly(nameof(BloquearCicloLetivo), SMCConditionalOperation.Equals, true, PersistentValue = true/*, RuleName = "R1"*/)]
        //[SMCConditionalReadonly(nameof(ComponentesCurriculares), SMCConditionalOperation.NotEqual, null, PersistentValue = true, RuleName = "R2")]
        //[SMCConditionalRule("R1 || R2")]
        public long SeqCicloLetivo { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SolicitacaoDispensaComponenteCurricularViewModel> ComponentesCurriculares { get; set; }

        /// <summary>
        /// Componentes curriculares que vieram por carga mas estão fora da matriz do aluno. Devem ser exibidos na tela como mensagem de alerta para tomada de decisão
        /// </summary>
        [SMCHidden]
        public List<SMCDatasourceItem> ComponentesCurricularesForaMatriz { get; set; }

        [SMCHidden]
        public long SeqMatrizCurricular { get; set; }

        [SMCConditionalDisplay(nameof(ExibirGrupoComponente), SMCConditionalOperation.Equals, "true")]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SolicitacaoDispensaGrupoCurricularViewModel> GruposCurriculares { get; set; }

        public float CursadosTotalCargaHorariaHoras { get; set; }

        public float CursadosTotalCargaHorariaHorasAula { get; set; }

        public float CursadosTotalCreditos { get; set; }

        public float DispensaTotalCargaHorariaHoras { get; set; }

        public float DispensaTotalCargaHorariaHorasAula { get; set; }

        public float DispensaTotalCreditos { get; set; }
    }
}