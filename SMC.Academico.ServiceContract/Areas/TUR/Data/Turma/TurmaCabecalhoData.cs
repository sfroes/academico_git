using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaCabecalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string CodigoFormatado { get; set; }

        public string CicloLetivoInicio { get; set; }

        public string CicloLetivoFim { get; set; }

        public short Vagas { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public SituacaoTurma SituacaoTurmaAtual { get; set; }

        public List<TurmaCabecalhoResponsavelData> Colaboradores { get; set; }

        public List<TurmaCabecalhoConfiguracoesData> TurmaConfiguracoesCabecalho { get; set; }

        public bool DiarioFechado { get; set; }

        public DateTime? InicioPeriodoLetivo { get; set; }
        
        public DateTime? FimPeriodoLetivo { get; set; }

    }
}
