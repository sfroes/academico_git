using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "Professor", Size = SMCSize.Grid24_24)]
    public class TurmaColaboradorDivisaoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCParameter]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqTurma { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqDivisao { get; set; }

        [SMCHidden]
        public string DescricaoTipoOrganizacao { get; set; }

        [ColaboradorLookup]
        [SMCGroupedProperty("Professor")]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCDependency(nameof(SeqTurma))]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid19_24)]
        public ColaboradorLookupNomeViewModel SeqColaborador { get; set; }

        [SMCGroupedProperty("Professor")]
        [SMCMask("999")]
        [SMCMinValue(1)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid5_24)]
        public short? QuantidadeCargaHoraria { get; set; }

        [SMCHidden]
        public bool ExisteHistoricoEscolarAluno { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Ajax()
                   .Assert("MSG_Alerta_Historico_Escolar", x => (x as TurmaColaboradorDivisaoDynamicModel).ExisteHistoricoEscolarAluno)
                   .EditInModal(refreshIndexPageOnSubmit: true)
                   .RequiredIncomingParameters(nameof(Seq))
                   .RedirectIndexTo("Index", "DivisaoTurmaColaborador", x => new { SeqTurma = new SMCEncryptedLong((x as TurmaColaboradorDivisaoDynamicModel).SeqTurma) })
                   .Header("CabecalhoDivisao")
                   .Service<IDivisaoTurmaColaboradorService>(edit: nameof(IDivisaoTurmaColaboradorService.BuscarDivisaoTurmaColaborador),
                                                             insert: nameof(IDivisaoTurmaColaboradorService.BuscarConfiguracaoDivisaoTurmaColaborador),
                                                               save: nameof(IDivisaoTurmaColaboradorService.SalvarDivisaoTurmaColaborador))
                   .Tokens(tokenInsert: UC_TUR_001_02_02.ASSOCIAR_PROFESSOR_DIVISAO_TURMA,
                              tokenEdit: UC_TUR_001_02_02.ASSOCIAR_PROFESSOR_DIVISAO_TURMA,
                            tokenRemove: UC_TUR_001_02_02.ASSOCIAR_PROFESSOR_DIVISAO_TURMA,
                              tokenList: UC_TUR_001_02_02.ASSOCIAR_PROFESSOR_DIVISAO_TURMA);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);
        }

        #endregion [ Configurações ]
    }
}