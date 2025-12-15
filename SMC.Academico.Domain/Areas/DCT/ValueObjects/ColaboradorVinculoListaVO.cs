using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorVinculoListaVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqColaborador { get; set; }

        public long SeqEntidadeVinculo { get; set; }

        [SMCMapProperty("EntidadeVinculo.Nome")]
        public string NomeEntidadeVinculo { get; set; }

        public List<long> SeqsEntidadeVinculoHierarquiaItens { get; set; }

        [SMCMapProperty("TipoVinculoColaborador.Descricao")]
        public string DescricaoTipoVinculoColaborador { get; set; }

        public bool EntidadeResponsavelAcessivelFiltroDados { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public bool InseridoPorCarga { get; set; }

        public long SeqTipoVinculoColaborador { get; set; }

        public List<ColaboradorVinculoCursoVO> Cursos { get; set; }
    }
}