using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoReplicaFormacaoEspecificaProgramaFiltroData : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }
        public long SeqEntidadeResponsavel{ get; set; }
        public List<CategoriaAtividade> CategoriasAtividadesSituacoesAtuais { get; set; }
        
    }
}