using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using SMC.SGA.Administrativo.Areas.DCT.Views.ColaboradorVinculoCurso.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoCursoCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

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

        [SMCCpf]
        [SMCValueEmpty("-")]
        public string Cpf { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroPassaporte { get; set; }

        public string CpfOuPassaporte => !string.IsNullOrEmpty(Cpf) ? SMCMask.ApplyMaskCPF(Cpf) : NumeroPassaporte;
        
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public bool InseridoPorCarga { get; set; }

        public string DescricaoTipoVinculoSelect { get; set; }

        public long SeqEntidadeVinculo { get; set; }

        [SMCConditionalDisplay(nameof(InseridoPorCarga), true)]
        [SMCDisplay]
        public string MensagemInformativa { get => UIResource.MensagemInformativa; }

        public string NomeEntidadeVinculo { get; set; }
    }
}
