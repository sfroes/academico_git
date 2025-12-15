using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteVO : InformacoesPessoaVO
    {
        public long SeqCampanhaCicloLetivo { get; set; }

        public long SeqFormaIngresso { get; set; }

        public long? SeqInstituicaoTransferenciaExterna { get; set; }

        public string CursoTransferenciaExterna { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqInstituicaoNivelEnsino { get; set; }

        public long SeqProcessoSeletivo { get; set; }

        [SMCMapProperty("ProcessoSeletivo.Descricao")]
        public string DescricaoProcessoSeletivo { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public long? SeqConvocado { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public long? SeqOrientador { get; set; }

        public bool Ativo { get; set; }

        public DateTime? DataAdmissao { get; set; }

        public DateTime? DataTermino { get; set; }

        public SituacaoIngressante SituacaoIngressante { get; set; }

        public OrigemIngressante OrigemIngressante { get; set; }

        public List<PessoaAtuacaoEnderecoVO> Enderecos { get; set; }

        public List<IngressanteOfertaVO> Ofertas { get; set; }

        public List<IngressanteFormacaoEspecificaVO> FormacoesEspecificas { get; set; }

        public List<PessoaAtuacaoTermoIntercambioVO> TermosIntercambio { get; set; }

        public bool? GrupoEscalonamentoAtivo { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public List<IngressanteDocumentoVO> Documentos { get; set; }

        public List<IngressanteHistoricoSituacaoVO> HistoricosSituacao { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        [SMCMapProperty("OrientacoesPessoaAtuacao.Orientacao.SeqTipoOrientacao")]
        public long? SeqTipoOrientacao { get; set; }

        public List<IngressanteOrientacaoVO> OrientacaoParticipacoesColaboradores { get; set; }

        #region [ Campos edição dynamic ]

        [SMCMapProperty("CampanhaCicloLetivo.CicloLetivo.Descricao")]
        public string DescricaoCicloLetivo { get; set; }

        [SMCMapProperty("CampanhaCicloLetivo.Campanha")]
        public CampanhaVO Campanha { get; set; }

        [SMCMapProperty("CampanhaCicloLetivo.SeqCicloLetivo")]
        public long SeqCicloLetivo { get; set; }

        public long SeqProcesso { get; set; }

        public bool? ExigeParceriaIntercambioIngresso { get; set; }

        public bool CadastrarNovaPessoa { get; set; }

        #endregion [ Campos edição dynamic ]
    }
}