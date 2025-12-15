using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteProcessoSeletivoListaVO
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqIngressante { get; set; }

        public string NomeInstituicaoEnsino { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string DescricaoOferta
        {
            get
            {
                if (DescricoesOfertas != null)
                    return string.Join(", ", DescricoesOfertas?.ToArray());
                else
                    return string.Empty;
            }
            set
            {
            }
        }

        public List<string> DescricoesOfertas { get; internal set; }
    }
}