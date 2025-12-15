using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class ConsultaPublicaData : ISMCMappable
    {
        public long Seq { get; set; }

        public bool Valido { get; set; }

        public string SituacaoDiploma { get; set; }

        public ClasseSituacaoDocumento ClasseSituacaoDocumento { get; set; }

        public string Nome { get; set; }

        public string NomeCivil { get; set; }

        public string Cpf { get; set; }

        public int? CodigoCursoEMEC { get; set; }

        public string NomeCurso { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTitulacao { get; set; }

        public string DescricaoTitulacaoXSD { get; set; }

        public DateTime DataIngresso { get; set; }

        public DateTime? DataConclusao { get; set; }

        public string NumeroProcesso { get; set; }

        public string NumeroRegistro { get; set; }

        public DateTime? DataRegistro { get; set; }

        public DateTime? DataRegistroDOU { get; set; }

        public int CodigoMEC { get; set; }

        public string NomeInstituicaoEnsino { get; set; }

        public string Mantenedora { get; set; }

        public string MensagemInformativaDiploma { get; set; }

        public bool HistoricoInvalido { get; set; }

        public List<ConsultaPublicaHistoricoData> Historicos { get; set; }
    }
}
