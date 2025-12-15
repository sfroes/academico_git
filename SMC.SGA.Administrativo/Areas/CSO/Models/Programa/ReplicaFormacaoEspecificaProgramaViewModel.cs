using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    [SMCStepConfiguration(ActionStep = nameof(ProgramaController.ReplicarFormacaoEspecificaProgramaStepCurso))]
    [SMCStepConfiguration(ActionStep = nameof(ProgramaController.ReplicarFormacaoEspecificaProgramaStepTitulacao))]
    [SMCStepConfiguration(ActionStep = nameof(ProgramaController.ReplicarFormacaoEspecificaProgramaStepCursoOfertaLocalidade))]
    [SMCStepConfiguration(ActionStep = nameof(ProgramaController.ReplicarFormacaoEspecificaProgramaStepConfirmacao))]
    public class ReplicaFormacaoEspecificaProgramaViewModel : SMCViewModelBase, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable, ISMCStep
    {
        #region DataSources

        public List<SMCDatasourceItem> Cursos { get; set; }

        public List<SMCDatasourceItem> CursosOfertasLocalidades { get; set; }

        #endregion

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long SeqFormacaoEspecifica { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long SeqEntidadeResponsavel { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public List<CategoriaAtividade> CategoriasAtividadesSituacoesAtuais
        {
            get
            {
                return new List<CategoriaAtividade>()
                {
                    CategoriaAtividade.Ativa,
                    CategoriaAtividade.EmAtivacao
                };
            }
        }

        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCRequired]
        [SMCSelect(nameof(Cursos), UseCustomSelect = true)]
        [SMCStep(0)]
        public List<long> SeqsCursos { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCRequired]
        [SMCStep(0)]
        public DateTime DataInicioVigenciaFormacaoCurso { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCMinDate(nameof(DataInicioVigenciaFormacaoCurso))]
        [SMCStep(0)]
        public DateTime? DataFimVigenciaFormacaoCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1)]
        public List<ReplicaFormacaoEspecificaProgramaTitulacaoCursoViewModel> CursosTitulacoes { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCRequired]
        [SMCSelect(nameof(CursosOfertasLocalidades), UseCustomSelect = true)]
        [SMCStep(2)]
        public List<long> SeqsCursosOfertasLocalidades { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3)]
        public List<SMCTreeViewNode<ReplicaFormacaoEspecificaProgramaConfirmacaoViewModel>> ItensSelecionados { get; set; }
    }
}