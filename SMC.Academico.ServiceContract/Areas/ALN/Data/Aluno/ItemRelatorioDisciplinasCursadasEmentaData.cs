using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ItemRelatorioDisciplinasCursadasEmentaData : ISMCMappable
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