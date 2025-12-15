using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorListaVO: ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public Sexo Sexo { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public bool Falecido { get; set; }

        public string FormacaoAcademica { get; set; }

        public List<ColaboradorVinculoListaVO> VinculosAtivos { get; set; }
    }
}