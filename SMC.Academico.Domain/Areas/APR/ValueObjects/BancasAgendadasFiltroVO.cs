using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class BancasAgendadasFiltroVO : ISMCMappable
    {
        public List<long> SeqEntidadesResponsaveis { get; set; }
        public List<long> SeqNiveisEnsino { get; set; }
        public long? SeqTipoEvento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public SituacaoBanca? SituacaoBanca { get; set; }
        public OrdenacaoBancasAgendadasRelatorio Ordenacao { get; set; }
        public bool? ExibirBancasComNota { get; set; }

        public long? SeqColaborador { get; set; }

        public TipoMembroBanca TipoMembroBanca { get; set; }
    }
}