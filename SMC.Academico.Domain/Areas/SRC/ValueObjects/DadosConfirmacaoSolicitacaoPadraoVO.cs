using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosConfirmacaoSolicitacaoPadraoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }
        public string DescricaoOriginal { get; set; }
    }
}
