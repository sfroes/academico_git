using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.ValueObjects
{
    /// <summary>
    /// Classe usada para fazer a projeção no banco
    /// </summary>
    public class EtapaProjecaoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public string DescricaoEtapa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long SeqEtapaSGF { get; set; }

        public bool Ativo { get; set; }

        public bool ExibeItemAposTerminoEtapa { get; set; }

        public bool ExibeItemMatriculaSolicitante { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public long? SeqEscalonamento { get; set; }

        public string Instrucoes { get; set; }

        public IEnumerable<SolicitacaoHistoricoSituacao> HistoricosSituacao { get; set; }

        public SituacaoEtapa? SituacaoEtapa { get; set; }

        public SolicitacaoHistoricoSituacao UltimaSituacaoEtapaSGF { get; set; }

        public SituacaoIngressante? UltimaSituacaoIngressante { get; set; }

        public IEnumerable<ServicoSituacaoIngressante> SituacoesPermitidasServicoIngressante { get; set; }

        public IEnumerable<ServicoSituacaoAluno> SituacoesPermitidasServicoAluno { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public long SeqTemplateProcessoSgf { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public List<long> SeqsMotivosBloqueios { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long? SeqCicloLetivoProcesso { get; set; }
    }
}