using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class EtapaListaData : ISMCMappable
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

        public SituacaoEtapaSolicitacaoMatricula SituacaoEtapaIngressante { get; set; }

        public string Instrucoes { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqTemplateProcessoSGF { get; set; }


        public List<EtapaSituacaoData> Situacoes { get; set; }

        public List<EtapaPaginaData> Paginas { get; set; }

        public int OrdemEtapaSGF { get; set; }
    }
}