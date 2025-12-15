using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CUR //Arvore onde esta o arquivo
{

    public class GrupoConfiguracaoComponente : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_validacaocamposobrigatorios() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoGrupoConfiguracaoComponente"))).SelectByText("Selecionar...");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Validar campo obrigatório. Informar nome do campo e mensagem de obrigatoriedade. 
                driver.FindElement(By.Name("Descricao")).CheckErrorMessage("Preenchimento obrigatório");
                driver.FindElement(By.Name("TipoGrupoConfiguracaoComponente"));
                driver.FindElement(By.Name("Itens_0__SeqConfiguracaoComponente_display_text")).CheckErrorMessage("Preenchimento obrigatório");
                             
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_Configuracao_apenas_um_grupo() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente - Automação");

                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("50.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();
                
                driver.FindElement(By.Id("Itens_1__SeqConfiguracaoComponente_botao_modal")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("50.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Não é possível salvar este grupo, pois ele deve conter pelo menos duas configurações de componentes curriculares associadas.", teste);
                
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }


      

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_configura_carga_horaria_diferente() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo configuração");
                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                              
                driver.FindElement(By.Name("Codigo")).SendKeys("50.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.XPath("//button[@id='Itens_1__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("24.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();
                                
                //driver.FindElement(By.XPath("//button[@id='BotaoSalvarTemplate']/i")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Outra forma de validar a mensagem exibida(pode substituir a checagem de mensagem de sucesso e a checagem de mensagem apresentada:
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Não é possível salvar este grupo. Todas as configurações de um grupo devem pertencer a componentes de mesma carga horária.", teste);

                Thread.Sleep(1500);

                      
        //Para fechar o Chrome em segundo plano
        driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_Configuracao_divisoes_equivalentes() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente - Automação");

                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("3279.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("Itens_1__SeqConfiguracaoComponente_botao_modal")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("3263.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Não é possível salvar este grupo. Todas as configurações de um grupo devem possuir divisões equivalentes.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }
                     
        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_Configuracao_componente_organizacao_diferente() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                //Chamar script de inclusão de componente que possui organização
                var componentecomorganizacao = new SMC.Academico.Tests.ADM.CSO.ComponenteCurricular();
                componentecomorganizacao.ComponenteCurricular_Inclusao_ComponentecomOrganizacao();
                
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Componente com tipo de organização - automação");
                             
                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("seminário de tese - automação");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();


                driver.FindElement(By.Id("Itens_1__SeqConfiguracaoComponente_botao_modal")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("148.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Não é possível salvar este grupo. Todas as configurações de um grupo devem possuir os mesmos tópicos com as mesmas cargas horárias.", teste);

                var componentecomorganizacaoexclusao = new SMC.Academico.Tests.ADM.CSO.ComponenteCurricular();
                componentecomorganizacao.ComponenteCurricular_Exclusao_ComponentecomOrganizacao();


                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }
        //daqui para baixo é regra 5

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_Configuracao_componente_mesmo_curriculo() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente - Automação");

                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("50.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("Itens_1__SeqConfiguracaoComponente_botao_modal")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("3383.3");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Não é possível salvar este grupo. As configurações de um grupo não podem pertencer a componentes de um mesmo currículo.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_Configuracao_existente_em_outro_grupo() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente - Automação");

                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("1232.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("Itens_1__SeqConfiguracaoComponente_botao_modal")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("3839.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Não é possível salvar este grupo, pois este contém configuração que já está em uso em outro grupo.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }
        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_configura_exige_componente_com_assunto() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                //Script para Alterar o componente 286 para não exigir assunto
                var componentesemassunto = new SMC.Academico.Tests.ADM.CSO.ComponenteCurricular();
                componentesemassunto.ComponenteCurricular_Alteracao();
                

                //Inserindo componente que exige assunto e outro que não exige
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo configuração automação");
                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();

                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("285.1");
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                //driver.FindElement(By.Id("5bc286a0-bcff-40f9-8fb8-6176c1a18727")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("Itens_1__SeqConfiguracaoComponente_botao_modal")).Click();

                //driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("286.1");
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                //driver.FindElement(By.Id("5bc286a0-bcff-40f9-8fb8-6176c1a18727")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();
                                
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Outra forma de validar a mensagem exibida(pode substituir a checagem de mensagem de sucesso e a checagem de mensagem apresentada:
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Não é possível salvar este grupo. Todas as configurações devem ser de componentes que exigem assunto.", teste);

                Thread.Sleep(1500);


                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Inclusao_Configuracao_Valida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//div[@id='Ativo']/div/div[2]/label/input")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente Válido - Automação");

                driver.FindElement(By.XPath("//button[@id='Itens_0__SeqConfiguracaoComponente_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("576.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("Itens_1__SeqConfiguracaoComponente_botao_modal")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("1128.1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqConfiguracaoComponente")).Click();

                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Grupo de configuração de componente incluído com sucesso.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Alteracao_Configuracao_Valida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente Válido - Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                                
                driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente Válido Alterado - Automação");
                driver.FindElement(By.Name("Ativo")).Click();
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                        
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Grupo de configuração de componente alterado com sucesso.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoConfiguracaoComponente_Exclusao_Configuracao_Valida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente Válido Alterado - Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                /* driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();
                 driver.FindElement(By.Name("Descricao")).SendKeys("Grupo Configuração Componente Válido Alterado - Automação");
                 driver.FindElement(By.Name("Ativo")).Click();*/

                driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //driver.FindElement(By.CssSelector("[title='Excluir'][data-type='smc-button']")).Click();
                //driver.FindElement(By.CssSelector("[title='Sim'][type='button']")).Click();


                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Grupo de configuração de componente excluído com sucesso.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }


        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            GrupoConfiguracaoComponente_Inclusao_validacaocamposobrigatorios(); 
            GrupoConfiguracaoComponente_Inclusao_Configuracao_apenas_um_grupo(); //regra 1
            GrupoConfiguracaoComponente_Inclusao_configura_carga_horaria_diferente(); //regra 2
            GrupoConfiguracaoComponente_Inclusao_Configuracao_divisoes_equivalentes(); //regra 3
            GrupoConfiguracaoComponente_Inclusao_Configuracao_componente_organizacao_diferente();//regra 4 
            GrupoConfiguracaoComponente_Inclusao_Configuracao_componente_mesmo_curriculo(); //regra 5
            GrupoConfiguracaoComponente_Inclusao_Configuracao_existente_em_outro_grupo(); //regra 6
            GrupoConfiguracaoComponente_Inclusao_configura_exige_componente_com_assunto(); //regra 7
            //regra 8 - está gerando erro na exclusão do grupo de configuração
            GrupoConfiguracaoComponente_Inclusao_Configuracao_Valida();
            GrupoConfiguracaoComponente_Alteracao_Configuracao_Valida();
            //GrupoConfiguracaoComponente_Exclusao_Configuracao_Valida();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso
            //driver.GoToUrl(Consts.SERVIDOR_DESENVOLVIMENTO_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CUR/GrupoConfiguracaoComponente"); //coloca o resto do endereço para acessar a pagina do teste
            //driver.GoToUrl2(Consts.SERVIDOR_DESENVOLVIMENTO_ADM + "CUR/GrupoConfiguracaoComponente"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}
