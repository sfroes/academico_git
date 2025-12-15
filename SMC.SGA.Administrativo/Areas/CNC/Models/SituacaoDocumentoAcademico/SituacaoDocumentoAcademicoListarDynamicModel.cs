using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class SituacaoDocumentoAcademicoListarDynamicModel : SMCDynamicViewModel
    {

        
        [SMCOrder(0)]                
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]        
        [SMCOrder(1)]        
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCSelect(SortBy = SMCSortBy.Description)]
        [SMCOrder(3)]        
        [SMCSize(SMCSize.Grid10_24)]
        public List<string> ListaGruposDocumento { get; set; }
        
    }
}