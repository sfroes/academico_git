using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorLookupData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public Sexo? Sexo { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }
    }
}