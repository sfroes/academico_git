using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class OrientacaoListarDynamicModel : SMCDynamicViewModel
    {
        public override long Seq { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoOrientacao { get; set; }

        public List<OrientacaoColaboradorViewModel> OrientacoesColaborador { get; set; }

        public List<OrientacaoPessoaAtuacaoViewModel> OrientacoesPessoaAtuacao { get; set; }

        public InstituicaoEnsinoData InstituicaoEnsino { get; set; }

        public NivelEnsinoData NivelEnsino { get; set; }

        public TipoOrientacaoData TipoOrientacao { get; set; }

        public string NomesAlunosExclucao { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        #region CamposAuxiliares

        public bool HabilitarBotaoExcluir
        {
            get {

                if (this.SeqTipoTermoIntercambio.HasValue)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoExcluir
        {
            get
            {

                if (this.SeqTipoTermoIntercambio.HasValue)
                {
                    return "Mensagem_Botao_Excluir_Instruction";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}