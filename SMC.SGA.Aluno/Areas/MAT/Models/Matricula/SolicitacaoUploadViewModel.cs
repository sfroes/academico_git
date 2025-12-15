using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class SolicitacaoUploadViewModel : MatriculaPaginaViewModelBase
    {
        //public long SeqSolicitacaoServico { get; set; }
        public override string Token => MatriculaTokens.SOLICITACAO_UPLOAD_DOCUMENTO;

        public List<SolicitacaoMatriculaDocumentoViewModel> Documentos { get; set; }

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

        public IEnumerable<SolicitacaoMatriculaDocumentoViewModel> DocumentosObrigatorios
        {
            get
            {
                if (Documentos.SMCAny())
                {
                    var documentosObrigatorios = Documentos.Where(d => d.Obrigatorio && (d.Grupos == null || !d.Grupos.Any()));

                    return documentosObrigatorios;
                }

                return Documentos?.Where(d => d.Obrigatorio && (d.Grupos == null || !d.Grupos.Any()));
            }
        }

        public List<SolicitacaoMatriculaDocumentoViewModel> DocumentosObrigatoriosNaoEntregues
        {
            get
            {
                if (DocumentosObrigatorios.Count() > 0)
                {
                    var documentosPendentes = DocumentosObrigatorios.Where(c => c.Documentos.SMCAny(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente
                                                                                                      || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao
                                                                                                      || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega));
                    return documentosPendentes.ToList();
                }
                else
                {
                    return new List<SolicitacaoMatriculaDocumentoViewModel>();
                }
            }
        }

        public bool EntregaPosteriorDocumentosObrigatorios
        {
            get
            {
                var retorno = Documentos?.Any(a => a.Obrigatorio && a.PermiteEntregaPosterior && (a.Grupos == null || !a.Grupos.Any()));
                return (bool)(retorno != null ? retorno : false);
            }
        }

        public bool DesabilitaBotaoDocumentosObrigatorios
        {
            get
            {
                if (DocumentosObrigatorios.SMCAny())
                {
                    var retornoDocsObrigatorios = DocumentosObrigatoriosNaoEntregues.All(al => !al.PermiteUploadArquivo);

                    return retornoDocsObrigatorios;

                    //var retorno = Documentos.Where(a => a.Obrigatorio && (a.Grupos == null || !a.Grupos.Any())).Count() == 1 && Documentos.Any(a => a.Obrigatorio && !a.PermiteUploadArquivo && (a.Grupos == null || !a.Grupos.Any()));
                    //return retorno;
                }
                return false;
            }
        }

        public IEnumerable<SolicitacaoMatriculaDocumentoViewModel> DocumentosOpcionais
        {
            get
            {
                if (Documentos.SMCAny())
                {
                    var docsOpcionais = Documentos?.Where(d => !d.Obrigatorio && (d.Grupos == null || !d.Grupos.Any()));

                    return docsOpcionais;
                }

                return Documentos?.Where(d => !d.Obrigatorio && (d.Grupos == null || !d.Grupos.Any()));
            }
        }

        public List<SolicitacaoMatriculaDocumentoViewModel> DocumentosOpcionaisNaoEntregues
        {
            get
            {
                if (DocumentosOpcionais.Count() > 0)
                {
                    var documentosPendentes = DocumentosOpcionais.Where(c => c.Documentos.SMCAny(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente
                                                                                                   || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao
                                                                                                   || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega));
                    return documentosPendentes.ToList();
                }
                else
                {
                    return new List<SolicitacaoMatriculaDocumentoViewModel>();
                }
            }
        }

        public bool EntregaPosteriorDocumentosOpcionais
        {
            get
            {
                var retorno = Documentos?.Any(a => !a.Obrigatorio && a.PermiteEntregaPosterior && (a.Grupos == null || !a.Grupos.Any()));
                return (bool)(retorno != null ? retorno : false);
            }
        }

        /// <summary>
        /// Se algum documento estiver marcado com entrega posterior e tiver termo de aceite marcado exibir a mensagem com o checkbox
        /// </summary>
        public bool ExibirTermoEntregaDocumentacao
        {
            get
            {
                var exiteDocumentosMarcadosPosterior = Documentos?.Any(a => a.Documentos.Any(aa => aa.EntregaPosterior.GetValueOrDefault()));

                if (exiteDocumentosMarcadosPosterior.GetValueOrDefault() && !string.IsNullOrEmpty(DescricaoTermoEntregaDocumentacao))
                {
                    return true;
                }

                return false;
            }
        }

        public bool DesabilitaBotaoDocumentosOpcionais
        {
            get
            {
                if (DocumentosOpcionais.SMCAny())
                {
                    var retornoDocsOpcionais = DocumentosOpcionais.All(al => !al.PermiteUploadArquivo);

                    return retornoDocsOpcionais;
                }

                return false;
            }
        }

        public Dictionary<GrupoDocumentoViewModel, List<SolicitacaoMatriculaDocumentoViewModel>> GruposDocumentosObrigatorios
        {
            get
            {
                var ret = new Dictionary<GrupoDocumentoViewModel, List<SolicitacaoMatriculaDocumentoViewModel>>();
                if (Documentos != null)
                {
                    Documentos.Where(d => d.Grupos != null && d.Grupos.Any()).SMCForEach(d =>
                    {
                        d.Grupos.ForEach(g =>
                        {
                            if (ret.ContainsKey(g))
                                ret[g].Add(d);
                            else
                                ret.Add(g, new List<SolicitacaoMatriculaDocumentoViewModel> { d });
                        });
                    });
                }

                return ret;
            }
        }

        public Dictionary<GrupoDocumentoViewModel, List<SolicitacaoMatriculaDocumentoViewModel>> GruposDocumentosObrigatoriosNaoEntregues
        {
            get
            {
                var grupoDocsObrigatorios = GruposDocumentosObrigatorios;

                if (GruposDocumentosObrigatorios.Count > 0)
                {
                    foreach (var grupo in grupoDocsObrigatorios)
                    {
                        grupo.Value.RemoveAll(c => c.Documentos.SMCAny(e => e.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido
                                                                         || e.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel));
                    }
                    
                }

                return grupoDocsObrigatorios;
            }
        }

        public bool EntregaPosteriorGruposDocumentosObrigatorios
        {
            get
            {
                var retorno = Documentos?.Any(a => !a.Obrigatorio && a.PermiteEntregaPosterior && (a.Grupos != null || a.Grupos.Any()));
                return (bool)(retorno != null ? retorno : false);
            }
        }

        [SMCHideLabel]
        public bool AceiteTermoEntregaDocumentacao { get; set; }

        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoTermoEntregaDocumentacao { get; set; }

        #region [ Métodos ]
        public bool GrupoDesabilitaBotaoUploadDocumentos(KeyValuePair<GrupoDocumentoViewModel, List<SolicitacaoMatriculaDocumentoViewModel>> grupoDocumentos)
        {

            if (grupoDocumentos.Value.SMCAny())
            {
                var retornoDocsOpcionais = grupoDocumentos.Value.All(al => !al.PermiteUploadArquivo);

                return retornoDocsOpcionais;
            }

            return false;
        }
        #endregion
    }
}