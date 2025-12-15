using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDispensaComponenteCurricularViewModel : SMCViewModelBase
    {
        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Assuntos { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqGrupoCurricularComponente), nameof(SolicitacaoServicoFluxoBaseController.ValidarAssuntoComponenteCurricular), null, true, includedProperties: new string[] { "SeqComponenteCurricular", "SeqCicloLetivo", "SeqPessoaAtuacao" })]
        public bool? ExibirAssunto { get; set; }

        [GrupoCurricularComponenteLookup(true)]
        [SMCConditional("smc.sga.solicitacaoDispensaItensDispensados.conditionalTabular", "ExibirGrupoComponente", SMCConditionalOperation.Equals, "true")]
        [SMCConditionalDisplay("ExibirGrupoComponente", SMCConditionalOperation.Equals, "true")]
        [SMCConditionalRequired("ExibirGrupoComponente", SMCConditionalOperation.Equals, "true")]
        [SMCDependency("FiltrarFormacoesEspecificasAluno")]
        [SMCDependency("SeqPessoaAtuacao")]
        [SMCDependency("PermitirSelecionarGruposComComponentes")]
        //[SMCDependency("SeqCicloLetivo")]
        [SMCDependency("SeqMatrizCurricular")]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid13_24)]
        [SMCUnique]
        public GrupoCurricularComponenteLookupViewModel SeqGrupoCurricularComponente { get; set; }

        [ComponenteCurricularLookup]
        [SMCConditional("smc.sga.solicitacaoDispensaItensDispensados.conditionalTabular", "ExibirGrupoComponente", SMCConditionalOperation.Equals, "false")]
        [SMCConditionalDisplay("ExibirGrupoComponente", SMCConditionalOperation.Equals, "false")]
        [SMCConditionalRequired("ExibirGrupoComponente", SMCConditionalOperation.Equals, "false")]
        [SMCDependency("SeqInstituicaoNivelResponsavel")]
        [SMCDependency("FiltrarFormacoesEspecificasAluno")]
        [SMCDependency("SeqMatrizCurricular")]
        [SMCOrder(2)]
        [SMCUnique]
        [SMCSize(SMCSize.Grid13_24)]
        public ComponenteCurricularLookupViewModel SeqComponenteCurricular { get; set; }

        [SMCSelect(nameof(Assuntos))]
        [SMCConditionalReadonly(nameof(ExibirAssunto), SMCConditionalOperation.NotEqual, true)]
        [SMCConditionalRequired(nameof(ExibirAssunto), SMCConditionalOperation.Equals, true)]
        [SMCOrder(3)]
        [SMCUnique]
        [SMCSize(SMCSize.Grid9_24)]
        [SMCDependency(nameof(SeqGrupoCurricularComponente), nameof(SolicitacaoServicoFluxoBaseController.PreencherComponenteCurricularAssunto), null, true, includedProperties: new string[] { "SeqComponenteCurricular", "SeqCicloLetivo", "SeqPessoaAtuacao" })]
        public long? SeqComponenteCurricularAssunto { get; set; }
    }
}