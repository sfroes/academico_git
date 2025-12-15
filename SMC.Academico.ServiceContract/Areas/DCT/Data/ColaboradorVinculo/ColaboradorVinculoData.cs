using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorVinculoData : ISMCSeq, ISMCMappable
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

        public List<ColaboradorResponsavelVinculoData> ColaboradoresResponsaveis { get; set; }

        public List<ColaboradorVinculoCursoData> Cursos { get; set; }

        public List<ColaboradorVinculoFormacaoEspecificaData> FormacoesEspecificas { get; set; }

        public long[] SeqsEntidadesResponsaveis { get; set; }

        public string DescricaoTipoVinculoSelect { get; set; }

        public List<SMCDatasourceItem> TiposAtividades { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public bool PermiteAlterarDadosColaborador { get; set; }

        public bool PermitirAlterarDataFimVinculo { get; set; }

        public MotivoFimVinculo? MotivoFimVinculo { get; set; }
    }
}