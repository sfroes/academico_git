using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.CUR
{
    public class TipoComponenteCurricularXInstituicaoeNivelEnsino : SMCSeleniumUnitTest
    {
        [Fact]
        public void TipoComponenteCurricularXInstituicaoeNivelEnsino_1_InserirSucesso()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //depende do script de inclusão de tipo de componente curricular - Chamada do script para inclusão
                var tipoComponente = new TipoComponenteCurricular();
                tipoComponente.TipoComponenteCurricular_1_Inserir();

                //Início do processo de inserção
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqInstituicaoNivel")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqInstituicaoNivel"))).SelectByText("Doutorado Acadêmico");
                driver.FindElement(By.Id("select_SeqInstituicaoNivel")).Click();
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoComponenteCurricular"))).SelectByText("Teste");
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Tipo de componente curricular'])[1]/following::i[2]")).Click();
                       
                driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoDivisaoComponente"))).SelectByText("TESTE");
                driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoDivisaoComponente")).Click();
                driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoTrabalho"))).SelectByText("Tese");
                driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoTrabalho")).Click();
                driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoEventoAgd")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoEventoAgd"))).SelectByText("Defesa");
                driver.FindElement(By.Id("select_TiposDivisaoComponente_0__SeqTipoEventoAgd")).Click();
                driver.FindElement(By.CssSelector("[name='TiposDivisaoComponente[0].PermiteCargaHorariaGrade']")).Click();
                driver.FindElement(By.CssSelector("[name='TiposDivisaoComponente[0].PermiteCargaHorariaGrade'][title='Não']")).Click();
                driver.FindElement(By.Name("EntidadeResponsavelObrigatoria")).Click();
                driver.FindElement(By.Name("QuantidadeMaximaEntidadeResponsavel")).Click();
                driver.FindElement(By.Name("QuantidadeMaximaEntidadeResponsavel")).Clear();
                driver.FindElement(By.Name("QuantidadeMaximaEntidadeResponsavel")).SendKeys("1");
                driver.FindElement(By.Id("select_TipoEntidadeResponsavel")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoEntidadeResponsavel"))).SelectByText("Curso x Unidade");
                driver.FindElement(By.Id("select_TipoEntidadeResponsavel")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                                                                                                                                                
                driver.FindElement(By.Name("ExibeCargaHoraria")).Click();
                driver.FindElement(By.Name("ExibeCredito")).Click();
                driver.FindElement(By.Name("PermiteEmenta")).Click();
                driver.FindElement(By.Name("QuantidadeMinimaCaracteresEmenta")).Click();
                driver.FindElement(By.Name("QuantidadeMinimaCaracteresEmenta")).Clear();
                driver.FindElement(By.Name("QuantidadeMinimaCaracteresEmenta")).SendKeys("2");
                driver.FindElement(By.Name("ExigeCargaHoraria")).Click();
                driver.FindElement(By.Name("ExigeCredito")).Click();
                driver.FindElement(By.Name("FormatoCargaHoraria")).Click();
                driver.FindElement(By.Name("QuantidadeHorasPorCredito")).Click();
                driver.FindElement(By.Name("QuantidadeHorasPorCredito")).Clear();
                driver.FindElement(By.Name("QuantidadeHorasPorCredito")).SendKeys("2");
                driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();
                driver.FindElement(By.Name("QuantidadeMinimaCaracteresEmenta")).Clear();
                driver.FindElement(By.Name("QuantidadeMinimaCaracteresEmenta")).SendKeys("20");
                driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });
        }


        [Fact]
        public void TipoComponenteCurricularXInstituicaoeNivelEnsino_2_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);

                driver.FindElement(By.Id("select_SeqInstituicaoNivel")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqInstituicaoNivel"))).SelectByText("Doutorado Acadêmico");
                driver.FindElement(By.Id("select_SeqInstituicaoNivel")).Click();
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click(1000);
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoComponenteCurricular"))).SelectByText("Teste");
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click(500);
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click(500);
                driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click(500);
                                               
                driver.FindElement(By.Id("TiposDivisaoComponente_DetailBotaoExcluirElemento0")).Click(500);
                driver.FindElement(By.Id("TiposDivisaoComponente_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoDivisaoComponente"))).SelectByText("TESTE");
                driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoDivisaoComponente")).Click();
                driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoTrabalho"))).SelectByText("Tese");
                driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoEventoAgd"))).SelectByText("Defesa");
                driver.FindElement(By.Id("select_TiposDivisaoComponente_1__SeqTipoEventoAgd")).Click();
                driver.FindElement(By.CssSelector("[name='TiposDivisaoComponente[1].PermiteCargaHorariaGrade']")).Click();
                driver.FindElement(By.CssSelector("[name='TiposDivisaoComponente[1].PermiteCargaHorariaGrade'][title='Sim']")).Click();
                driver.FindElement(By.Name("EntidadeResponsavelObrigatoria")).Click();
                driver.FindElement(By.Name("QuantidadeMaximaEntidadeResponsavel")).Click();
                driver.FindElement(By.Name("QuantidadeMaximaEntidadeResponsavel")).Clear();
                driver.FindElement(By.Name("QuantidadeMaximaEntidadeResponsavel")).SendKeys("01");
                driver.FindElement(By.Id("select_TipoEntidadeResponsavel")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoEntidadeResponsavel"))).SelectByText("Curso x Oferta x Localidade");
                driver.FindElement(By.Id("select_TipoEntidadeResponsavel")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Tipo Divisão e Entidade'])[1]/following::span[1]")).Click();
                driver.FindElement(By.Id("PARAMETRO")).Click();
                //driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Não'])[2]/input[1]")).Click();
                driver.FindElement(By.Id("PermiteConfiguracaoComponente")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Não'])[4]/input[1]")).Click();
                driver.FindElement(By.Name("PermiteSubdivisaoOrganizacao")).Click();
                driver.FindElement(By.Name("SiglaObrigatoria")).Click();
                driver.FindElement(By.Name("CriterioAprovacaoObrigatorio")).Click();
                driver.FindElement(By.Name("ExibeCargaHoraria")).Click();
                driver.FindElement(By.CssSelector("[name='ExibeCargaHoraria'][title='Sim']")).Click();
                driver.FindElement(By.Name("PermiteAssuntoComponente")).Click();
                driver.FindElement(By.Name("NomeReduzidoObrigatorio")).Click();
                driver.FindElement(By.Name("QuantidadeVagasPrevistasObrigatorio")).Click();
                driver.FindElement(By.Id("select_SeqCriterioAprovacaoPadrao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqCriterioAprovacaoPadrao"))).SelectByText("Aprovado/reprovado");
                driver.FindElement(By.Id("select_SeqCriterioAprovacaoPadrao")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Não'])[12]/input[1]")).Click();
                driver.FindElement(By.Name("PermiteEmenta")).Click();
                driver.FindElement(By.Name("QuantidadeMinimaCaracteresEmenta")).Clear();
                driver.FindElement(By.Name("QuantidadeMinimaCaracteresEmenta")).SendKeys("30");
                driver.FindElement(By.Name("ExigeCargaHoraria")).Click();
                driver.FindElement(By.Id("ExibeCredito")).Click();
                driver.FindElement(By.Id("ExigeCredito")).Click();
                driver.FindElement(By.Name("FormatoCargaHoraria")).Click();
                driver.FindElement(By.Name("QuantidadeHorasPorCredito")).Click();
                driver.FindElement(By.Name("QuantidadeHorasPorCredito")).Clear();
                driver.FindElement(By.Name("QuantidadeHorasPorCredito")).SendKeys("16");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                

                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoComponenteCurricularXInstituicaoeNivelEnsino_3_ExcluirNaoConfirmaExclusao()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);

                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoComponenteCurricular"))).SelectByText("Teste");
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaNao")).Click();
                driver.Driver.Close();


            });
        }
        [Fact]
        public void TipoComponenteCurricularXInstituicaoeNivelEnsino_4_ExcluirConfirmaExclusao()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);

                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoComponenteCurricular"))).SelectByText("Teste");
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                               
                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Exclusão do tipo de componente curricular criado no início do script de inclusão com sucesso
                //Necessário chamar primeiro o script de alteração 
                var tipoComponenteAlteracao = new TipoComponenteCurricular();
                    tipoComponenteAlteracao.TipoComponenteCurricular_2_Alterar();

                  //Chamada do script para exclusão
                    var tipoComponenteExclusao = new TipoComponenteCurricular();
                    tipoComponenteExclusao.TipoComponenteCurricular_3_Excluir();
                    driver.Driver.Close();

            });
        }


        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoComponenteCurricularXInstituicaoeNivelEnsino_1_InserirSucesso();
            TipoComponenteCurricularXInstituicaoeNivelEnsino_2_Alterar();
            TipoComponenteCurricularXInstituicaoeNivelEnsino_3_ExcluirNaoConfirmaExclusao();
            TipoComponenteCurricularXInstituicaoeNivelEnsino_4_ExcluirConfirmaExclusao();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/CUR//InstituicaoNivelTipoComponenteCurricular");
            //----------------------------------------------------
        }

    }
}




