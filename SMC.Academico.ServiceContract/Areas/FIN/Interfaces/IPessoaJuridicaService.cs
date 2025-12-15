using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Framework.Dynamic;
using SMC.Framework.Model;
using SMC.Academico.ServiceContract.Areas.FIN.Data;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IPessoaJuridicaService : ISMCService
    {
        /// <summary>
        /// Salvar uma pessoa juridica e suas entidades
        /// </summary>
        /// <param name="pessoaJuridica">Dados da pessoa juridica a ser salva</param>
        /// <returns>Sequencial da pessoa juridica</returns>
        long SalvarPessoaJuridia(PessoaJuridicaData pessoaJuridica);

        SMCPagerData<PessoaJuridicaData> BuscarPessoasJuridicas(PessoaJuridicaData filtro);

        PessoaJuridicaData BuscarPessoaJuridica(long seq);
    }
}
