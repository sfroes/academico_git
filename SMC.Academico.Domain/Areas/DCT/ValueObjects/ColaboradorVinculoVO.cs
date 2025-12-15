using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorVinculoVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqColaborador { get; set; }

        public long SeqEntidadeVinculo { get; set; }

        public long SeqTipoVinculoColaborador { get; set; }

        public bool InseridoPorCarga { get; set; }

        public bool? EntidadeVinculoGrupoPrograma { get; set; }

        public bool ExibirColaboradorResponsavel { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string TituloPesquisa { get; set; }

        public string Observacao { get; set; }

        public List<ColaboradorResponsavelVinculo> ColaboradoresResponsaveis { get; set; }

        public List<ColaboradorVinculoCursoVO> Cursos { get; set; }

        public List<ColaboradorVinculoFormacaoEspecifica> FormacoesEspecificas { get; set; }

        public long[] SeqsEntidadesResponsaveis { get; set; }

        public string DescricaoTipoVinculoSelect { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public bool PermiteAlterarDadosColaborador { get; set; }

        public MotivoFimVinculo? MotivoFimVinculo { get; set; }

        public bool PermitirAlterarDataFimVinculo { get; set; }
    }
}