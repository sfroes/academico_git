using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoListaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public List<CurriculoCursoOfertaListaData> CursosOferta { get; set; }
    }
}