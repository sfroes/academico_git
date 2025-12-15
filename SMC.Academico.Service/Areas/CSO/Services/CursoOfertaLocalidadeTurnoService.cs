using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class CursoOfertaLocalidadeTurnoService : SMCServiceBase, ICursoOfertaLocalidadeTurnoService
    {
        #region [ Service ]

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeTurnoDomainService>(); }
        }

        public List<SMCDatasourceItem> BuscarTurnosPorLocalidadeCursoOfertaSelect(long? seqLocalidade, long? seqCursoOferta)
        {
            return this.CursoOfertaLocalidadeTurnoDomainService.BuscarTurnosPorLocalidadeCusroOfertaSelect(seqLocalidade, seqCursoOferta);
        }

        #endregion [ Service ]
    }
}