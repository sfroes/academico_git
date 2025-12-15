using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoTurmaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public List<TurmaListarGrupoCursoVO> Vinculos { get; set; }
    }
}