using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class AvaliacaoTrabalhoAcademicoBancaExaminadoraData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Sigla { get; set; }

        public long? SeqAvaliacao { get; set; }

        public long? SeqTrabalhoAcademico { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqCalendario { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        public string Local { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public string MotivoCancelamento { get; set; }

        public string NotaConceito { get; set; }

        public List<TrabalhoAcademicoMembroBancaData> MembrosBancaExaminadora { get; set; }

        public List<SMCDatasourceItem> TiposEvento { get; set; }

		public DateTime? DataDepositoSecretaria { get; set; }

        public bool AlunoFormado { get; set; }
    }
}
