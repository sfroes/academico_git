using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class FormacaoEspecificaFiltroVO : ISMCMappable
    {
        public long? SeqCurso { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long[] SeqsEntidadesResponsaveis { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqCursoOferta { get; set; }

        public bool? SelecaoNivelFolha { get; set; }

        public bool? SelecaoNivelSuperior { get; set; }

        public bool ConsiderarSometeTipoFomacaoEspecifica { get; set; }

        public List<long> SeqTipoFormacaoEspecifica { get; set; }

        public long? SeqTipoAreaTematicaExcessao { get; set; }
    }
}