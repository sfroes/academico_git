using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData : ISMCMappable
    {
        public bool EmissaoDiplomaDigital1Via { get; set; }

        public string DescricaoSituacaoAtualMatricula { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string DescricaoTipoHistorico { get; set; }

        public DateTime? DataIngresso { get; set; }

        public DateTime? DataConclusao { get; set; }

        public DateTime? DataColacao { get; set; }

        public double CargaHorariaCurso { get; set; }

        public double CargaHorariaCursoIntegralizada { get; set; }

        public string CodigoCurriculo { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public int? NumeroVia { get; set; }

        public string MensagemInformativa { get; set; }

        public int? CodigoCursoOfertaLocalidade { get; set; }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeData> Enade { get; set; }
    }
}
