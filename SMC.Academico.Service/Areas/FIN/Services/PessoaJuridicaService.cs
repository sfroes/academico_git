using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Framework.Extensions;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Model;
using SMC.Academico.Domain.Areas.FIN.Specifications;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class PessoaJuridicaService : SMCServiceBase, IPessoaJuridicaService
    {
        #region Service

        private PessoaJuridicaDomainService PessoaJuridicaDomainService
        {
            get { return this.Create<PessoaJuridicaDomainService>(); }
        }

        #endregion Service

        public long SalvarPessoaJuridia(PessoaJuridicaData pessoaJuridica)
        {
            var pessoaJuridicaTr = pessoaJuridica.Transform<PessoaJuridica>();
            return PessoaJuridicaDomainService.SalvarPessoaJuridica(pessoaJuridicaTr);
        }

        public SMCPagerData<PessoaJuridicaData> BuscarPessoasJuridicas(PessoaJuridicaData filtro)
        {
            return PessoaJuridicaDomainService.BuscarPessoasJuridicas(filtro.Transform<PessoaJuridicaFilterSpecification>()).Transform<SMCPagerData<PessoaJuridicaData>>();
        }

        public PessoaJuridicaData BuscarPessoaJuridica(long seq)
        {
            return PessoaJuridicaDomainService.BuscarPessoaJuridica(seq).Transform<PessoaJuridicaData>();
        }
    }
}
