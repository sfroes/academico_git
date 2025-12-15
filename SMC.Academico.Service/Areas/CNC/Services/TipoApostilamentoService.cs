using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class TipoApostilamentoService : SMCServiceBase, ITipoApostilamentoService
    {
        #region [ DomainService ]

        private TipoApostilamentoDomainService TipoApostilamentoDomainService => Create<TipoApostilamentoDomainService>();

        #endregion [ DomainService ]

        public TipoApostilamentoData BuscarTipoApostilamento(long seq)
        {
            return TipoApostilamentoDomainService.BuscarTipoApostilamento(seq).Transform<TipoApostilamentoData>();
        }

        public List<SMCDatasourceItem> BuscarTiposApostilamentoSelect()
        {
            return this.TipoApostilamentoDomainService.BuscarTiposApostilamentoSelect();
        }

        public List<SMCDatasourceItem> BuscarTiposApostilamentoSemTokenFormacaoSelect()
        {
            return this.TipoApostilamentoDomainService.BuscarTiposApostilamentoSemTokenFormacaoSelect();
        }
    }
}
