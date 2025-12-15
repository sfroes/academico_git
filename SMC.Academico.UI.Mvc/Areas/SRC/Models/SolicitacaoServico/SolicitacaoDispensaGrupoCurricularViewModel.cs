using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDispensaGrupoCurricularViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqGrupoCurricular), nameof(SolicitacaoServicoFluxoBaseController.ValidarFormatoConfiguracaoGrupoCurricular), null, true)]
        public bool BloquearTotalDispensado { get; set; }

        [GrupoCurricularLookup]
        [SMCSize(SMCSize.Grid18_24)]
        [SMCDependency("SeqCurriculoCursoOferta")]
        [SMCDependency("DesconsiderarItensQueNaoPermitemCadastroDispensa")]
        [SMCDependency("DesconsiderarItensCursadosAprovacaoOuDispensadosAluno")]
        [SMCDependency("DesconsiderarItensVinculadosAoCurriculoCursoOferta")]
        [SMCDependency("DesconsiderarGruposTodosItensObrigatorios")]
        [SMCDependency("SeqPessoaAtuacao")]
        [SMCDependency("FiltrarFormacoesEspecificasAluno")]
        [SMCDependency("PermitirSelecionarGruposComComponentes")]
        [SMCRequired]
        [SMCUnique]
        public GrupoCurricularLookupViewModel SeqGrupoCurricular { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(SeqGrupoCurricular), SMCConditionalOperation.Equals, 0, "", null, RuleName = "R1", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(BloquearTotalDispensado), SMCConditionalOperation.Equals, true, RuleName = "R2", PersistentValue = false)]
        [SMCConditionalRequired(nameof(SeqGrupoCurricular), SMCConditionalOperation.GreaterThen, 0, RuleName = "R3")]
        [SMCConditionalRequired(nameof(BloquearTotalDispensado), SMCConditionalOperation.NotEqual, true, RuleName = "R4")]
        [SMCMinValue(1)]
        [SMCConditionalRule("R3 && R4")]
        [SMCConditionalRule("R1 || R2")]
        // Segundo e-mail da Luciana enviado 13 de setembro de 2018 13:57
        // - O campo “Total a ser dispensado” não deverá ser exibido preenchido, pelo menos por enquanto. 
        //[SMCDependency(nameof(SeqGrupoCurricular), nameof(SolicitacaoServicoFluxoBaseController.PreencherTotalDispensado), null, true)]
        public short? QuantidadeDispensaGrupo { get; set; }
    }
}