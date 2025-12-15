using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFiltroVO : ISMCMappable
    {
        public long? SeqFormacaoEspecifica { get; set; }
        public List<long> SeqsCursos { get; set; }
        public List<CategoriaAtividade> CategoriasAtividadesSituacoesAtuais { get; set; }

    }
}