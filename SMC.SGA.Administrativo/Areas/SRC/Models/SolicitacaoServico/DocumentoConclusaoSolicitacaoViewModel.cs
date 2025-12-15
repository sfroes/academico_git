using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DocumentoConclusaoSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCValueEmpty("-")]
        public long SeqDocumentoAcademico { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoTipoDocumentoAcademico { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoCurso { get; set; }

        [SMCValueEmpty("-")]
        public List<string> DescricoesFormacaoEspecifica { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoGrauAcademico { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoTitulacao { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoSituacaoDocumentoAcademicoAtual { get; set; }

        [SMCHidden]
        public short NumeroViaDocumento { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroViaDocumentoFormatado
        {
            get
            {
                if (NumeroViaDocumento == 0)
                    return $"-";

                return $"{NumeroViaDocumento}°";
            }
        }

        [SMCHidden]
        [SMCValueEmpty("-")]
        public long? SeqDocumentoGAD { get; set; }

        [SMCHidden]
        [SMCValueEmpty("-")]
        public string UrlDocumentoGAD { get; set; }

        [SMCHidden]
        public string TokenTipoDocumentoAcademico { get; set; }
    }
}