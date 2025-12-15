using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class FichaCatalograficaData : ISMCMappable
    {
        public string NomeAlunoABNT { get; set; }
        public string Cutter { get; set; }
        public string TituloTrabalho { get; set; }
        public string NomeAluno { get; set; }
        public string Cidade { get; set; }
        public string AnoDefesa { get; set; }
        public short? NumeroPaginas { get; set; }
        public string Orientador { get; set; }
        public string Coorientador { get; set; }
        public string TipoTrabalho { get; set; }
        public string NivelEnsino { get; set; }
        public string InstituicaoEnsino { get; set; }
        public string GrupoPrograma { get; set; }
        public string Assuntos { get; set; }
        public string NomeOrientadorABNT { get; set; }
        public string NumeroCdu { get; set; }
        public string PossuiIlustracao { get; set; }

        // Rodapé
        public string NomeBibliotecario { get; set; }
        public string NumeroCrbBibliotecario { get; set; }
    }
}