using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ReplicaFormacaoEspecificaProgramaVO : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }
        public long SeqEntidadeResponsavel { get; set; }
        public List<CategoriaAtividade> CategoriasAtividadesSituacoesAtuais { get; set; }
        public List<long> SeqsCursos { get; set; }
        public DateTime DataInicioVigenciaFormacaoCurso { get; set; }
        public DateTime? DataFimVigenciaFormacaoCurso { get; set; }
        public List<long> SeqsCursosOfertasLocalidades { get; set; }
        public List<ReplicaFormacaoEspecificaProgramaTitulacaoCursoVO> CursosTitulacoes { get; set; }
    }
}