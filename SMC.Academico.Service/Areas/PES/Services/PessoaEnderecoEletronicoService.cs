using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaEnderecoEletronicoService : SMCServiceBase, IPessoaEnderecoEletronicoService
    {
        #region [ Services ]

        private PessoaEnderecoEletronicoDomainService PessoaEnderecoEletronicoDomainService
        {
            get { return this.Create<PessoaEnderecoEletronicoDomainService>(); }
        }

        #endregion [ Services ]

        public SMCPagerData<PessoaEnderecoEletronicoLookupData> BuscarPessoaEnderecoEletronicosLookup(PessoaEnderecoEletronicoFiltroLookupData filtro)
        {
            int total;

            var enderecosEletronicos = this.PessoaEnderecoEletronicoDomainService
                .SearchBySpecification(filtro.Transform<PessoaEnderecoEletronicoFilterSpecification>().SetOrderByDescending(s => s.DataInclusao), out total, IncludesPessoaEnderecoEletronico.EnderecoEletronico)
                .TransformList<PessoaEnderecoEletronicoLookupData>();

            enderecosEletronicos.ForEach(e =>
            {
                e.BloquearEdicao = e.Descricao.Trim().ToLower().EndsWith(TERMINACAO_EMAIL.PUCMINAS) && e.TipoEnderecoEletronico == TipoEnderecoEletronico.Email;
            });

            return new SMCPagerData<PessoaEnderecoEletronicoLookupData>(enderecosEletronicos, total);
        }

        public PessoaEnderecoEletronicoData BuscarPessoaEnderecoEletronico(long seq)
        {
            return this.PessoaEnderecoEletronicoDomainService
                .SearchByKey(new SMCSeqSpecification<PessoaEnderecoEletronico>(seq), IncludesPessoaEnderecoEletronico.EnderecoEletronico)
                .Transform<PessoaEnderecoEletronicoData>();
        }

        public long SalvarEnderecoEletronicoPessoa(PessoaEnderecoEletronicoData enderecoEletronicoData)
        {
            //var enderecoEletronico = enderecoEletronicoData.Transform<PessoaEnderecoEletronico>();
            //this.PessoaEnderecoEletronicoDomainService.SaveEntity(enderecoEletronico);
            //return enderecoEletronico.Seq;

            return PessoaEnderecoEletronicoDomainService.SalvarEnderecoEletronicoPessoa(enderecoEletronicoData.Transform<PessoaEnderecoEletronico>());
        }
    }
}