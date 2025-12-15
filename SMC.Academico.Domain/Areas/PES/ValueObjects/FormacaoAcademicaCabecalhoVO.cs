using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class FormacaoAcademicaCabecalhoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }


    }
}
