using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Framework.Model;
using SMC.Academico.Domain.Areas.CUR.DomainServices;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class QualisPeriodicoService : SMCServiceBase, IQualisPeriodicoService
    {
        #region Serviços
            
        private QualisPeriodicoDomainService QualisPeriodicoDomainService
        {
            get { return this.Create<QualisPeriodicoDomainService>(); }
        }

        #endregion

        public List<SMCDatasourceItem> BuscarAreaAvaliacaoSelect()
        {
            return QualisPeriodicoDomainService.BuscarAreaAvaliacaoSelect();
        }
    }
}
