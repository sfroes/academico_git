using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Academico.Tests.ADM.DCT;
using SMC.Framework.Test;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace SMC.Academico.Tests.ADM.SRC
{
    /********************************************************************        ATENÇÃO        ********************************************************************/
    /* Antes de executar o script, verficiar de existe parametrização na tela "Associação de Vínculo por Instituição e Nível de Ensino" (SGA Administrativo), 
       menu Parâmetros por Nível de Ensino > Aluno > Tipo de Vínculo de Aluno x Nível de Ensino.
       A parametrização deve considerar o Nível de ensino, Vínculo (Curso Regular), Tipo de termo de intercâmbio e o Tipo de orintação (Orientação em Intercâmbio). */
    
  /* Todos os scripts de intercâmbio e de encerramento foram comentados devido a dificuldade de passagem de parâmetro
  Pensou-se em fazer um select e passar como parâmetro o protocolo do intercâmbio, mas se não houvesse nenhum o script falharia. Depois pensou-se em
  criar a solicitação de intercâmbio, mas haveria a necessidade de passar o filtro do aluno que poderia fazer a solicitação de intercâmbio e caiu na mesma situação.
  Decidimos suspender o avanço, por enquanto.

    //Método para validar modal de atendimento associado a outro usuário.
    public enum TipoBotao
    {
        BotaoNao = 0,
        BotaoSim = 1
    }

    public class SolicitacaoServico : SMCSeleniumUnitTest
    {
        //Informar Protocolo da solicitação que deseja realizar atendimento.
        private string _protocolo = "2019.001905";
        private string _nomeProfessor = "Angela Franca Versiani";

        [Fact]
        [Trait("Cenário", "Administrativo - Realizar Atedimento Solicitação Cotutela - Deferimento")]

        public void RealizarAtendimentoCotutelaDeferimento()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.Name("NumeroProtocolo")).Click();
                driver.FindElement(By.Name("NumeroProtocolo")).Clear();
                driver.FindElement(By.Name("NumeroProtocolo")).SendKeys(_protocolo);
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();

                // Clica para realizar atendimento.
                driver.FindElement(By.Id("2")).Click(200);

                // Caso abra a modal de confirmação para realizar o atendimento (registro associado a outra pessoa), clica em não.
                var abriuModalConfirmarAlteracao = ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoNao);

                // Caso tenha aberto a modal e clicado em não, clica novamente para realizar o atendimeno e clica em sim.
                if (abriuModalConfirmarAlteracao)
                {
                    // Testa o botão sim do realizar atendimento.
                    driver.FindElement(By.Id("2")).Click(200);
                    ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoSim);
                }

                driver.FindElement(By.Id("proximaPagina")).Click();

                //Pesquisa Intercâmbio
                driver.FindElement(By.Id("SeqTermoIntercambio_botao_modal")).Click();
                driver.FindElement(By.Name("DescricaoParceria")).Clear();
                driver.FindElement(By.Name("DescricaoParceria")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoParceriaIntercambio"))).SelectByText("Acordo");
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Cotutela Incluir");
                driver.FindElement(By.Id("select_SeqTipoTermoIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTermoIntercambio"))).SelectByText("Cotutela");
                driver.FindElement(By.Id("select_SeqTipoTermoIntercambio")).Click();
                driver.FindElement(By.Id("SeqInstituicaoExterna_botao_modal")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("ABEU - CENTRO UNIVERSITÁRIO");
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqInstituicaoExterna")).Click(1000);
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click(1000);

                //FIX: Não tem id fixo no radiobutton do lookup.
                driver.FindElement(By.XPath(".//input[@type='radio'][@name='SelectedValues']")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqTermoIntercambio")).Click();

                //Seleciona Data início e Data fim.
                var primeiroDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFim")).Click();
                driver.FindElement(By.Name("DataFim")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));

                //Seleciona Orientação
                driver.FindElement(By.Id("select_SeqTipoOrientacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoOrientacao"))).SelectByText("Orientação em Intercâmbio");
                driver.FindElement(By.Id("select_SeqTipoOrientacao")).Click();

                //Inclui Participantes
                driver.FindElement(By.Id("Participantes_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Colaborador'])[2]/following::a[1]")).Click();
                driver.FindElement(By.XPath(".//*[normalize-space(text()) and normalize-space(.)='"+ _nomeProfessor +"']")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Participantes_0__TipoParticipacaoOrientacao"))).SelectByText("Orientador");
                driver.FindElement(By.Id("select_Participantes_0__TipoParticipacaoOrientacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Participantes_0__SeqInstituicaoExterna"))).SelectByText("PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS");
                driver.FindElement(By.Id("select_Participantes_0__SeqInstituicaoExterna")).Click();


                driver.FindElement(By.Id("proximaPagina")).Click();

                //Parecer do atendimento.
                driver.FindElement(By.Id("select_Situacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Situacao"))).SelectByText("Deferido");
                driver.FindElement(By.Id("select_Situacao")).Click();
                driver.FindElement(By.Name("Parecer")).Click();
                driver.FindElement(By.Name("Parecer")).Clear();
                driver.FindElement(By.Name("Parecer")).SendKeys("Teste QA Parecer");
                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("BotaoWinYes")).Click();

                //Checando a mensagem de Sucesso
                CheckSuccess();
            });
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Realizar Atedimento Solicitação Cotutela - Indeferimento")]

        public void RealizarAtendimentoCotutelaIndeferimento()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.Name("NumeroProtocolo")).Click();
                driver.FindElement(By.Name("NumeroProtocolo")).Clear();
                driver.FindElement(By.Name("NumeroProtocolo")).SendKeys(_protocolo);
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();

                // Clica para realizar atendimento.
                driver.FindElement(By.Id("2")).Click(200);

                // Caso abra a modal de confirmação para realizar o atendimento (registro associado a outra pessoa), clica em não.
                var abriuModalConfirmarAlteracao = ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoNao);

                // Caso tenha aberto a modal e clicado em não, clica novamente para realizar o atendimeno e clica em sim.
                if (abriuModalConfirmarAlteracao)
                {
                    // Testa o botão sim do realizar atendimento.
                    driver.FindElement(By.Id("2")).Click(200);
                    ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoSim);
                }

                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("proximaPagina")).Click();

                //Parecer do atendimento.
                driver.FindElement(By.Id("select_Situacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Situacao"))).SelectByText("Indeferido");
                driver.FindElement(By.Id("select_Situacao")).Click();
                driver.FindElement(By.Name("Parecer")).Click();
                driver.FindElement(By.Name("Parecer")).Clear();
                driver.FindElement(By.Name("Parecer")).SendKeys("Teste QA Parecer");
                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("BotaoWinYes")).Click();

                //Checando a mensagem de Sucesso
                CheckSuccess();
            });
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Realizar Atedimento Solicitação Sanduiche - Deferimento")]

        public void RealizarAtendimentoSanduicheDeferimento()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.Name("NumeroProtocolo")).Click();
                driver.FindElement(By.Name("NumeroProtocolo")).Clear();
                driver.FindElement(By.Name("NumeroProtocolo")).SendKeys(_protocolo);
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();

                // Clica para realizar atendimento.
                driver.FindElement(By.Id("2")).Click(200);

                // Caso abra a modal de confirmação para realizar o atendimento (registro associado a outra pessoa), clica em não.
                var abriuModalConfirmarAlteracao = ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoNao);

                // Caso tenha aberto a modal e clicado em não, clica novamente para realizar o atendimeno e clica em sim.
                if (abriuModalConfirmarAlteracao)
                {
                    // Testa o botão sim do realizar atendimento.
                    driver.FindElement(By.Id("2")).Click(200);
                    ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoSim);
                }

                driver.FindElement(By.Id("proximaPagina")).Click();

                //Pesquisa Intercâmbio
                driver.FindElement(By.Id("SeqTermoIntercambio_botao_modal")).Click();
                driver.FindElement(By.Name("DescricaoParceria")).Clear();
                driver.FindElement(By.Name("DescricaoParceria")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoParceriaIntercambio"))).SelectByText("Acordo");
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Sanduiche Incluir");
                driver.FindElement(By.Id("select_SeqTipoTermoIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTermoIntercambio"))).SelectByText("Sanduiche");
                driver.FindElement(By.Id("select_SeqTipoTermoIntercambio")).Click();
                driver.FindElement(By.Id("SeqInstituicaoExterna_botao_modal")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("ABEU - CENTRO UNIVERSITÁRIO");
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqInstituicaoExterna")).Click(1000);
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click(1000);

                //FIX: Não tem id fixo no radiobutton do lookup.
                driver.FindElement(By.XPath(".//input[@type='radio'][@name='SelectedValues']")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqTermoIntercambio")).Click();

                //Seleciona Orientação
                driver.FindElement(By.Id("select_SeqTipoOrientacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoOrientacao"))).SelectByText("Orientação em Intercâmbio");
                driver.FindElement(By.Id("select_SeqTipoOrientacao")).Click();

                //Inclui Participantes
                driver.FindElement(By.Id("Participantes_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Colaborador'])[2]/following::a[1]")).Click();
                driver.FindElement(By.XPath(".//*[normalize-space(text()) and normalize-space(.)='" + _nomeProfessor + "']")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Participantes_0__TipoParticipacaoOrientacao"))).SelectByText("Orientador");
                driver.FindElement(By.Id("select_Participantes_0__TipoParticipacaoOrientacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Participantes_0__SeqInstituicaoExterna"))).SelectByText("PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS");
                driver.FindElement(By.Id("select_Participantes_0__SeqInstituicaoExterna")).Click();


                driver.FindElement(By.Id("proximaPagina")).Click();

                //Parecer do atendimento.
                driver.FindElement(By.Id("select_Situacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Situacao"))).SelectByText("Deferido");
                driver.FindElement(By.Id("select_Situacao")).Click();
                driver.FindElement(By.Name("Parecer")).Click();
                driver.FindElement(By.Name("Parecer")).Clear();
                driver.FindElement(By.Name("Parecer")).SendKeys("Teste QA Parecer");
                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("BotaoWinYes")).Click();

                //Checando a mensagem de Sucesso
                CheckSuccess();
            });
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Realizar Atedimento Solicitação Sanduiche - Indeferimento")]

        public void RealizarAtendimentoSanduicheIndeferimento()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.Name("NumeroProtocolo")).Click();
                driver.FindElement(By.Name("NumeroProtocolo")).Clear();
                driver.FindElement(By.Name("NumeroProtocolo")).SendKeys(_protocolo);
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();

                // Clica para realizar atendimento.
                driver.FindElement(By.Id("2")).Click(200);

                // Caso abra a modal de confirmação para realizar o atendimento (registro associado a outra pessoa), clica em não.
                var abriuModalConfirmarAlteracao = ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoNao);

                // Caso tenha aberto a modal e clicado em não, clica novamente para realizar o atendimeno e clica em sim.
                if (abriuModalConfirmarAlteracao)
                {
                    // Testa o botão sim do realizar atendimento.
                    driver.FindElement(By.Id("2")).Click(200);
                    ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoSim);
                }

                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("proximaPagina")).Click();

                //Parecer do atendimento.
                driver.FindElement(By.Id("select_Situacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Situacao"))).SelectByText("Indeferido");
                driver.FindElement(By.Id("select_Situacao")).Click();
                driver.FindElement(By.Name("Parecer")).Click();
                driver.FindElement(By.Name("Parecer")).Clear();
                driver.FindElement(By.Name("Parecer")).SendKeys("Teste QA Parecer");
                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("BotaoWinYes")).Click();

                //Checando a mensagem de Sucesso
                CheckSuccess();
            });
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Realizar Atedimento Solicitação Cotutela - Deferimento com Pesquisa Professor")]

        public void RealizarAtendimentoCotutelaDeferimentoPesquisaProfessor()
        {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            //Pesquisa Colaborador
            var admSolicitacao = new SMC.Academico.Tests.ADM.DCT.Colaborador();
            (_nomeProfessor) = admSolicitacao.Pesquisar();

            //-------------------------------------------------------------------------------------------------
            //Realiza o atendimento da solicitação passando o colaborador pesquisado.
            RealizarAtendimentoCotutelaDeferimento();
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Realizar Atedimento sem Preencher Dados Termo Intercâmbio e Orientação")]

        public void RealizarAtendimentoSemTermoIntercambioOrientacao()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.Name("NumeroProtocolo")).Click();
                driver.FindElement(By.Name("NumeroProtocolo")).Clear();
                driver.FindElement(By.Name("NumeroProtocolo")).SendKeys(_protocolo);
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();

                // Clica para realizar atendimento.
                driver.FindElement(By.Id("2")).Click(200);

                // Caso abra a modal de confirmação para realizar o atendimento (registro associado a outra pessoa), clica em não.
                var abriuModalConfirmarAlteracao = ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoNao);

                // Caso tenha aberto a modal e clicado em não, clica novamente para realizar o atendimeno e clica em sim.
                if (abriuModalConfirmarAlteracao)
                {
                    // Testa o botão sim do realizar atendimento.
                    driver.FindElement(By.Id("2")).Click(200);
                    ClicaBotaoModalConfirmacaoRealizarAtendimento(driver, TipoBotao.BotaoSim);
                }

                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("proximaPagina")).Click();

                //Parecer do atendimento.
                driver.FindElement(By.Id("select_Situacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Situacao"))).SelectByText("Deferido");
                driver.FindElement(By.Id("select_Situacao")).Click();
                driver.FindElement(By.Name("Parecer")).Click();
                driver.FindElement(By.Name("Parecer")).Clear();
                driver.FindElement(By.Name("Parecer")).SendKeys("Teste QA Parecer");
                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("BotaoWinYes")).Click();

                //Verificar com Hugo, como validar exatamente a mensagem informada na documentação.
                //Checando a mensagem de Sucesso
                CheckError();
            });
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Realizar Atedimento Solicitação Sanduiche - Deferimento com Pesquisa Aluno")]
        public void RealizarAtendimentoSanduicheDeferimentoPesquisaProfessor()
        {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            //Pesquisa Colaborador
            var admSolicitacao = new SMC.Academico.Tests.ADM.DCT.Colaborador();
            (_nomeProfessor) = admSolicitacao.Pesquisar();

            //-------------------------------------------------------------------------------------------------
            //Realiza o atendimento da solicitação passando o colaborador pesquisado.
            RealizarAtendimentoSanduicheDeferimento();
        }


        private bool ClicaBotaoModalConfirmacaoRealizarAtendimento(ISMCWebDriver driver, TipoBotao tipoBotao)
        {
            try
            {
                SMCWebElement botaoPergunta;

                if (tipoBotao == TipoBotao.BotaoNao)
                    botaoPergunta = driver.FindElement(By.XPath(".//button[@id='BotaoPadraoPerguntaNao']"));
                else
                    botaoPergunta = driver.FindElement(By.XPath(".//button[@id='BotaoPadraoPerguntaSim']"));

                if (botaoPergunta.Element != null)
                {
                    botaoPergunta.Click();
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        //Fim dos scripts de teste de atendimento de Solicitaão de Intercâmbio.
        //-------------------------------------------------------------------------------------------------

        [Fact]
        [Trait("Cenário", "Administrativo - Validar Notificação.")]

        public void ValidarNotificacaoCancelamentoMatricula()
        //Deve ser usado no Encerramento de processo. Valida se no Encerramento de processo de Renovação de matrícula, enviou notificação do cancelamento de matrícula
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.Name("NumeroProtocolo")).Click();
                driver.FindElement(By.Name("NumeroProtocolo")).Clear();
                driver.FindElement(By.Name("NumeroProtocolo")).SendKeys("2018.006021");
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                driver.FindElement(By.Id("3")).Click();
                driver.FindElement(By.CssSelector("[data-index='2']")).Click();
                //Abrir a notificação do tipo "Aluno Cancelado - encerramento processo de renovação"
                driver.FindElement(By.Id("1")).Click();
                driver.FindElement(By.Id("Mensagem")).Click();
                //Tratar o tipo da mensagem - ela deve ser do tipo Aluno cancelado - encerramento processo de renovação
                


            });
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Pesquisa Solicitações de um aluno")]

        public void PesquisaSolicitacoesAluno()
            //Deve ser usado no Encerramento de processo de Renovação de matrícula. Pesquisa solicitações do aluno e valida se as solicitações foram excluídas.
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.Name("SeqPessoa_display_text")).Click();
                driver.FindElement(By.Name("SeqPessoa_display_text")).Clear();
                driver.FindElement(By.Name("SeqPessoa_display_text")).SendKeys("Adimilson Angelo Moura");
                Thread.Sleep(2000);
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                Thread.Sleep(2000);
                //Todas as solicitações do ciclo letivo que teve encerramento de processo de renovação de matrícula devem estar canceladas
            });
        }

        [Fact]
        [Trait("Cenário", "Administrativo - Pesquisa Solicitações de um aluno")]

        public void ValidaHistoricoEncerramentoProcessoRenovacaoMatricula() 
            //Deve ser usado no Encerramento de processo de Renovação de matrícula. Valida se inseriu histórico quando o processo foi encerrado
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Serviço'])[1]/following::a[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Prorrogação de prazo de conclusão de curso'])[1]/following::span[1]")).Click();
                driver.FindElement(By.Id("select_SeqProcessoEtapa")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqProcessoEtapa"))).SelectByText("3º Etapa - Efetivação da Matrícula");
                driver.FindElement(By.Id("select_SeqProcessoEtapa")).Click();
                driver.FindElement(By.Id("select_SeqSituacaoEtapa")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqSituacaoEtapa"))).SelectByText("Encerrado - Aluno desistente na solicitação");
                driver.FindElement(By.Id("select_SeqSituacaoEtapa")).Click();
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Visualizar'])[1]/i[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Identificação'])[1]/following::span[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Etapa'])[1]/following::p[42]")).Click();

                //Deve ter inserido uma situação do tipo "Encerrado - Aluno desistente na solicitação", com data início data do dia da execução
            });
        }
    
        private static void Login(ISMCWebDriver driver)
        {
            //-------------------------------------------------------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //Login
            driver.SMCLoginCpf();

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "SRC/SolicitacaoServico");
            //-------------------------------------------------------------------------------------------------
        }
    }
    */
}
