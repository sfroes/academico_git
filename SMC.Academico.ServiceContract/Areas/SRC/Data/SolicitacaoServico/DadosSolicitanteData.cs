using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosSolicitanteData : ISMCMappable
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

        public List<EnderecoData> Enderecos { get; set; }

        public List<TelefoneData> Telefones { get; set; }

        public List<EnderecoEletronicoData> EnderecosEletronicos { get; set; }

        #endregion BI_PES_007 - Consulta dados de contato

        #region BI_PES_008 - Consulta dados pessoais

        public PessoaIdentificacaoData Identificacao { get; set; }

        #endregion BI_PES_008 - Consulta dados pessoais
    }
}