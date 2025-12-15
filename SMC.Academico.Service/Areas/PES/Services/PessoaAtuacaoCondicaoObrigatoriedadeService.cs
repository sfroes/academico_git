using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Service;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaAtuacaoCondicaoObrigatoriedadeService : SMCServiceBase, IPessoaAtuacaoCondicaoObrigatoriedadeService
    {
        #region [ DomainServices ]

        private PessoaAtuacaoCondicaoObrigatoriedadeDomainService PessoaAtuacaoCondicaoObrigatoriedadeDomainService
        {
            get { return Create<PessoaAtuacaoCondicaoObrigatoriedadeDomainService>(); }
        }

        #endregion [ DomainServices ]

        public PessoaAtuacaoCondicaoObrigatoriedadeData AlterarPessoaAtuacaoCondicaoObrigatoriedade(long seqPessoaAtuacao)
        {
            var ret = PessoaAtuacaoCondicaoObrigatoriedadeDomainService.BuscarPessoaAtuacaoCondicaoObrigatoriedade(seqPessoaAtuacao);
            return ret.Transform<PessoaAtuacaoCondicaoObrigatoriedadeData>();
        }

        public long SalvarPessoaAtuacaoCondicaoObrigatoriedade(PessoaAtuacaoCondicaoObrigatoriedadeData obj)
        {
            foreach (var item in obj.CondicoesObrigatoriedade)
            {
                PessoaAtuacaoCondicaoObrigatoriedadeDomainService.SaveEntity(SMCMapperHelper.Create<PessoaAtuacaoCondicaoObrigatoriedade>(item));
            }
            return obj.CondicoesObrigatoriedade.FirstOrDefault().Seq; //Não será utilizado de fato.
        }
    }
}