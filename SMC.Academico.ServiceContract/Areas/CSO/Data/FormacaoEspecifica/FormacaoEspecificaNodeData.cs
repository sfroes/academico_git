using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class FormacaoEspecificaNodeData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqTipoEntidade { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public long? SeqFormacaoEspecificaSuperior { get; set; }

        public bool TipoFormacaoEspecificaFolha { get; set; }

        public string Descricao { get; set; }

        public bool? Ativo { get; set; }

        public long SeqCurso { get; set; }

        [SMCMapProperty("TipoFormacaoEspecifica.Descricao")]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        [SMCMapProperty("TipoFormacaoEspecifica.ExibeGrauDescricaoFormacao")]
        public bool ExibeGrauDescricaoFormacao { get; set; }

        [SMCMapProperty("GrauAcademico.Descricao")]
        public string DescricaoGrauAcademico { get; set; }

        public bool Selecionavel { get; set; }

        public bool PossuiCursoAssociado { get; set; }
    }
}