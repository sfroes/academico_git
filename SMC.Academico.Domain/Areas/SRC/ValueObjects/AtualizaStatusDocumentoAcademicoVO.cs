using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class AtualizaStatusDocumentoAcademicoVO
    {
        public string UsuarioInclusao { get; set; }
        public long SeqDocumentoAcademico { get; set; }
        public string TipoCancelamento { get; set; }
        public string MotivoCancelamento { get; set; } // ErroDeFato, ErroDeDireito, DecisaoJudicial, ReemissaoParaComplementoDeInformacao, ReemissaoParaInclusaoDeHabilitacao, ReemissaoParaAnotacaoDeRegistro 
        public string Observacao { get; set; }
    }
}
