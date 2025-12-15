using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoComponenteVO : ISMCMappable, ISMCSeq
    {
		public bool Artigo { get; set; }

		public long Seq { get; set; }
        
        public short Numero { get; set; }

        public string DescricaoTipoComponente { get; set; }

        public short? CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public string Formato { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public bool PermiteGrupo { get; set; }

        public string DescricaoComponenteCurricularOrganizacao { get; set; }

    }
}
