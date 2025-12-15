using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class HistoricoEscolarListaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqAluno { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public string DescricaoAlunoHistorico { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string CodigoComponenteCurricular { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public string DescricaoAssunto { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Creditos { get; set; }

        public ObrigatorioOptativo ObrigatorioOptativo { get; set; }

        public decimal? Nota { get; set; }

        public string DescricaoConceito { get; set; }

        public short? Faltas { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }

        public string DescricaoSituacaoFinal { get; set; }

        public List<string> Colaboradores { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public long? SeqSolicitacaoDispensa { get; set; }

        public string ProtocoloSolicitacaoDispensa { get; set; }

        public DateTime? DataSolicitacaoDispensa { get; set; }

        public List<TipoGestaoDivisaoComponente> TiposGestaoDivisaoComponente { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string SiglaComponente { get; set; }

        public string Observacao { get; set; }

        public bool SomenteLeitura { get; set; }

    }
}