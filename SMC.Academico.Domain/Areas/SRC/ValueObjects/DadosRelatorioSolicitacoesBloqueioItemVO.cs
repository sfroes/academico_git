using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosRelatorioSolicitacoesBloqueioItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string NumeroProtocolo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Solicitante { get; set; }

        public string EtapaAtual { get; set; }

        public string SituacaoAtual { get; set; }

        public string EtapaSituacaoAtual { get; set; }

        public string TipoBloqueio { get; set; }

        public string MotivoBloqueio { get; set; }

        public string TipoMotivoBloqueio { get; set; }

        public string Referente { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string DataBloqueioFormatada { get; set; }

        public bool ImpedeInicioEtapa { get; set; }

        public bool ImpedeFimEtapa { get; set; }

        public string Impede
        {
            get
            {
                if (ImpedeInicioEtapa && ImpedeFimEtapa)
                {
                    return "Início e fim da etapa";
                }
                else if (ImpedeInicioEtapa)
                {
                    return "Início da etapa";
                }
                else if (ImpedeFimEtapa)
                {
                    return "Fim da etapa";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}