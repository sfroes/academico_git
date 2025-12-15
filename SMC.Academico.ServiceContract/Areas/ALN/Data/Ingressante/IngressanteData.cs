using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Data.Ingressante;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class IngressanteData : PessoaAtuacaoData
    {
        public SituacaoIngressante SituacaoIngressante { get; set; }

        public OrigemIngressante OrigemIngressante { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public long SeqFormaIngresso { get; set; }

        public long? SeqInstituicaoTransferenciaExterna { get; set; }

        public string CursoTransferenciaExterna { get; set; }

        public long SeqCampanhaCicloLetivo { get; set; }

        public long SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqProcessoSeletivo { get; set; }

        public string DescricaoProcessoSeletivo { get; set; }

        public long? SeqInscricaoGpi { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public bool? GrupoEscalonamentoAtivo { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public DateTime DataAdmissao { get; set; }

        public DateTime? DataPrevisaoConclusao { get; set; }

        public long SeqProcesso { get; set; }

        public bool? ExigeParceriaIntercambioIngresso { get; set; }

        public bool CadastrarNovaPessoa { get; set; }

        #region Props Navigation

        [SMCMapProperty("ProcessoSeletivo.Descricao")]
        public string DescricaoProcesso { get; set; }

        [SMCMapProperty("ProcessoSeletivo.Descricao")]
        public string NomeCicloLetivo { get; set; }

        #endregion Props Navigation

        #region Navigation

        public CampanhaData Campanha { get; set; }

        //public FormaIngressoData FormaIngresso { get; set; }

        //public TipoVinculoAlunoData TipoVinculoAluno { get; set; }

        public MatrizCurricularOfertaData MatrizCurricularOferta { get; set; }

        //public CampanhaCicloLetivoData CampanhaCicloLetivo { get; set; }

        public EntidadeData EntidadeResponsavel { get; set; }

        //public GrupoEscalonamentoData GrupoEscalonamento { get; set; }

        //public NivelEnsinoData NivelEnsino { get; set; }

        //public ProcessoSeletivoData ProcessoSeletivo { get; set; }

        //public ConfiguracaoProcessoData ConfiguracaoProcesso { get; set; }

        //public List<IngressanteFormacaoEspecificaData> FormacoesEspecificas { get; set; }

        public List<IngressanteOfertaData> Ofertas { get; set; }

        //public List<IngressanteHistoricoSituacaoData> HistoricosSituacao { get; set; }

        public List<CondicoesObrigatoriedadeData> CondicoesObrigatoriedade { get; set; }

        public List<PessoaAtuacaoTermoIntercambioData> TermosIntercambio { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public List<IngressanteOrientacaoData> OrientacaoParticipacoesColaboradores { get; set; }

        public long? SeqCampanhaOferta { get; set; }

        #endregion Navigation
    }
}