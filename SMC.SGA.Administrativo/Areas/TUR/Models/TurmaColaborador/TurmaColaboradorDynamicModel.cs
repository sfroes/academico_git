using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaColaboradorDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCParameter]
        public override long Seq { get; set; }

        [SMCHidden]        
        public long SeqTurma { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<TurmaColaboradorViewModel> Colaborador { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Ajax()
                   .EditInModal(refreshIndexPageOnSubmit: true)
                   .RequiredIncomingParameters(nameof(Seq))
                   .RedirectIndexTo("Index", "DivisaoTurmaColaborador", x => new { SeqTurma = new SMCEncryptedLong((x as TurmaColaboradorDynamicModel).SeqTurma) })
                   .Header("CabecalhoTurma")
                   .Service<ITurmaColaboradorService>(edit: nameof(ITurmaColaboradorService.BuscarTurmaColaborador),
                                                      save: nameof(ITurmaColaboradorService.SalvarTurmaColaborador))
                   .Tokens(tokenInsert: UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA,
                              tokenEdit: UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA,
                            tokenRemove: UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA,
                              tokenList: UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA);
        }

        #endregion [ Configurações ]
    }
}