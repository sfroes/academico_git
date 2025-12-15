using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.ValueObjects
{
    public class EtapaListaVO : ISMCMappable
    {
        public string DescricaoEtapa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long SeqEtapaSGF { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public long? SeqEscalonamento { get; set; }

        public bool Ativo { get; set; }

        public bool ExibirVisualizarPlanoEstudos { get; set; }

        public bool PossuiFluxoNaAplicacaoSGAAluno { get; set; }

        public string Instrucoes { get; set; }

        public SituacaoEtapaSolicitacaoMatricula SituacaoEtapaIngressante { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqTemplateProcessoSGF { get; set; }

        public List<EtapaSituacaoVO> Situacoes { get; set; }

        public List<EtapaPaginaVO> Paginas { get; set; }

        public int OrdemEtapaSGF { get; set; }

        public virtual IList<SolicitacaoHistoricoNavegacao> HistoricosNavegacao { get; set; }

        public virtual IList<SolicitacaoHistoricoSituacao> HistoricosSituacao { get; set; }
    }
}