using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class ClassificacaoPeriodicoService : SMCServiceBase, IClassificacaoPeriodicoService
    {
        #region Serviços

        private ClassificacaoPeriodicoDomainService ClassificacaoPeriodicoDoaminService
        {
           get { return this.Create<ClassificacaoPeriodicoDomainService>(); }
        }

        #endregion

        public List<SMCDatasourceItem> BuscarClassificacaoPeriodicoSelect()
        {
            return this.ClassificacaoPeriodicoDoaminService.BuscarClassificacaoPeriodicoSelect();
        }
        
    }
}
