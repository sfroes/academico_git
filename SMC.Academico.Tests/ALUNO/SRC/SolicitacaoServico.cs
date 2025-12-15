using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace SMC.Academico.Tests.ALUNO.SRC
{

    public class SolicitacaoServicoIntercambio : SMCSeleniumUnitTest
    {
        // O upload de arquivo na linha 69 não está funcionando
        //Informar baixo e-mail e vínculo do aluno que deseja criar a solicitação.
        //private string _email = "abdiasjjunior@gmail.com";
        private string _email = "jonyh.jo@gmail.com";

        //private string _dadosVinculo = "Mestrado em Ciências da Religião - PUC Minas Coração Eucarístico - Livre - 1/2019";
        private string _dadosVinculo = "Mestrado em Letras - PUC Minas Coração Eucarístico - Livre - 2/2022";

        [Fact]
        [Trait("Cenário", "Aluno - Inserir Solicitação Serviço")]

        public string Inserir()
        {
            string protocolo = string.Empty;

            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver, _email);

                //Verificar abertura de modal para seleção de Vículo do aluno.
                SelecionarVinculo(driver);

                driver.FindElement(By.Id("BotaoNovaSolicitacao")).Click(500);
                driver.FindElement(By.Id("select_SeqTipoServico")).Click(500);
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoServico"))).SelectByText("Alterações no vínculo acadêmico");
                driver.FindElement(By.Id("select_SeqTipoServico")).Click();
                driver.FindElement(By.Name("SeqProcesso")).Click();
                new SMCSelectElement(driver.FindElement(By.Name("SeqProcesso"))).SelectByText("Processo de solicitação de intercâmbio");
                driver.FindElement(By.Name("SeqProcesso")).Click();
                driver.FindElement(By.Id("BotaoProxSocilitacao")).Click();
                driver.FindElement(By.Id("select_SeqJustificativa")).Click();
                driver.FindElement(By.Id("select_SeqJustificativa")).Click();
                driver.FindElement(By.Name("ObservacoesJustificativa")).Click();
                driver.FindElement(By.Name("ObservacoesJustificativa")).Clear();
                driver.FindElement(By.Name("ObservacoesJustificativa")).SendKeys("Teste QA Observações");
                driver.FindElement(By.Id("proximaPagina")).Click();
                driver.FindElement(By.Id("select_Campo_20393")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Campo_20393"))).SelectByText("Cotutela");
                driver.FindElement(By.Id("select_Campo_20393")).Click();
                driver.FindElement(By.Name("Campo_20394")).Click(800);
                driver.FindElement(By.Name("Campo_20394")).SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("Campo_20396")).Click(500);
                var ultimoDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
                driver.FindElement(By.Name("Campo_20396")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("Campo_20395")).Click();
                driver.FindElement(By.Name("Campo_20395")).Clear();
                driver.FindElement(By.Name("Campo_20395")).SendKeys("Teste QA Orientador");
                driver.FindElement(By.Name("Campo_20397")).Click();
                driver.FindElement(By.Name("Campo_20397")).Clear();
                driver.FindElement(By.Name("Campo_20397")).SendKeys("Teste QA Instituição");
                driver.FindElement(By.Id("proximaPagina")).Click(1000);
                //Uploud de aquivo.
                driver.FindElement(By.XPath("//*[@id='opcionais']/div[1]/div/button")).Click(2000);
                driver.FindElement(By.XPath("//*[@id='Documentos_0__Documentos_0__ArquivoAnexado']/div[1]/input")).SendKeys(@"\\GTIWEBDES01\Files\pdf.pdf");
                Thread.Sleep(2500);
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                //Clique duplo no botão salvar para sair da janela criada que mostra o arquivo anexo.
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                driver.FindElement(By.Id("proximaPagina")).Click(1200);
                driver.FindElement(By.Id("proximaPagina")).Click(1800);
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click(1000);
                protocolo = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Protocolo'])[1]/following::p[1]")).GetValue();
                driver.FindElement(By.Id("sairProcesso")).Click();

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

            return (protocolo);
        }

        [Fact]
        [Trait("Cenário", "Aluno - Inserir Solicitação Serviço com Pesquisa Aluno")]

        //Pesquisa aluno (Matriculado em Curso Regular) no sga administrativo, loga no SGA Aluno e cria a solicitação.
        public string ADMAlunoPesquisarInserir()
        {
            string protocolo = string.Empty;

            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                //Pesquisar Aluno
                var admSolicitacao = new SMC.Academico.Tests.ADM.ALN.Aluno();
                (_email, _, _, _, _dadosVinculo, _, _) = admSolicitacao.Pesquisar();
                //-------------------------------------------------------------------------------------------------

                Inserir();

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

            return (protocolo);
        }

        //Verificar abertura de modal para seleção de Vículo do aluno.
        private void SelecionarVinculo(ISMCWebDriver driver)
        {
            //Via javascript verifica se o componente existe
            var buttonExiste = driver.ExistJS("button[id='btnSelecionarAluno']");

            //Verificando se existe pra depois realmente procurar o elemento
            if (buttonExiste.HasValue && buttonExiste.Value)
            {
                var botaoSelecionarVinculo = driver.FindElement(By.XPath(".//button[@id='btnSelecionarAluno']"));
                if (botaoSelecionarVinculo.Element != null)
                {
                    driver.FindElement(By.Id("select_SeqInstituicaoEnsino")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_SeqInstituicaoEnsino"))).SelectByText("Pontifícia Universidade Católica de Minas Gerais");
                    driver.FindElement(By.Id("select_SeqInstituicaoEnsino")).Click();
                    driver.FindElement(By.Id("select_SeqAluno")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_SeqAluno"))).SelectByText(_dadosVinculo);
                    driver.FindElement(By.Id("select_SeqAluno")).Click();
                    botaoSelecionarVinculo.Click();

                    //Para fechar o Chrome em segundo plano
                    driver.Driver.Close();

                }
            }
        }

        private static void Login(ISMCWebDriver driver, string email = "hugobrito@pucminas.br")
        {
            //-------------------------------------------------------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ALUNO);
            //Login
            driver.SMCLoginEmail(email);

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ALUNO + "");
            //-------------------------------------------------------------------------------------------------
        }


    }
}
