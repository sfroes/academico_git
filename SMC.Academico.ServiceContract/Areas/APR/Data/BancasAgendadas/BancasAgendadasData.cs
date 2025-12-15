using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class BancasAgendadasData : ISMCMappable
    {
        public long SeqAplicacaoAvaliacao { get; set; }
        public long SeqEntidadeCurso { get; set; }
        public string NomeCursoLocalidade { get; set; }
        public string DescricaoTurno { get; set; }
        public string NomeAluno { get; set; }
        public string DescricaoTipoTrabalho { get; set; }
        public long SeqTipoEvento { get; set; }
        public string DescricaoTipoEvento { get; set; }
        public string DescricaoLocal { get; set; }
        public DateTime DataInicioAplicacaoAvaliacao { get; set; }
        public string DescricaoTitulo { get; set; }
        public string DescricaoEscalaApuracaoItem { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string DescricaoCancelamento { get; set; }
        public decimal? Nota { get; set; }
        public string NomeEntidadeResponsavel { get; set; }
        public string DescricaoComponenteCurricular { get; set; }
        public string PotencialPropriedadeIntelectual { get; set; }
        public OrdenacaoBancasAgendadasRelatorio OrdenacaoBancasAgendadasRelatorio { get; set; }

        #region Navigation Properties

        public IList<BancasAgendadasAreaConhecimentoData> AreasConhecimento { get; set; }
        public IList<MembroBancaExaminadoraData> MembrosBancaExaminadora { get; set; }

        #endregion Navigation Properties
    }
}