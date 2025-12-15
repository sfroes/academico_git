using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosSolicitanteVO : ISMCMappable
    {
        public TipoAtuacao TipoAtuacao { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string NomeCompleto
        {
            get
            {
                var nomeCompleto = string.Empty;

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{NomeSocial} ({Nome})";
                    else
                        nomeCompleto += $"{NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{Nome}";
                }
                return nomeCompleto;
            }
        }

        #region BI_PES_007 - Consulta dados de contato

        public List<EnderecoVO> Enderecos { get; set; }

        public List<TelefoneVO> Telefones { get; set; }

        public List<EnderecoEletronicoVO> EnderecosEletronicos { get; set; }

        #endregion BI_PES_007 - Consulta dados de contato

        #region BI_PES_008 - Consulta dados pessoais

        public PessoaIdentificacaoVO Identificacao { get; set; }

        #endregion BI_PES_008 - Consulta dados pessoais
    }
}