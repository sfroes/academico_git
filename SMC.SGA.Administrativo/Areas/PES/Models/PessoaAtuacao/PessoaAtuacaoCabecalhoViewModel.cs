using SMC.Academico.Common.Areas.PES.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid4_24)]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string Nome { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string NomeSocial { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
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
        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string Cpf { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string NumeroPassaporte { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public Sexo Sexo { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataNascimento { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public bool Falecido { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoTipoDocumento { get; set; }

        [SMCHidden]
        public string CpfOuPassaporte => !string.IsNullOrEmpty(Cpf) ? SMCMask.ApplyMaskCPF(Cpf) : NumeroPassaporte;

        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCValueEmpty("-")]
        public long? CodigoMigracaoAluno { get; set; }

        public long? RA { get; set; }

        public long? SeqIngressante { get; set; }
    }
}