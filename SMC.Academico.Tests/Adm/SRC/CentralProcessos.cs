using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Academico.Tests.ALUNO.ALN;
using SMC.Framework.Test;
using System.Threading;
using Xunit;
using System;

namespace SMC.Academico.Tests.ADM.SRC
{
    public class CentralProcessos : SMCSeleniumUnitTest
    {
        
        // private bool phantomJS = false;



        [Fact]
        public void Central_Processos_1_InserirProcesso()
        {
            //var dataatual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            //var fimperiodo = new DateTime(DateTime.Today.Year, DateTime.Today.Month+1, 1);
            base.ExecuteTest((driver) =>
            {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            Login(driver);
            driver.FindElement(By.Id("BotaoNovoVP")).Click();
            driver.FindElement(By.Name("Descricao")).Clear();
            driver.FindElement(By.Name("Descricao")).SendKeys("PROCESSO TESTE");
            driver.FindElement(By.Id("select_SeqTipoServico")).Click();

            new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoServico"))).SelectByText("Alterações no plano de estudos");
            driver.FindElement(By.Id("select_SeqTipoServico")).Click();
            driver.FindElement(By.Id("select_SeqServico")).Click();
            Thread.Sleep(1600);
            new SMCSelectElement(driver.FindElement(By.Id("select_SeqServico"))).SelectByText("Alteração de plano de estudo");
            driver.FindElement(By.Id("select_SeqServico")).Click();
            /*driver.FindElement(By.Name("ValorPercentualServicoAdicional")).Click();
            driver.FindElement(By.Name("ValorPercentualServicoAdicional")).Clear();
            driver.FindElement(By.Name("ValorPercentualServicoAdicional")).SendKeys("99,99");*/
            driver.FindElement(By.Id("SeqCicloLetivo_botao_modal")).Click();
            driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
            driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
            driver.FindElement(By.Id("smc-dataselector-SeqCicloLetivo")).Click();
            driver.FindElement(By.Name("DataInicio")).Click();
            driver.FindElement(By.Name("DataInicio")).Clear();
            driver.FindElement(By.Name("DataInicio")).SendKeys("04122024");
            driver.FindElement(By.Name("DataInicio")).Click();
            //driver.FindElement(By.Name("DataInicio")).SendKeys(dataatual.ToString("dd/MM/yyyy"));

            driver.FindElement(By.Name("DataFim")).Clear();
            driver.FindElement(By.Name("DataFim")).SendKeys("31072028");
            driver.FindElement(By.Name("DataFim")).Click();
            //driver.FindElement(By.Name("DataFim")).SendKeys(fimperiodo.ToString("dd/MM/yyyy"));
            driver.FindElement(By.Id("EntidadesResponsaveis_botao_modal")).Click();
            driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
            driver.FindElement(By.Id("smc-dataselector-EntidadesResponsaveis")).Click();
            driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
            
            //Checando mensagem de sucesso
            //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

            //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

            //Compara a mensagem exibida com a esperada:
                Assert.Equal("Processo incluído com sucesso.", teste);
                driver.Driver.Close();



            });
        }
        [Fact]
        public void Central_Processos_2_EditarProcesso()
        {
            //var dataatual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            //var fimperiodo = new DateTime(DateTime.Today.Year, DateTime.Today.Month+2, 1);

            base.ExecuteTest((driver) =>
            {
                Login(driver);
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("PROCESSO TESTE");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
              /*  driver.FindElement(By.Name("Descricao")).SendKeys("");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
               //Carrega a mensagem exibida na tela para a variavel msgerro
                var msgerro = driver.FindElement(By.Id("parsley-id-37")).GetValue();
                //verifica a mensagem exibida na tela
                Assert.False(CheckMessage(msgerro), "Mensagem esperada não exibida");*/
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("NOVO PROCESSO DE TESTES2");
                driver.FindElement(By.Id("SeqCicloLetivo_botao_modal")).Click();
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCicloLetivo")).Click();
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).Clear();
                //driver.FindElement(By.Name("DataInicio")).SendKeys(DateTime.ToString("dd/MM/yyyy"));
                var Hoje = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, System.DateTime.Today.Day);
                driver.FindElement(By.Name("DataInicio")).SendKeys(Hoje.ToString("dd/MM/yyyy"));
               // driver.FindElement(By.Name("DataInicio")).SendKeys("05122020");
                driver.FindElement(By.Name("DataFim")).Clear();

                var primeiroDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(2).AddDays(-1);
                driver.FindElement(By.Name("DataFim")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
              //  driver.FindElement(By.Name("DataFim")).SendKeys("31122020");
                driver.FindElement(By.Id("EntidadesResponsaveis_botao_modal")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector1")).Click();
                driver.FindElement(By.Id("smc-dataselector-EntidadesResponsaveis")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //verifica mensagem de sucesso
                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                
                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Processo alterado com sucesso.", teste);
                driver.Driver.Close();


            });

        }
        [Fact]
        public void Central_Processos_3_ExcluirProcesso()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("NOVO PROCESSO DE TESTES2");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaNao")).Click();
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                
                //verifica se a mensagem exibida eh a esperada
                 Assert.True(CheckMessage("Processo excluído com sucesso."), "Mensagem esperada não exibida");
                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();

            });

        }
        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            Central_Processos_1_InserirProcesso();
            Central_Processos_2_EditarProcesso();
            Central_Processos_3_ExcluirProcesso();

        }


        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //driver.GoToUrl(Consts.SERVIDOR_DESENVOLVIMENTO_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/SRC/Processo");
            //driver.GoToUrl2(Consts.SERVIDOR_DESENVOLVIMENTO_ADM + "/SRC/Processo");

        }


    }
}


