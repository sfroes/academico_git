using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.Util;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCDescription]
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

        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCCpf]
        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public List<PessoaAtuacaoBloqueioDetalheDynamicModel> Bloqueios { get; set; }

        /// <summary>
        /// Valor do Cpf ou Passaporte
        /// </summary>
        public string CpfOuPassaporte { get => string.IsNullOrEmpty(Cpf) ? NumeroPassaporte : SMCMask.ApplyMaskCPF(Cpf); }

        /// <summary>
        /// Valida se é aluno ou ingressante
        /// </summary>
        public bool EhAluno => this.TipoAtuacao == TipoAtuacao.Aluno; 
    }
}