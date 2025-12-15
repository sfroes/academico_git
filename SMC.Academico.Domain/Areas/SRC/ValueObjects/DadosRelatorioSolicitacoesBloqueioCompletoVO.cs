using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosRelatorioSolicitacoesBloqueioCompletoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string NumeroProtocolo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Solicitante { get; set; }

        public long SeqProcesso { get; set; }

        public string Processo { get; set; }

        public DateTime DataInicioProcesso { get; set; }

        public long SeqEtapaSGF { get; set; }

        public short OrdemEtapa { get; set; }

        public CategoriaSituacao CategoriaSituacao { get; set; }

        public long SeqSituacaoEtapaSGF { get; set; }

        public long SeqTemplateProcessoSGF { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public string TipoBloqueio { get; set; }

        public string MotivoBloqueio { get; set; }

        public string Referente { get; set; }

        public DateTime DataBloqueio { get; set; }

        public bool ImpedeInicioEtapa { get; set; }

        public bool ImpedeFimEtapa { get; set; }
    }
}