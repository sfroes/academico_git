using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Models;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    // Dados Pessoais Tab0
    [SMCStepConfiguration]
    // Contatos Tab1
    [SMCStepConfiguration]
    [SMCGroupedPropertyConfiguration(GroupId = "Nacionalidade", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "Naturalidade", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoPassaporte", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoRg", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoIdentidadeEstrangeira", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoCnh", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoTituloEleitor", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoPisPasep", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoMilitar", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "RegistroProfissional", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "NecessiadesEspeciais", Size = SMCSize.Grid12_24)]
    public class AlunoDynamicModel : PessoaAtuacaoViewModel, ISMCMappable
    {
        #region [ Controles dynamic ]

        [SMCHidden]
        [SMCStep(3, 1)]
        public override TipoAtuacao TipoAtuacao { get => TipoAtuacao.Aluno; }

        [SMCIgnoreProp]
        public override object BuscaPessoaExistenteRouteValues { get => null; }

        #endregion [ Controles dynamic ]

        [SMCHidden]
        [SMCStep(0)]
        public long NumeroRegistroAcademico { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long SeqTipoVinculoAluno { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public DateTime? DataExclusao { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public int? CodigoAlunoMigracao { get; set; }

        #region [ Configuracao ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Header(nameof(AlunoController.BuscarAlunoCabecalho))
                   .Detail<AlunoListarDynamicModel>("_DetailList")
                   .ConfigureButton((button, model, action) =>
                   {
                       if(action == SMCDynamicButtonAction.Insert)
                       {
                      
                           button 
                            .Action("ModalSincronizarDadosAluno", "Aluno")
                            .Attribute(SMCMvcConsts.MODAL_ID, "modalMigracao")
                            .Ajax()
                            .Text("Importar");
                       }
                   })
                   .Tab()
                   .DisableInitialListing(true)
                   .ModalSize(SMCModalWindowSize.Largest)
                   .Service<IAlunoService>(index: nameof(IAlunoService.BuscarAlunos),
                                           save: nameof(IAlunoService.SalvarAluno),
                                           edit: nameof(IAlunoService.BuscarAluno))
                   .Ajax()
                   .Tokens(tokenInsert: UC_ALN_001_01_03.IMPORTAR_ALUNO,
                           tokenEdit: UC_ALN_001_01_02.MANTER_ALUNO,
                           tokenList: UC_ALN_001_01_01.PESQUISAR_ALUNO,
                           tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION);
        }

        #endregion [ Configuracao ]
    }
}