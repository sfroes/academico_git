using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosRelatorioServicoCicloLetivoVO : ISMCMappable
    {
        public long? SeqGrupoPrograma { get; set; }

        public string NomeGrupoPrograma { get; set; }

        public long? SeqEtapa { get; set; }

        public string DescricaoEtapa { get; set; }

        public long? SeqCurso { get; set; }

        public string NomeCurso { get; set; }

        public int? QuantidadeNaoIniciada { get; set; }

        public int? QuantidadeEmAndamento { get; set; }

        public int? QuantidadeFimComSucesso { get; set; }

        public int? QuantidadeFimSemSucesso { get; set; }

        public int? QuantidadeCancelada { get; set; }

    }
}