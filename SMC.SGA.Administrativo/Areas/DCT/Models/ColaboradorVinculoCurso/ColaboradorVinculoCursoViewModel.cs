using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoCursoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCIgnoreProp]
        [SMCDataSource]
        public List<SMCDatasourceItem> CursoOfertasLocalidades { get; set; }

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqColaborador { get; set; }

        [SMCHidden]
        public long SeqColaboradorVinculo { get; set; }

        [SMCHidden]
        public long seqEntidadeVinculo { get; set; }        

        [SMCDetail]        
        public SMCMasterDetailList<CursoViewModel> Cursos { get; set; }
    }
}