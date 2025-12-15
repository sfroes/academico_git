using SMC.Academico.UI.Mvc.Areas.ALN.Controllers;
using SMC.Academico.UI.Mvc.Areas.APR.Controllers;
using SMC.Academico.UI.Mvc.Areas.CAM.Controllers;
using SMC.Academico.UI.Mvc.Areas.CNC.Controllers;
using SMC.Academico.UI.Mvc.Areas.CSO.Controllers;
using SMC.Academico.UI.Mvc.Areas.CUR.Controllers;
using SMC.Academico.UI.Mvc.Areas.DCT.Controllers;
using SMC.Academico.UI.Mvc.Areas.MAT.Controllers;
using SMC.Academico.UI.Mvc.Areas.PES.Controllers;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Academico.UI.Mvc.Areas.TUR.Controllers;
using SMC.Academico.UI.Mvc.Constants;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.Academico.UI.Mvc
{
    public static class AcademicoRouteExtensions
    {
        public static void RegistrarRotaAcademico(this RouteCollection routes)
        {
            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.DETALHES_DIVISAO_TURMA_ROUTE_KEY,
                url: "DetalhesDivisaoTurmaRoute/{action}",
                defaults: new { controller = "DetalhesDivisaoTurma", area = "" },
                namespaces: new[] { typeof(DetalhesDivisaoTurmaController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.SOLICITACAO_MATRICULA_ITEM_ROUTE_KEY,
                url: "SolicitacaoMatriculaItemRoute/{action}",
                defaults: new { controller = "SolicitacaoMatriculaItem", area = "" },
                namespaces: new[] { typeof(SolicitacaoMatriculaItemController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.REGISTRO_DOCUMENTOS_ROUTE_KEY,
                url: "RegistroDocumentosItemRoute/{action}",
                defaults: new { controller = "RegistroDocumento", area = "" },
                namespaces: new[] { typeof(RegistroDocumentoController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.PROCESSO_ROUTE_KEY,
                url: "ProcessoRoute/{action}",
                defaults: new { controller = "Processo", area = "" },
                namespaces: new[] { typeof(ProcessoController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.SELECAO_ATIVIDADE_ROUTE_KEY,
                url: "SelecaoAtividadeRoute/{action}",
                defaults: new { controller = "SelecaoAtividade", area = "" },
                namespaces: new[] { typeof(SelecaoAtividadeController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.SELECAO_TURMA_ROUTE_KEY,
                url: "SelecaoTurmaRoute/{action}",
                defaults: new { controller = "SelecaoTurma", area = "" },
                namespaces: new[] { typeof(SelecaoTurmaController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.PESSOA_ENDERECO_ROUTE_KEY,
                url: "PessoaEnderecoRoute/{action}",
                defaults: new { controller = "PessoaEndereco", area = "" },
                namespaces: new[] { typeof(PessoaEnderecoController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.PESSOA_TELEFONE_ROUTE_KEY,
                url: "PessoaTelefoneRoute/{action}",
                defaults: new { controller = "PessoaTelefone", area = "" },
                namespaces: new[] { typeof(PessoaTelefoneController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.PESSOA_ENDERECO_ELETRONICO_ROUTE_KEY,
                url: "PessoaEnderecoEletronicoRoute/{action}",
                defaults: new { controller = "PessoaEnderecoEletronico", area = "" },
                namespaces: new[] { typeof(PessoaEnderecoEletronicoController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.COMPROVANTE_MATRICULA_ROUTE_KEY,
                url: "ComprovanteMatriculaRoute/{action}",
                defaults: new { controller = "ComprovanteMatriculaController", area = "MAT" },
                namespaces: new[] { typeof(ComprovanteMatriculaController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.SOLICITACAO_SERVICO_ROUTE_KEY,
                url: "SolicitacaoServicoRoute/{action}",
                defaults: new { controller = "SolicitacaoServico", area = "" },
                namespaces: new[] { typeof(SolicitacaoServicoController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.LANCAMENTO_HISTORICO_ESCOLAR_ROUTE_KEY,
                url: "LancamentoHistoricoEscolarRoute/{action}",
                defaults: new { controller = "LancamentoHistoricoEscolar", area = "", action = "index" },
                namespaces: new[] { typeof(LancamentoHistoricoEscolarController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.INTEGRALIZACAO_CURRICULAR_ROUTE_KEY,
                url: "IntegralizacaoCurricularRoute/{action}",
                defaults: new { controller = "IntegralizacaoCurricular", area = "" },
                namespaces: new[] { typeof(IntegralizacaoCurricularController).Namespace }
            );

            routes.MapRoute(
               name: ACADEMICO_ROUTE_KEYS.COMPONENTE_CURRICULAR_EMENTA_ROUTE_KEY,
               url: "ComponenteCurricularEmentaRoute/{action}",
               defaults: new { controller = "ComponenteCurricularEmenta", area = "" },
               namespaces: new[] { typeof(ComponenteCurricularEmentaController).Namespace }
           );

            routes.MapRoute(
               name: ACADEMICO_ROUTE_KEYS.CHANCELA_MATRICULA_ROUTE_KEY,
               url: "ChancelaRoute/{action}",
               defaults: new { controller = "Chancela", area = "", action = "index" },
               namespaces: new[] { typeof(ChancelaController).Namespace }
           );

            routes.MapRoute(
               name: ACADEMICO_ROUTE_KEYS.ALUNO_ROUTE_KEY,
               url: "AlunoRoute/{action}",
               defaults: new { controller = "Aluno", area = "ALN", action = "index" },
               namespaces: new[] { typeof(AlunoController).Namespace }
           );

            routes.MapRoute(
               name: ACADEMICO_ROUTE_KEYS.COMPONENTE_CURRICULAR_ROUTE_KEY,
               url: "ComponenteCurricularRoute/{action}",
               defaults: new { controller = "ComponenteCurricular", area = "" },
               namespaces: new[] { typeof(SMC.Academico.UI.Mvc.Areas.CUR.Controllers.ComponenteCurricularController).Namespace }
           );

            routes.MapRoute(
               name: ACADEMICO_ROUTE_KEYS.CREDENCIAIS_ACESSO_ROUTE_KEY,
               url: "CredenciaisAcessoRoute/{action}",
               defaults: new { controller = "CredenciaisAcesso", area = "", action = "index" },
               namespaces: new[] { typeof(SMC.Academico.UI.Mvc.Areas.PES.Controllers.CredenciaisAcessoController).Namespace }
           );

            routes.MapRoute(
              name: ACADEMICO_ROUTE_KEYS.SUPORTE_TECNICO_ROUTE_KEY,
              url: "SuporteTecnicoRoute/{action}",
              defaults: new { controller = "SuporteTecnico", area = "", action = "index" },
              namespaces: new[] { typeof(SMC.Academico.UI.Mvc.Areas.PES.Controllers.SuporteTecnicoController).Namespace }
          );

            routes.MapRoute(
               name: ACADEMICO_ROUTE_KEYS.AULA_ROUTE_KEY,
               url: "AulaRoute/{action}",
               defaults: new { controller = "Aula", area = "", action = "index" },
               namespaces: new[] { typeof(SMC.Academico.UI.Mvc.Areas.APR.Controllers.AulaController).Namespace }
           );

            routes.MapRoute(
               name: ACADEMICO_ROUTE_KEYS.ENTREGA_ONLINE_ROUTE_KEY,
               url: "EntregaOnlineRoute/{action}",
               defaults: new { controller = "EntregaOnlineShared", area = "", action = "index" },
               namespaces: new[] { typeof(SMC.Academico.UI.Mvc.Areas.APR.Controllers.EntregaOnlineSharedController).Namespace }
           );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.LOOKUP_TURMA_ROUTE_KEY,
                url: "LookupTurmaRoute/{action}",
                defaults: new { controller = "LookupTurma", area = "" },
                namespaces: new[] { typeof(LookupTurmaController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.LOOKUP_CICLO_LETIVO_ROUTE_KEY,
                url: "LookupCilcoLetivoRoute/{action}",
                defaults: new { controller = "LookupCicloLetivo", area = "" },
                namespaces: new[] { typeof(LookupCicloLetivoController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.LOOKUP_COLABORADOR_ROUTE_KEY,
                url: "LookupColaboradorRoute/{action}",
                defaults: new { controller = "LookupColaborador", area = "" },
                namespaces: new[] { typeof(LookupColaboradorController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.LOOKUP_CURSO_OFERTA_ROUTE_KEY,
                url: "LookupCursoOfertaRoute/{action}",
                defaults: new { controller = "LookupCursoOferta", area = "" },
                namespaces: new[] { typeof(LookupCursoOfertaController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.LOOKUP_INSTITUICAO_EXTERNA_ROUTE_KEY,
                url: "LookupInstituicaoExternaRoute/{action}",
                defaults: new { controller = "LookupInstituicaoExterna", area = "" },
                namespaces: new[] { typeof(LookupInstituicaoExternaController).Namespace }
            );

            routes.MapRoute(
                name: ACADEMICO_ROUTE_KEYS.LOOKUP_CURSO_OFERTA_LOCALIDADE_ROUTE_KEY,
                url: "LookupCursoOfertaLocalidadeRoute/{action}",
                defaults: new { controller = "LookupCursoOfertaLocalidade", area = "" },
                namespaces: new[] { typeof(LookupCursoOfertaLocalidadeController).Namespace }
            );
        }
    }
}