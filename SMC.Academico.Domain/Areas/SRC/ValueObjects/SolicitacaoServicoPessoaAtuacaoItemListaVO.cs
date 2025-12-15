using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoServicoPessoaAtuacaoItemListaVO : ISMCMappable
    {

        public long SeqSolicitacao { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string DescricaoEtapa { get; set; }

        public string Instrucoes { get; set; }

        public string SituacaoEtapa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public bool ExibirVisualizarPlanoEstudos { get; set; }


    }
}