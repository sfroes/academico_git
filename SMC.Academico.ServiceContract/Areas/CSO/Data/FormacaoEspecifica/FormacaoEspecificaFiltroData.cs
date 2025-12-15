using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class FormacaoEspecificaFiltroData : ISMCMappable
    {
        public long[] SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public List<long> SeqsFormacoesEspecificasOrigem { get; set; }

        public long? SeqCursoOferta { get; set; }

        public bool? SelecaoNivelFolha { get; set; }

        public bool? SelecaoNivelSuperior { get; set; }

        public bool ConsiderarSometeTipoFomacaoEspecifica { get; set; }

        public List<long> SeqTipoFormacaoEspecifica { get; set; }
    }
}