using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class EnvioNotificacaoFiltroSelecaoData : SMCPagerFilterData, ISMCMappable
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        public List<SMCDatasourceItem> SituacoesMatricula { get; set; }

        public List<SMCDatasourceItem> TiposAtividadeColaborador { get; set; }

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> Localidades { get; set; }

        public List<SMCDatasourceItem> NiveisEnsino { get; set; }
        public List<SMCDatasourceItem> LayoutEmail { get; set; }

        #endregion [ DataSources ]

        public TipoAtuacao TipoAtuacao { get; set; }
        public List<long> SeqsEntidadesResponsaveis { get; set; }
        public long? SeqLocalidade { get; set; }
        public long? SeqNivelEnsino { get; set; }
        public long? SeqCursoOferta { get; set; }
        public long? SeqTurno { get; set; }
        public long? SeqCicloLetivoSituacaoMatricula { get; set; }
        public List<long> SeqsSituacaoMatriculaCicloLetivo { get; set; }
        public long? SeqFormacaoEspecifica { get; set; }
        public string Turma { get; set; }
        public long? NumeroRegistroAcademico { get; set; }
        public long? SeqTipoVinculoAluno { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public long? SeqCursoOfertaLocalidade { get; set; }
        public TipoAtividadeColaborador? TipoAtividade { get; set; }
        public long? SeqInstituicaoExterna { get; set; }
        public long? SeqColaborador { get; set; }
        public long? SeqTipoVinculoColaborador { get; set; }
    }
}