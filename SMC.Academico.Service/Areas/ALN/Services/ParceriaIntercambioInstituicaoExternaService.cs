using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Framework.Specification;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Academico.Domain.Areas.ALN.Specifications;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class ParceriaIntercambioInstituicaoExternaService : SMCServiceBase, IParceriaIntercambioInstituicaoExternaService
    {
        #region [ Services ]

        private ParceriaIntercambioInstituicaoExternaDomainService ParceriaIntercambioInstituicaoExternaDomainService
        {
            get { return this.Create<ParceriaIntercambioInstituicaoExternaDomainService>(); }
        }

        #endregion [ Services ]
        

        public List<SMCDatasourceItem> BuscarParceriaIntercambioInstituicoesExternasSelect(long seqParceriaIntercambio, bool? ativo = null)
        { 
            var spec = new ParceriaIntercambioInstituicaoExternaSpecification() { SeqParceriaIntercambio = seqParceriaIntercambio, Ativo = ativo };
            spec.SetOrderBy(o => o.InstituicaoExterna.Nome);
            var instituicoesExternas = ParceriaIntercambioInstituicaoExternaDomainService.SearchBySpecification(spec, IncludesParceriaIntercambioInstituicaoExterna.InstituicaoExterna); 
             
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in instituicoesExternas)
                retorno.Add(new SMCDatasourceItem(item.Seq, item.InstituicaoExterna.Nome));

            return retorno;
        }
    }
}
