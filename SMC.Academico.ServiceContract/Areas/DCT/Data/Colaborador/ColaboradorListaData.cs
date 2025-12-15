using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorListaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("DadosPessoais.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("DadosPessoais.NomeSocial")]
        public string NomeSocial { get; set; }

        [SMCMapProperty("DadosPessoais.Sexo")]
        public Sexo Sexo { get; set; }

        [SMCMapProperty("Pessoa.DataNascimento")]
        public DateTime DataNascimento { get; set; }

        [SMCMapProperty("Pessoa.Cpf")]
        public string Cpf { get; set; }

        [SMCMapProperty("Pessoa.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        [SMCMapProperty("Pessoa.Falecido")]
        public bool Falecido { get; set; }

        public string FormacaoAcademica { get; set; }


        [SMCMapProperty("Vinculos")]
        public List<ColaboradorVinculoListaData> VinculosAtivos { get; set; }
    }
}