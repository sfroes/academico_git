using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System;

namespace SMC.SGA.Administrativo.Models
{
    //Utilizado para exibir dados em partial view 
    public class EnderecoEletronicoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        public long Seq { get; set; }

        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

        public string Descricao { get; set; }

        public string DescricaoFormatada
        {
            get
            {
                return $"{Enum.GetName(typeof(TipoEnderecoEletronico), TipoEnderecoEletronico)}: {Descricao}";
            }
        }
    }
}