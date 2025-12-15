using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class PessoaAtuacaoTermoIntercambioService : SMCServiceBase, IPessoaAtuacaoTermoIntercambioService
    {
        #region DomainService 

        private PessoaAtuacaoTermoIntercambioDomainService PessoaAtuacaoTermoIntercambioDomainService => Create<PessoaAtuacaoTermoIntercambioDomainService>();

        #endregion

        public SMCPagerData<PessoaAtuacaoTermoIntercambiListaData> BuscarPessoaAtuacaoTermoIntercambio(PessoaAtuacaoTermoIntercambioFiltroData filtros)
        {
            if (filtros.SeqInstituicaoExterna == null || filtros.SeqInstituicaoExterna.Seq == null)
            {
                filtros.SeqInstituicaoEnsino = null;
            }
            else
            {
                filtros.SeqInstituicaoEnsino = filtros.SeqInstituicaoExterna.Seq;
            }

            return PessoaAtuacaoTermoIntercambioDomainService.BuscarListaPessoaAtuacaoTermoIntercambio(filtros.Transform<PessoaAtuacaoTermoIntercambioFilterSpecification>())
                                                                                                              .Transform<SMCPagerData<PessoaAtuacaoTermoIntercambiListaData>>();             
        }

        public PessoaAtuacaoTermoIntercambioPeriodoData BuscarPeriodoIntercambio(long seq)
        {
            var periodoIntercambio = PessoaAtuacaoTermoIntercambioDomainService.BuscarPeriodoIntercambio(seq).Transform<PessoaAtuacaoTermoIntercambioPeriodoData>();
            return periodoIntercambio;
        }

        public long SalvarPeriodoIntercambio(PessoaAtuacaoTermoIntercambioSalvarPeriodoData periodo)
        {
            return PessoaAtuacaoTermoIntercambioDomainService.SalvarPeriodoIntercambio(periodo.Transform<PeriodoIntercambioVO>());
        }
    }
}
