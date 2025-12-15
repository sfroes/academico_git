using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizGrupoViewModel : SMCViewModelBase
    {
        public long SeqGrupoCurricular { get; set; }

        public long? SeqGrupoCurricularSuperior { get; set; }

        public string DescricaoGrupo { get; set; }

        public string ConfiguracaoGrupo { get; set; }

        public decimal? PercentualConclusaoGrupo { get; set; }

        public string PercentualConclusaoGrupoFormatado
        { 
            get 
            {
                if (this.PercentualConclusaoGrupo.HasValue)
                    return this.PercentualConclusaoGrupo.ToString() + "%";
                else
                    return "-";
            }
        }

        public List<IntegralizacaoMatrizGrupoViewModel> GruposFilhos { get; set; }

        public List<IntegralizacaoMatrizConfiguracaoViewModel> Configuracoes { get; set; }

        public List<IntegralizacaoMatrizGrupoBeneficioViewModel> Beneficios { get; set; }

        public List<IntegralizacaoMatrizGrupoCondicaoViewModel> CondicoesObrigatorias { get; set; }

        public List<IntegralizacaoMatrizGrupoFormacaoViewModel> FormacoesEspecificas { get; set; }

        public List<string> GruposDivisoes { get; set; }
        
        public string MensagemDispensaGrupo { get; set; }

        public List<long?> SeqSolicitacaoServico { get; set; }

        public List<string> ProtocoloDispensaGrupo { get; set; }

    }
}
