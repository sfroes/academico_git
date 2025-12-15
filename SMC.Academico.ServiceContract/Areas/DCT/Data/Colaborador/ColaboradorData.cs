using SMC.Academico.ServiceContract.Areas.PES.Data;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorData : PessoaAtuacaoData
    {
        public long SeqPessoa { get; set; }

        public List<ColaboradorInstituicaoExternaData> InstituicoesExternas { get; set; }

        public long SeqEntidadeVinculo { get; set; }

        public long SeqTipoVinculoColaborador { get; set; }

        public DateTime DataInicioVinculo { get; set; }

        public DateTime? DataFimVinculo { get; set; }

        public string TituloPesquisa { get; set; }

        public string Observacao { get; set; }

        public List<ColaboradorResponsavelVinculoData> ColaboradoresResponsaveis { get; set; }

        public List<ColaboradorVinculoCursoData> Cursos { get; set; }

        public List<ColaboradorVinculoFormacaoEspecificaData> FormacoesEspecificas { get; set; }

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