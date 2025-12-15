using System;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DeclaracaoGenericaListarVO : ISMCMappable
    {        
        public long Seq { get; set; }
        public long SeqPessoa { get; set; }
        public long? NumeroRegistroAcademico { get; set; }
        public int? CodigoAlunoMigracao { get; set; }
        public string NomeAluno { get; set; }
        public string NomeSocialAluno { get; set; }
        public string DescricaoCursoOfertaLocalidade { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string SituacaoAtual { get; set; }

    }
}
