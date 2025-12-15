using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class FormacaoEspecificaVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqInstituicaoEnsino { get; set; }
        public long? SeqFormacaoEspecificaSuperior { get; set; }
        public long SeqTipoFormacaoEspecifica { get; set; }
        public string Descricao { get; set; }
        public long? SeqEntidadeResponsavel { get; set; }
        public long? SeqGrauAcademico { get; set; }
        public long? SeqCursoOferta { get; set; }
        public bool? Ativo { get; set; }
        public TipoFormacaoEspecificaVO TipoFormacaoEspecifica { get; set; }

    }
}