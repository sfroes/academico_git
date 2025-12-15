using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CurriculoListaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public bool Ativo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public IEnumerable<CurriculoCursoOfertaListaVO> CursosOferta { get; set; }
    }
}