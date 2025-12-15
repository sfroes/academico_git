using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorVO : InformacoesPessoaVO, ISMCMappable
    {
        public long SeqEntidadeVinculo { get; set; }

        public long SeqTipoVinculoColaborador { get; set; }

        [SMCMapProperty("DataInicio")]
        public DateTime DataInicioVinculo { get; set; }

        [SMCMapProperty("DataFim")]
        public DateTime? DataFimVinculo { get; set; }

        public string TituloPesquisa { get; set; }

        public string Observacao { get; set; }

        public List<ColaboradorInstituicaoExternaVO> InstituicoesExternas { get; set; }

        public List<ColaboradorResponsavelVinculo> ColaboradoresResponsaveis { get; set; }

        public List<ColaboradorVinculoCurso> Cursos { get; set; }

        public List<ColaboradorVinculoFormacaoEspecifica> FormacoesEspecificas { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public long SeqHierarquiaClassificacao { get; set; }

        public long? SeqTitulacao { get; set; }

        public string Descricao { get; set; }

        public short? AnoInicio { get; set; }

        public short? AnoObtencaoTitulo { get; set; }

        public bool? TitulacaoMaxima { get; set; }

        public string Curso { get; set; }

        public string Orientador { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public long? SeqClassificacao { get; set; }

        public List<long> SeqDocumentoApresentado { get; set; }
    }
}