using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorVinculoIdentidadeAcademicaVO : ISMCMappable
    {
        public DateTime? DataValidade { get; set; }

        public long SeqPrograma { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }
    }
}