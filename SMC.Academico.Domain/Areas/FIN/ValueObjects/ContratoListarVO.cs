using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class ContratoListarVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string NumeroRegistro { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public string DataFimValidade { get; set; }

        public List<ContratoCursosListarVO> Cursos { get; set; }

        public List<ContratoNiveisEnsinoVO> NiveisEnsino { get; set; }
    }
}