/*SCRIPT DE TESTE ANTIGO
namespace SMC.Academico.Tests.ADM.SRC
{
    public class CentralProcessos : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        

        [Fact]
        public void EncerrarProcessoIndisponivel()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("select_SeqTipoServico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoServico"))).SelectByText("Alterações no vínculo acadêmico");
                driver.FindElement(By.Id("select_SeqTipoServico")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[1]/following::i[1]")).Click();

                //Validando se o botão Encerrar processo não apareceu
                var resultado = driver.Exist(By.Id("DynamicBotao_5"));
                Assert.False(resultado);
                
                //Checando a mensagem de Sucesso
                //CheckMessage("");

            });
        }
        
        [Fact]
        public void EncerrarProcessoDisponivel()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("select_SeqTipoServico")).Click();
                //new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoServico"))).SelectByText("Renovação de Matrícula");
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoServico"))).SelectByText("Matrícula de Reabertura");
                driver.FindElement(By.Id("select_SeqTipoServico")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[1]/following::i[1]")).Click();
                // driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Encerrar processo'])[1]/following::a[1]")).Click();
                driver.FindElement(By.Id("DynamicBotao_6")).Click(); 
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                Thread.Sleep(3000);
                Assert.True(CheckSuccess(), "Mensagem de sucesso não exibida");
                //Validando se o botão Encerrar processo apareceu
                //var resultado = driver.Exist(By.Id("DynamicBotao_5"));    //elemento foi retornado vazio
                   
               // Thread.Sleep(3000);
                //Assert.True(resultado);
                //Thread.Sleep(3000);

                //Checando a mensagem de Sucesso
                //CheckMessage("");

            });
        }


        [Fact]
        public void EncerrarProcessoEncerrado() //Processo já está encerrado, valida apenas mensagem.
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Esse script está aguardando a criação de método para identificar processo já encerrado de forma automática. 
                //Hoje está utilizando um processo que já estava encerrado, mas que pode não estar quando voltar um backup.

                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoServico"))).SelectByText("Renovação de Matrícula");
                driver.FindElement(By.Id("select_SeqTipoServico")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[6]/following::i[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Encerrar processo'])[6]/following::a[1]")).Click();

                //Validando se o botão Encerrar processo apareceu inativo e se a mensagem foi exibida
                //var resultado = driver.Exist(By.Id("DynamicBotao_5"));    //elemento foi retornado vazio
                //Assert.True(resultado);

                //Checando a mensagem de Opção indisponível pois o processo já está encerrado              
                
                var toolTipText = driver.FindElement(By.CssSelector("span[class='smc-instrucoes-preenchimento-mensagem']")).GetValue();
                Assert.False(CheckMessage(toolTipText), "Mensagem esperada nao exibida");

                });
        }


        [Fact]
        public void EncerrandoEtapaProcesso() //Processo com etapas não encerradas, valida apenas mensagem.
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Esse script está aguardando a criação de método para identificar processo já encerrado de forma automática. 
                //Hoje está passando ID de um processo que não estava encerrado, mas que pode não estar quando voltar um backup.

                driver.FindElement(By.Name("Seq")).Click();
                driver.FindElement(By.Name("Seq")).Clear();
                driver.FindElement(By.Name("Seq")).SendKeys("310"); //Toda vez que roda tem que trocar o id do processo
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Para processo que possui 3 etapas
                //driver.FindElement(By.Id("ButtonSetDropdown2")).Click();
                //driver.FindElement(By.Id("3")).Click();
                //driver.FindElement(By.Id("BotaoWinYes")).Click();
                //driver.FindElement(By.Id("BotaoWinNo")).Click();

                //driver.FindElement(By.Id("ButtonSetDropdown3")).Click();
                //driver.FindElement(By.Id("7")).Click();
                //driver.FindElement(By.Id("BotaoWinYes")).Click();
                //driver.FindElement(By.Id("BotaoWinNo")).Click();

                //Etapa3
                driver.FindElement(By.Id("ButtonSetDropdown4")).Click();
                Thread.Sleep(1500);
                driver.FindElement(By.Id("11")).Click();
                //driver.FindElement(By.Id("BotaoWinYes")).Click();
                driver.FindElement(By.Id("BotaoWinNo")).Click();


                //Validando se as etapas foram encerradas
                //select para identificar se está encerrada ou não:
                //select * from src.processo_etapa where idt_dom_situacao_etapa =4 --4 Encerrada, 3 Em manutenção, 2 Liberada, 1 Aguardando liberação


                //Checando a mensagem de Processo já encerrado
                //CheckMessage("Opção indisponível. Para o encerramento do processo é necessário que todas as etapas sejam previamente encerradas.", 1500);


            });
        }

        [Fact]
        public void EncerrandoProcesso() //Processo com etapas encerradas, valida apenas mensagem.
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Esse script está aguardando a criação de método para identificar processo já encerrado de forma automática. 
                //Hoje está passando ID de um processo que não estava encerrado, mas que pode não estar quando voltar um backup.
                                              
                driver.FindElement(By.Name("Seq")).Click();
                driver.FindElement(By.Name("Seq")).Clear();
                driver.FindElement(By.Name("Seq")).SendKeys("390"); //Toda vez que roda tem que trocar o id do processo
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("ButtonSetDropdown4")).Click();
                driver.FindElement(By.Id("DynamicBotao_5")).Click();
                //driver.FindElement(By.Id("BotaoWinYes")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaNao")).Click();

               
               


            });
        }



        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoEncerrarProcesso()
        {
            EncerrarProcessoIndisponivel();   //falta filtro por situação do processo (método a ser criado) e validação de mensagem
            EncerrarProcessoEncerrado();      //falta filtro por situação do processo (método a ser criado) e validação de mensagem
            //Chama script que identifica situações futuras para validar se foram excluídas após encerramento da etapa
            var ValidaSituacaofutura = new SMC.Academico.Tests.ADM.ALN.Aluno();
            ValidaSituacaofutura.IdentificaSituacoesFuturas();
            //Encerra etapas pendentes, pois processo não encerra se houver etapa pendente
            //EncerrarProcessoEtapaspendentes();//falta filtro por situação do processo (método a ser criado) e validação de mensagem
            //Encerra processo que está com etapas encerradas
            EncerrarProcessoDisponivel();     //falta filtro por situação do processo (método a ser criado) e validação de mensagem
            //Chama script para verificar se no encerramento da etapa, houve cancelamento de matrícula e a notificação foi enviada
            var ValidaNotificacao = new SolicitacaoServico();
            ValidaNotificacao.ValidarNotificacaoCancelamentoMatricula(); //falta validar se a notificação é de cancelamento de matrícula
            //Chama script para pesquisar as solicitações do aluno que teve cancelamento de matrícula e verificar se elas foram encerradas
            var ValidaSolicitaoesEncerradas = new SolicitacaoServico();
            ValidaSolicitaoesEncerradas.PesquisaSolicitacoesAluno(); //falta validar se as solicitações estão com o estado encerrado
            //Chama script para pesquisar o benefício do aluno que teve cancelamento de matrícula e verificar se foi cancelado, tem que continuar deferido
            var SituacaoBeneficio = new SMC.Academico.Tests.ADM.ALN.Aluno(); 
            SituacaoBeneficio.ValidaBeneficioDeferido();
            //Chama script para validar o histórico do aluno
            var HistoricoAluno = new SMC.Academico.Tests.ADM.ALN.Aluno();
            HistoricoAluno.ValidaAtualizacaoHistorico();
            
            //Chama script para conferir situações futuras
            //Sugestão: armazenar os dados do  IdentificaSituacoesFuturas e aqui executar o script novamente e comparar os resultados 

            //Chama script para validar se o histórico foi atualizado
            var HistoricoSituacaoSolicitacao = new SolicitacaoServico();
            HistoricoSituacaoSolicitacao.ValidaHistoricoEncerramentoProcessoRenovacaoMatricula();
                                       
        }*/
