using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ItemRelatorioDisciplinasCursadasEmentaVO : ISMCMappable
    {
        public long? SeqAluno { get; set; }

        public long? SeqHistoricoEscolar { get; set; }

		public string DescricaoComponenteCurricular { get; set; }

		public string DescricaoComponenteCurricularAssunto { get; set; }

		public DateTime? DataInicioEmenta { get; set; }

        public DateTime? DataFimEmenta { get; set; }

        public string Ementa { get; set; }
    }
}