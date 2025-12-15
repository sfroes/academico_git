using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class ComponentesCreditosData : ISMCMappable
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

        public short? CargaHorariaGrade { get; set; }

        public short? CargaHorariaExecutada { get; set; }

        public short Credito { get; set; }

        public short? Faltas { get; set; }

        public string PercentualFrequencia { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public string DescricaoSituacaoHistoricoEscolar { get { return SituacaoHistoricoEscolar.HasValue ? SMCEnumHelper.GetDescription(SituacaoHistoricoEscolar) : null; } }

        public DateTime? DataExame { get; set; }
        
        public string Observacao { get; set; }

        public bool PermitirCredito { get; set; }

        public TipoArredondamento? TipoArredondamento { get; set; }
    }
}