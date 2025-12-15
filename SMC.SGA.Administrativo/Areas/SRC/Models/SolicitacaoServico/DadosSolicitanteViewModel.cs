using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DadosSolicitanteViewModel : SMCViewModelBase
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

        public List<EnderecoViewModel> Enderecos { get; set; }

        public List<TelefoneViewModel> Telefones { get; set; }

        public List<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }

        #endregion BI_PES_007 - Consulta dados de contato

        #region BI_PES_008 - Consulta dados pessoais

        public PessoaIdentificacaoViewModel Identificacao { get; set; }

        #endregion BI_PES_008 - Consulta dados pessoais
    }
}