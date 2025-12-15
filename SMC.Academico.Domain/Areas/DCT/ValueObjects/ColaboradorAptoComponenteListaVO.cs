using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorAptoComponenteListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAtuacaoColaborador { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoComponenteCurricular { get; set; }
    }
}