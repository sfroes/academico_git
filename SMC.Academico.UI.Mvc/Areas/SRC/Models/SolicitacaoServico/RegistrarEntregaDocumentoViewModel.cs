using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class RegistrarEntregaDocumentoViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => MatriculaTokens.REGISTRO_DOCUMENTO_ENTREGUE;

        [SMCHidden]
        //public override string ChaveTextoBotaoProximo => "Botao_Confirmar";
        public override string ChaveTextoBotaoProximo
        {
            get
            {
                if (TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU)
                    return "Botao_Confirmar_Renovacao";
                else if (TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
                    return "Botao_Confirmar_Reabertura";
                else if (TokenServico == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU
                      || TokenServico == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA)
                    return "Botao_Efetivar";
                else
                    return "Botao_Navegacao_Proximo";
            }
        }

        //[SMCHidden]
        //public bool RenovacaoMatricula { get; set; }

        public List<SolicitacaoDocumentoViewModel> Documentos { get; set; }

        public bool DocumentosPendentes
        {
            get
            {
                //if (RenovacaoMatricula)
                //{
                    var pendente = false;

                    if (HabilitarEfetivacaoMatricula)
                        pendente = false;
                    else
                        pendente = true;

                    var documentosPendentes = DocumentosObrigatorios.Where(c => c.Documentos.SMCAny(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)).ToList();

                    if (documentosPendentes.Count > 0)
                    {
                        var docsPendentesSemPrazoEntrega = documentosPendentes.Where(c => c.Documentos.SMCAny(d => d.DataPrazoEntrega == null)).ToList();

                        if (docsPendentesSemPrazoEntrega.Count > 0)
                        {
                            ExistemDocumentosPendentesSemPrazoEntrega = true;
                        }
                    }

                   // return pendente;
                //}
                //else
                //{
                    // - Caso todos os documentos obrigatórios para a etapa estejam com a situação "Deferido", "Pendente" ou "Aguardando análise do setor responsável", exibir a seguinte mensagem:
                    // "Não existe validação de entrega de documentos obrigatórios pendente. Clique em "Próximo" para prosseguir."
                    // -Caso exista pelo menos um documento obrigatório configurado para etapa com a situação diferente de "Deferido", "Pendente" ou "Aguardando análise do setor responsável", exibir a seguinte mensagem:
                    // "Existem documentos obrigatórios com validação de entrega pendente. Clique em "Registrar entrega de documentos" para realizar a validação."
                    // Exibido em RegistrarEntregaDocumentos.cshtml

                    var obrigatoriosValidos = DocumentosObrigatorios.Where(c => c.Documentos.All(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido
                                                                                                   || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente
                                                                                                   || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)).ToList();

                    pendente = obrigatoriosValidos.Count() != DocumentosObrigatorios.Count();

                    // Caso não tenha nenhum documento obrigatório faltando, verifica se todos os grupos foram atendidos.
                    if (!pendente)
                    {
                        GruposDocumentosObrigatorios.ToList().ForEach(g =>
                        {
                            var totalOkGrupo = g.Value.Count(d => d.Documentos.All(a => a.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido
                                                                                     || a.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente
                                                                                     || a.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel));


                            if (totalOkGrupo < g.Key.NumeroMinimoDocumentosRequerido)
                                pendente = true;
                        });
                    }

                    return pendente;
               // }
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
                //return Documentos?.Where(d => d.SeqGrupoDocumentoRequerido.HasValue).GroupBy(x => x.SeqGrupoDocumentoRequerido);
                return ret;
            }
        }

        public bool ExibirSegundaVia { get; set; }
    }
}