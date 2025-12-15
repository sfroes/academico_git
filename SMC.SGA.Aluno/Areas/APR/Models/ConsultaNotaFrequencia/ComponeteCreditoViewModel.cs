using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.SGA.Aluno.Areas.APR.Models.ConsultaNotaFrequencia
{
    public class ComponeteCreditoViewModel : ISMCMappable
    {
        //Sequencial do aluno
        public long Seq { get; set; }

        public TipoConclusao TipoConclusao { get; set; }

        public string SemAnoCicloLetivo { get; set; }

        public string DescricaoComponente { get; set; }

        public string DescricaoComponenteAssunto { get; set; }

        public string ListaProfessores { get; set; }

        public string Nota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public short CargaHoraria { get; set; }

        public short Credito { get; set; }

        public short? Faltas { get; set; }

        public string PercentualFrequencia { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public DateTime? DataExame { get; set; }

        public string DescricaoComponenteAssuntoFormatadaAproveitamento { get => $"{this.DescricaoComponente.ToUpper()}{(!string.IsNullOrEmpty(this.DescricaoComponenteAssunto) ? ": " + this.DescricaoComponenteAssunto : string.Empty)}<br / ><br / > {this.ListaProfessores}"; }

        public string DescricaoComponenteAssuntoFormatadaConcluido { get =>      $"{this.DescricaoComponente.ToUpper()}{(!string.IsNullOrEmpty(this.DescricaoComponenteAssunto) ? ": " + this.DescricaoComponenteAssunto : string.Empty)}<br / ><br / > {this.ListaProfessores}"; }

        public string DescricaoComponenteFormatada { get => $"{this.DescricaoComponente.ToUpper()}"; }

        public bool PermitirCredito { get; set; }
    }
}