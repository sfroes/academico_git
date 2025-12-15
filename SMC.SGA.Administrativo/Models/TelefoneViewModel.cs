using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System;

namespace SMC.SGA.Administrativo.Models
{
    public class TelefoneViewModel : SMCViewModelBase, ISMCMappable
    {
        public long Seq { get; set; }

        public int CodigoPais { get; set; }

        public int CodigoArea { get; set; }

        public string Numero { get; set; }

        public TipoTelefone TipoTelefone { get; set; }

        public string NomeContato { get; set; }

        public bool? Preferencial { get; set; }

        public string TelefoneFormatado
        {
            get
            {
                return $"{Enum.GetName(typeof(TipoTelefone), TipoTelefone)}: ({CodigoPais})({CodigoArea}) {Numero}";
            }
        }
    }
}