using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class UploadSolicitacaoPadraoPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_UPLOAD_DOCUMENTO;

        public List<SolicitacaoDocumentoViewModel> Documentos { get; set; }
        public bool DocumentosPendentes
        {
            get
            {
                var pendente = DocumentosObrigatorios?.Any(d => d.SituacaoEntregaDocumentoLista != SituacaoEntregaDocumentoLista.RegistroDocumentoOK);

                // Caso não tenha nenhum documento obrigatório faltando, verifica se todos os grupos foram atendidos.
                if (pendente.HasValue && !pendente.Value)
                {
                    GruposDocumentosObrigatorios.ToList().ForEach(g =>
                    {
                        var totalOkGrupo = g.Value.Count(d => d.SituacaoEntregaDocumentoLista == SituacaoEntregaDocumentoLista.RegistroDocumentoOK);
                        if (totalOkGrupo < g.Key.NumeroMinimoDocumentosRequerido)
                            pendente = true;
                    });
                }
                return pendente.GetValueOrDefault();
            }
        }

        public IEnumerable<SolicitacaoDocumentoViewModel> DocumentosObrigatorios
        {
            get
            {
                return Documentos?.Where(d => d.Obrigatorio && (d.Grupos == null || !d.Grupos.Any()));
            }
        }

        public IEnumerable<SolicitacaoDocumentoViewModel> DocumentosOpcionais
        {
            get
            {
                return Documentos?.Where(d => !d.Obrigatorio && (d.Grupos == null || !d.Grupos.Any()));
            }
        }

        public Dictionary<GrupoDocumentoViewModel, List<SolicitacaoDocumentoViewModel>> GruposDocumentosObrigatorios
        {
            get
            {
                var ret = new Dictionary<GrupoDocumentoViewModel, List<SolicitacaoDocumentoViewModel>>();
                if (Documentos != null)
                {
                    Documentos.Where(d => d.Grupos != null && d.Grupos.Any()).SMCForEach(d =>
                    {
                        d.Grupos.ForEach(g =>
                        {
                            if (ret.ContainsKey(g))
                                ret[g].Add(d);
                            else
                                ret.Add(g, new List<SolicitacaoDocumentoViewModel> { d });
                        });
                    });
                }
                //return Documentos?.Where(d => d.SeqGrupoDocumentoRequerido.HasValue).GroupBy(x => x.SeqGrupoDocumentoRequerido);
                return ret;
            }
        }
    }
}