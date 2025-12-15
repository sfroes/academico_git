using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ReferenciaFamiliarListarDynamicModel : SMCDynamicViewModel
    {
        public override long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        [SMCDescription]
        public string NomeParente { get; set; }

        public TipoParentesco TipoParentesco { get; set; }

        public List<EnderecoEletronicoListarViewModel> EnderecosEletronicos { get; set; }

        public List<EnderecoViewModel> Enderecos { get; set; }

        public List<TelefoneViewModel> Telefones { get; set; }
    }
}