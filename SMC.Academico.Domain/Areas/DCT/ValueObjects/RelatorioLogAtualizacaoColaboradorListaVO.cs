using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class RelatorioLogAtualizacaoColaboradorListaVO : ISMCMappable
    {
        public string DataProcessamento { get; set; }

        public string Professor { get; set; }

        public string Acao { get; set; }

        public string Motivo { get; set; }

        public string Entidade { get; set; }

        public string AtividadeFinalizada { get; set; }

        public string Aluno { get; set; }

        public string DataInicioAfastamento { get; set; }

        public string DataFimAfastamento { get; set; }

        public bool ExibeColunasInicioFimAfastamento { get; set; }
    }
}