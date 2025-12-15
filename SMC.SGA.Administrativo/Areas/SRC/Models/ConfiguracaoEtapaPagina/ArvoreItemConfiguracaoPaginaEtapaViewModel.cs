using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ArvoreItemConfiguracaoPaginaEtapaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        public long Seq { get; set; }

        public long SeqEtapaProcesso { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqItem { get; set; }

        public long SeqPai { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public TipoItemPaginaEtapa Tipo { get; set; }

        public bool PaginaPermiteExibicaoOutrasPaginas { get; set; }

        public bool PaginaObrigatoria { get; set; }

        public bool PaginaExibeFormulario { get; set; }

        public bool PaginaPermiteDuplicar { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoDuplicar
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return false;
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoDuplicar
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "MensagemDesabilitaBotao_EtapaEncerrada";
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return "MensagemDesabilitaBotao_EtapaLiberada";
                }

                return string.Empty;
            }
        }

        [SMCHidden]
        public bool HabilitarBotaoExcluir
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return false;
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return false;
                }
                else if (PaginaObrigatoria)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoExcluir
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "MensagemDesabilitaBotao_EtapaEncerrada";
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return "MensagemDesabilitaBotao_EtapaLiberada";
                }
                else if (PaginaObrigatoria)
                {
                    return "MensagemDesabilitaBotaoExcluir_PaginaObrigatoria";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}