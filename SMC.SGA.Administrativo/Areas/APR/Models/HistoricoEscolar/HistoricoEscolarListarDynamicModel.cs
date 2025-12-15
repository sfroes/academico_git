using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class HistoricoEscolarListarDynamicModel : SMCDynamicViewModel
    {
        public override long Seq { get; set; }

        public long SeqAluno { get; set; }

        public long SeqAlunoHistorico { get; set; }

        /// <summary>
        /// Será somente leitura caso tenha uma origem de avaliação, uma solicitação de dispensa ou um tipo de gestão não permitido para o usuário logado
        /// </summary>
        //public bool SomenteLeitura
        //{
        //    get
        //    {
        //        var tiposPermitidos = SMCAuthorizationHelper.Authorize(UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES) ?
        //            new[] { TipoGestaoDivisaoComponente.Exame, TipoGestaoDivisaoComponente.Turma } :
        //            new[] { TipoGestaoDivisaoComponente.Exame };
        //        return SeqOrigemAvaliacao.HasValue || SeqSolicitacaoDispensa.HasValue || TiposGestaoDivisaoComponente.Except(tiposPermitidos).Any();
        //    }
        //}

        public bool SomenteLeitura { get; set; }

        public string DescricaoAlunoHistorico { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        [SMCDescription]
        public string DescricaoComponenteCurricular { get; set; }

        [SMCValueEmpty("-")]
        public short? CargaHoraria { get; set; }

        [SMCValueEmpty("-")]
        public short? Creditos { get; set; }

        [SMCSelect]
        public ObrigatorioOptativo ObrigatorioOptativo { get; set; }

        [SMCValueEmpty("-")]
        public short? Nota { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoConceito { get; set; }

        [SMCValueEmpty("-")]
        public short? Faltas { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoSituacaoFinal { get; set; }

        public List<string> Colaboradores { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public long? SeqSolicitacaoDispensa { get; set; }

        public List<TipoGestaoDivisaoComponente> TiposGestaoDivisaoComponente { get; set; }

        public string Observacao { get; set; }
    }
}