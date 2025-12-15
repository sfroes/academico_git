using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrientacaoColaboradorMasterDetailsViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCDataSource]
        [SMCRequired]
        [SMCSelect(nameof(TurmaOrientacaoDynamicModel.ListaColaboradores), NameDescriptionField = nameof(ColaboradorNameDescriptionField))]
        [SMCSize(SMCSize.Grid18_24)]
        [SMCUnique]
        public string SeqColaborador { get; set; }

        [SMCReadOnly]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        [SMCIgnoreProp]
        public string ColaboradorNameDescriptionField { get; set; }
    }
}