using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaCabecalhoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }
                
        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        public string CicloLetivoInicio { get; set; }

        public string CicloLetivoFim { get; set; }

        public short Vagas { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public SituacaoTurma SituacaoTurmaAtual { get; set; }

        public List<PessoaDadosPessoais> ColaboradoresBanco { get; set; }

        public List<TurmaCabecalhoResponsavelVO> Colaboradores { get; set; }

        public List<TurmaCabecalhoConfiguracoesVO> TurmaConfiguracoesCabecalho { get; set; }

        public bool DiarioFechado { get; set; }

        public DateTime? InicioPeriodoLetivo { get; set; }

        public DateTime? FimPeriodoLetivo { get; set; }
    }
}
