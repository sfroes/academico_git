using SMC.Framework;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SMC.SGA.Administrativo.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    [SMCAllowAnonymous]
    public class FrontController : SMCControllerBase
    {
        public ActionResult PartialHeader()
        {
            return PartialView("_Header");
        }

        public ActionResult PartialMenu()
        {
            return PartialView("_Menu");
        }

        public ActionResult PartialFooter()
        {
            return PartialView("_Footer");
        }

        public JsonResult BuscarDados()
        {
            var lancamento = new
            {
                seqTurma = 1,
                materiaLecionada = "Ao vivo do servidor!!",
                alunos = new List<object>(),
                trabalhos = new List<object>()
            };

            lancamento.trabalhos.Add(new { seq = 1, valor = 20, titulo = "Prova de Arquitetura de Computadores - 20 pontos" });
            lancamento.trabalhos.Add(new { seq = 2, valor = 20, titulo = "Prova de Algoritimos - 20 pontos" });
            lancamento.trabalhos.Add(new { seq = 3, valor = 10, titulo = "Prova de Redes - 10 pontos" });
            lancamento.trabalhos.Add(new { seq = 4, valor = 50, titulo = "Prova Final - 50 pontos" });

            var idsTrabalhos = new long[] { 1, 2, 3, 4 };
            lancamento.alunos.Add(new { seq = 25209, ra = 25209, lancamentos = CriarTrabalhosAluno(25209, idsTrabalhos), nome = "ADRIANA CARNEIRO DE OLIVEIRA" });
            lancamento.alunos.Add(new { seq = 25368, ra = 25368, lancamentos = CriarTrabalhosAluno(25368, idsTrabalhos), nome = "ALESSANDRO MENDES CARDOSO" });
            lancamento.alunos.Add(new { seq = 25232, ra = 25232, lancamentos = CriarTrabalhosAluno(25232, idsTrabalhos), nome = "ANA FLÁVIA BARROS MOREIRA" });
            lancamento.alunos.Add(new { seq = 25120, ra = 25120, lancamentos = CriarTrabalhosAluno(25120, idsTrabalhos), nome = "ANDRESSA SILMARA ALVES CARVALHO" });
            lancamento.alunos.Add(new { seq = 24826, ra = 24826, lancamentos = CriarTrabalhosAluno(24826, idsTrabalhos), nome = "ANTONIETA CAETANO GONÇALVES" });
            lancamento.alunos.Add(new { seq = 25213, ra = 25213, lancamentos = CriarTrabalhosAluno(25213, idsTrabalhos), nome = "CYNTHIA FITTIPALDI SILVA GUIMARAES" });
            lancamento.alunos.Add(new { seq = 25583, ra = 25583, lancamentos = CriarTrabalhosAluno(25583, idsTrabalhos), nome = "ÉLIO VASCONCELLOS VIEIRA" });
            lancamento.alunos.Add(new { seq = 25730, ra = 25730, lancamentos = CriarTrabalhosAluno(25730, idsTrabalhos), nome = "ELISA BERTILLA DE SIQUEIRA SILVA" });
            lancamento.alunos.Add(new { seq = 24743, ra = 24743, lancamentos = CriarTrabalhosAluno(24743, idsTrabalhos), nome = "ÉRICA MONTEIRO BARBOSA" });
            lancamento.alunos.Add(new { seq = 25191, ra = 25191, lancamentos = CriarTrabalhosAluno(25191, idsTrabalhos), nome = "FABIANO DE OLIVEIRA COSTA" });
            lancamento.alunos.Add(new { seq = 25030, ra = 25030, lancamentos = CriarTrabalhosAluno(25030, idsTrabalhos), nome = "FRANCISCO JOSÉ VILAS BÔAS NETO" });
            lancamento.alunos.Add(new { seq = 25255, ra = 25255, lancamentos = CriarTrabalhosAluno(25255, idsTrabalhos), nome = "GISLANE TESTI COLET" });
            lancamento.alunos.Add(new { seq = 25449, ra = 25449, lancamentos = CriarTrabalhosAluno(25449, idsTrabalhos), nome = "ISABEL PAULINA SILVA CASTRO" });
            lancamento.alunos.Add(new { seq = 25321, ra = 25321, lancamentos = CriarTrabalhosAluno(25321, idsTrabalhos), nome = "JOAO ALVES DE SOUZA JUNIOR" });
            lancamento.alunos.Add(new { seq = 01988, ra = 01988, lancamentos = CriarTrabalhosAluno(01988, idsTrabalhos), nome = "JOSUE EDSON LEITE" });
            lancamento.alunos.Add(new { seq = 25225, ra = 25225, lancamentos = CriarTrabalhosAluno(25225, idsTrabalhos), nome = "LEANDRO MAGNO XAVIER FERRARI" });
            lancamento.alunos.Add(new { seq = 25193, ra = 25193, lancamentos = CriarTrabalhosAluno(25193, idsTrabalhos), nome = "LÍVIA GUIMARÃES GONÇALVES" });
            lancamento.alunos.Add(new { seq = 24627, ra = 24627, lancamentos = CriarTrabalhosAluno(24627, idsTrabalhos), nome = "MARCELO AZEVEDO MAFFRA" });
            lancamento.alunos.Add(new { seq = 25169, ra = 25169, lancamentos = CriarTrabalhosAluno(25169, idsTrabalhos), nome = "MARCOS EDMAR RAMOS ALVARES DA SILVA" });
            lancamento.alunos.Add(new { seq = 25265, ra = 25265, lancamentos = CriarTrabalhosAluno(25265, idsTrabalhos), nome = "NANCY RAQUEL DUTRA FELIPETTO MALTA" });
            lancamento.alunos.Add(new { seq = 25168, ra = 25168, lancamentos = CriarTrabalhosAluno(25168, idsTrabalhos), nome = "NÚBIA LEONI DE FREITAS NOGUEIRA" });
            lancamento.alunos.Add(new { seq = 24976, ra = 24976, lancamentos = CriarTrabalhosAluno(24976, idsTrabalhos), nome = "RAFAEL CASTRO DE PAULA MACHADO" });
            lancamento.alunos.Add(new { seq = 24813, ra = 24813, lancamentos = CriarTrabalhosAluno(24813, idsTrabalhos), nome = "RENAN ARAUJO E FREITAS" });
            lancamento.alunos.Add(new { seq = 25208, ra = 25208, lancamentos = CriarTrabalhosAluno(25208, idsTrabalhos), nome = "RODRIGO ALVIM GUSMAN PEREIRA" });

            return Json(lancamento);
        }

        private List<object> CriarTrabalhosAluno(long idAluno, long[] idsTrabalhos)
        {
            var trabalhos = new List<object>();
            foreach (var idTrabalho in idsTrabalhos)
            {
                trabalhos.Add(new
                {
                    seqAluno = idAluno,
                    seqTrabalho = idTrabalho,
                    valor = (null as short?)
                });
            }
            return trabalhos;
        }

        public class Lancamento
        {
            public long SeqAluno { get; set; }
            public long[] Valores { get; set; }
        }

        public JsonResult ValidarSituacaoFinal(Lancamento lancamento)
        {
            var total = lancamento.Valores.Sum();
            bool aprovado = total >= 70;
            return Json(aprovado ? "Aprovado" : "Reprovado");
        }
    }
}