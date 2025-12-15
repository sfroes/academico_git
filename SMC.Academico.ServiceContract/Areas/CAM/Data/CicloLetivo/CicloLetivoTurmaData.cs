using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoTurmaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public List<TurmaListarGrupoCursoData> Vinculos { get; set; }
    }
}