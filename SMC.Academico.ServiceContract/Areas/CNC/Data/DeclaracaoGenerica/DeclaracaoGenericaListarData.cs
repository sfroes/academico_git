using System;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.DeclaracaoGenerica
{
    public class DeclaracaoGenericaListarData : ISMCMappable
    {
        public long Seq { get; set; }
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
