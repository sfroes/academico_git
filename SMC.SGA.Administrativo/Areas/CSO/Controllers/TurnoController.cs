using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class TurnoController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private ITurnoService TurnoService
        {
            get { return this.Create<ITurnoService>(); }
        }

        private IInstituicaoNivelTurnoService InstituicaoNivelTurnoService
        {
            get { return this.Create<IInstituicaoNivelTurnoService>(); }
        }

        #endregion [ Service ]

        [SMCAuthorize(UC_CSO_001_09_01.MANTER_TURNO)]
        public JsonResult BuscarTurnoPorCursoOfertaSelectLookup(CursoOfertaLookupViewModel seqCursoOferta)
        {
            List<SMCDatasourceItem> listItens = new List<SMCDatasourceItem>();

            if (seqCursoOferta != null && seqCursoOferta.Seq > 0)
                listItens = TurnoService.BuscarTurnosPorCursoOfertaSelect(seqCursoOferta.Seq.Value);
            else
                listItens = InstituicaoNivelTurnoService.BuscarTurnosPorInstituicaoSelect();

            return Json(listItens);
        }

        /// <summary>
        /// Busca os turno por oferta de curso ou localidade segundo a regra BI_CSO_002.NV05 - Seleção Oferta de Curso - Nível de Ensino
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta associado aos turnos</param>
        /// <param name="seqLocalidade">Sequencial da localidade associada aos turnos</param>
        /// <returns>Lista de turnos associados às entidades informadas ou todos turnos caso nenhuma entidade seja informada</returns>
        [SMCAllowAnonymous]
        public JsonResult BuscarTurnosPorCursoOfertaOuLocalidadeSelect(long? seqCursoOferta, long? seqLocalidade)
        {
            return Json(TurnoService.BuscarTurnosSelect(new TurnoFiltroData()
            {
                SeqCursoOferta = seqCursoOferta,
                SeqLocalidade = seqLocalidade
            }));
        }       
    }
}