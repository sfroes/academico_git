using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorAptoComponenteData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAtuacaoColaborador { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string NomeColaborador { get; set; }

        public string DescricaoComponenteCurricular { get; set; }
    }
}