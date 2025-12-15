using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConsultaTurmaAmostraViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? SeqConfiguracaoAvaliacaoPpa { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoAvaliacaoPpaTurma { get; set; }

        #region [Alterar Data Limite Resposta]

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataLimiteResposta { get; set; }

        #endregion

        #region [Cabecalho comum]

        public string NomeCabecalho { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoConfiguracaoAvaliacaoPpa { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string EntidadeResponsavel { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public TipoAvaliacaoPpa TipoAvaliacao { get; set; }

        #endregion

        #region [Cabecalho Consulta Amostras]

        [SMCHidden]
        public bool ExibirTurma { get; set; }

        [SMCConditionalDisplay(nameof(ExibirTurma), SMCConditionalOperation.Equals, false)]
        public string Turma { get; set; }

        [SMCConditionalDisplay(nameof(ExibirTurma), SMCConditionalOperation.Equals, true)]
        public string DescricaoTurma { get; set; }

        #endregion

        #region [Cabecalho Alteracao Configuracao]
        
        [SMCHidden]
        public bool ExibirDatas { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }


        #endregion

        #region [Filtro Listas]
        [SMCHidden]
        public ConsultaTurmaAmostraFiltroViewModel Filtro { get; set; }
        #endregion
    }
}