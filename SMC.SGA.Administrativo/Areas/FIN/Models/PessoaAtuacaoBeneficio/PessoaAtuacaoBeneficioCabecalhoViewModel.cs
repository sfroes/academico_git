using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioCabecalhoViewModel : SMCViewModelBase
    {
        [SMCIgnoreProp]
        public string Nome { get; set; }

        [SMCIgnoreProp]
        public string NomeSocial { get; set; }

        [SMCIgnoreProp]
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

        [SMCCpf]
        [SMCIgnoreProp]
        public string Cpf { get; set; }

        [SMCIgnoreProp]
        public string Passaporte { get; set; }

        [SMCIgnoreProp]
        public bool Falecido { get; set; }

        [SMCIgnoreProp]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCValueEmpty("-")]
        public long? CodigoMigracaoAluno { get; set; }

        public long RA { get; set; }

        public long SeqIngressante { get; set; }

        [SMCValueEmpty("-")]
        public string DadosVinculo { get; set; }

        [SMCValueEmpty("-")]
        public string CondicaoPagamento { get; set; }

        #region Propriedades Tratadas para view

        public string NomeSocialTratado
        {
            get
            {
                if (string.IsNullOrEmpty(this.NomeSocial))
                {
                    return " - ";
                }
                else
                {
                    return this.NomeSocial;
                }
            }
        }

        [SMCCpf]
        public string CpfTratado
        {
            get
            {
                if (string.IsNullOrEmpty(this.Cpf))
                {
                    return " - ";
                }
                else
                {
                    return this.Cpf;
                }
            }
        }

        public string PassaporteTratado
        {
            get
            {
                if (string.IsNullOrEmpty(this.Passaporte))
                {
                    return " - ";
                }
                else
                {
                    return this.Passaporte;
                }
            }
        }

        #endregion Propriedades Tratadas para view
    }
}