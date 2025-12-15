using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoServicoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string NumeroProtocolo { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqServico { get; set; }

        public List<long> SeqsServicos { get; set; }

        public long? SeqProcesso { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public bool? DisponivelParaAtendimento { get; set; }

        public bool? PossuiBloqueio { get; set; }

        public long? SeqUsuarioResponsavel { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqSituacaoEtapa { get; set; }

        public TipoFiltroCentralSolicitacao TipoFiltroCentralSolicitacao { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public long? SeqBloqueio { get; set; }

        public DateTime? DataInclusaoInicio { get; set; }

        public DateTime? DataInclusaoFim { get; set; }

        public CategoriaSituacao? CategoriaSituacao { get; set; }

        public DateTime? DataSolicitacao { get; set; }

        public string Solicitante { get; set; }

        public string CPF { get; set; }

        public string Passaporte { get; set; }

        public long? SituacaoAtual { get; set; }

        public string DescricaoLookupSolicitacao { get; set; }

        public string DescricaoTipoVinculoAluno { get; set; }
        public long? SeqTipoServico { get; set; }

    }
}