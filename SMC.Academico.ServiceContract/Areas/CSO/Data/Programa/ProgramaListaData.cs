using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ProgramaListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public long SeqTipoEntidade { get; set; }

        public long SeqHierarquiaEntidadeItem { get; set; }

        public string DescricaoSituacaoAtual { get; set; }

        public TipoPrograma TipoPrograma { get; set; }

        public List<CursoListaData> Cursos { get; set; }
    }
}