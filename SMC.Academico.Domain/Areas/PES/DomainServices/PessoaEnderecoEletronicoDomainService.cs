using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaEnderecoEletronicoDomainService : AcademicoContextDomain<PessoaEnderecoEletronico>
    {
        public long SalvarEnderecoEletronicoPessoa(PessoaEnderecoEletronico enderecoEletronico)
        {
            if (enderecoEletronico.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
            {
                if (!SMCValidationHelper.ValidateEmail(enderecoEletronico.EnderecoEletronico.Descricao))
                {
                    throw new PessoaEnderecoEletronicoEmailException();
                }

                //Caso o endereco eletrônico termine com @sga.pucminas.br
                if (enderecoEletronico.EnderecoEletronico.Descricao.Trim().ToLower().EndsWith(TERMINACAO_EMAIL.PUCMINAS))
                    throw new PessoaEnderecoEletronicoTerminacaoEmailPucMinasException();
            }

            if (enderecoEletronico.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Website)
            {
                if (!SMCValidationHelper.ValidateUrl(enderecoEletronico.EnderecoEletronico.Descricao))
                {
                    throw new PessoaEnderecoEletronicoUrlException();
                }
            }

            this.SaveEntity(enderecoEletronico);
            return enderecoEletronico.Seq;
        }
    }
}