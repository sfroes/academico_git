using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IngressoVO : ISMCMappable
    {
        public DateTimeOffset Data { get; set; }
        public List<string> FormasAcesso { get; set; } // enum  Enem, Vestibular, Avaliação Seriada, Seleção Simplificada, Egresso BI/LI, PEC-G, Transferência Ex Officio, Decisão judicial, Seleção para Vagas Remanescentes, Seleção para Vagas de Programas Especiais, Prova agendada, Entrevista, Transferencia, Outros, Programas de avaliação seriada ou continuada, Convenios, Histórico escolar, Sisu
        public string AnoMesProcessoSeletivo { get; set; } // Ano e mês da realização do processo seletivo no formato AAAA-MM

        [Obsolete("OBSOLETO a partir da v1.04.1.")]
        public DateTimeOffset? DataConclusao { get; set; }

        public string FormaAcesso { get; set; } //enum Enem, Vestibular, Avaliação Seriada, Seleção Simplificada, Egresso BI/LI, PEC-G, Transferência Ex Officio, Decisão judicial, Seleção para Vagas Remanescentes, Seleção para Vagas de Programas Especiais, Prova agendada, Entrevista, Transferencia, Outros, Programas de avaliação seriada ou continuada, Convenios, Histórico escolar, Sisu
    }
}
