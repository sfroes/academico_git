using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NumeroProtocolo { get; set; }

        public DateTime DataSolicitacao { get; set; }

        [SMCMapProperty("DescricaoOriginal")]
        public string Descricao { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }

        public DateTime? DataPrevistaSolucao { get; set; }

        public DateTime? DataSolucao { get; set; }

        public long? SeqJustificativaSolicitacaoServico { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public int? SeqCondicaoPagamentoGra { get; set; }

        public string CodigoAdesao { get; set; }

        public DateTime? DataAdesao { get; set; }

        public List<SolicitacaoMatriculaItemVO> Itens { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public IEnumerable<SolicitacaoMatriculaItemDivisoesVO> SeqsDivisaoTurma { get; set; }

        public List<SolicitacaoDocumentoRequeridoVO> Documentos { get; set; }

        public TermoAdesaoSolicitacaoMatriculaVO TermoAdesaoVO { get; set; }

        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }
    }
}