using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaService : SMCServiceBase, IPessoaService
    {
        #region [ DomainServices ]

        private PessoaDomainService PessoaDomainService
        {
            get { return this.Create<PessoaDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        #endregion [ DomainServices ]

        #region Servicos Externos

        public SMC.Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService PessoaCADService => Create<SMC.Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService>();

        #endregion

        /// <summary>
        /// Busca uma lista de pessoas com seus dados pessoais
        /// </summary>
        /// <param name="filtro">Filtros da listagem de pessoas</param>
        /// <returns>Lista não paginada de pessoas</returns>
        /// <exception cref="PessoaAtuacaoDuplicadaException">Caso o tipo de atuação seja colaborador e o usuário já tenha uma atuação de colaborador cadastrada</exception>
        public List<PessoaExistenteListaData> BuscarPessoaExistente(PessoaFiltroData filtro)
        {
            return PessoaDomainService.BuscarPessoaExistente(filtro.Transform<PessoaFiltroVO>()).TransformList<PessoaExistenteListaData>();
        }

        /// <summary>
        /// Busca uma pessoa com os dados pessoais
        /// </summary>
        /// <param name="filter">Filtros a serem usados</param>
        /// <returns>Dados da pessoa</returns>
        public PessoaData BuscarPessoa(PessoaFiltroData filter)
        {
            var spec = filter.Transform<PessoaFilterSpecification>();
            return this.PessoaDomainService.SearchByKey(spec, IncludesPessoa.DadosPessoais_ArquivoFoto | IncludesPessoa.Filiacao)
                .Transform<PessoaData>();
        }

        /// <summary>
        /// Busca pessoas com os dados pessoais
        /// </summary>
        /// <param name="filter">Filtros a serem usados</param>
        /// <returns>Dados das pessoas</returns>
        public List<PessoaData> BuscarPessoas(PessoaFiltroData filter)
        {
            var spec = filter.Transform<PessoaFilterSpecification>();
            return this.PessoaDomainService.SearchBySpecification(spec, IncludesPessoa.DadosPessoais_ArquivoFoto | IncludesPessoa.Filiacao)
                .TransformList<PessoaData>();
        }

        /// <summary>
        /// Busca uma pessoa com os dados pessoais
        /// </summary>
        /// <param name="seqPessoa">Seq da pessoa</param>
        /// <returns>Dados da pessoa</returns>
        public PessoaData BuscarPessoaLookup(long seqPessoa)
        {
            return this.PessoaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Pessoa>(seqPessoa),
                p => new PessoaData()
                {
                    Seq = p.Seq,
                    Cpf = p.Cpf,
                    NumeroPassaporte = p.NumeroPassaporte,
                    Nome = p.DadosPessoais.OrderByDescending(o => o.Seq).FirstOrDefault().Nome,
                    NomeSocial = p.DadosPessoais.OrderByDescending(o => o.Seq).FirstOrDefault().NomeSocial,
                });
        }

        /// <summary>
        /// Busca pessoas com dados pessoais
        /// </summary>
        /// <param name="filter">Filtro a ser usado</param>
        /// <returns>Dados das pessoas</returns>
        public SMCPagerData<PessoaData> BuscarPessoasLookup(PessoaFiltroData filter)
        {
            var spec = filter.Transform<PessoaFilterSpecification>();

            spec.SetOrderBy(o => o.DadosPessoais.FirstOrDefault().Nome);

            int total = 0;

            var result = this.PessoaDomainService.SearchProjectionBySpecification(spec,
                p => new PessoaData()
                {
                    Seq = p.Seq,
                    Cpf = p.Cpf,
                    NumeroPassaporte = p.NumeroPassaporte,
                    Nome = p.DadosPessoais.OrderByDescending(o => o.Seq).FirstOrDefault().Nome,
                    NomeSocial = p.DadosPessoais.OrderByDescending(o => o.Seq).FirstOrDefault().NomeSocial,
                }, out total).ToList();

            return new SMCPagerData<PessoaData>(result, total);
        }

        /// <summary>
        /// Grava os dados de uma pessoa com suas dependências
        /// </summary>
        /// <param name="pessoa">Pessoa a ser gravada</param>
        /// <returns>Sequencial da pessoa gravada</returns>
        public long SalvarPessoa(PessoaData pessoa)
        {
            return this.PessoaDomainService.SalvarPessoa(pessoa.Transform<Pessoa>());
        }

        /// <summary>
        /// Valida as quantidades de validação conforme a configuração da instituição logada
        /// </summary>
        /// <param name="pessoa">Dados pessoais da pessoa a ser gravada</param>
        /// <param name="tipoAtuacao">Tipo da atuação</param>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        /// <exception cref="PessoaDadosPesosaisQuantidadeFiliacaoMaximaException">Caso tenha mais que a quantidade máxima de filiação configurada para o tipo de atuação</exception>
        /// <exception cref="PessoaDadosPesosaisQuantidadeFiliacaoMinimaException">Caso não tenha a quantidade mínima de filiação obrigatória configurada para o tipo de atuação</exception>
        public void ValidarQuantidadesFiliacao(PessoaData pessoa, TipoAtuacao tipoAtuacao)
        {
            this.PessoaDomainService.ValidarQuantidadesFiliacao(pessoa.Transform<Pessoa>(), tipoAtuacao);
        }

        /// <summary>
        /// Buscar o código de pessoa do CAD associado ao ingressante. (A pessoa do CAD está nos Dados Mestres)
        /// </summary>
        /// <param name="seqPessoa">Sequencial do pessoa</param>
        /// <param name="tipoPessoa">Tipo pessoa física ou jurídica</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Código de pessoa do CAD</returns>
        public long BuscarCodigoDePessoaNosDadosMestres(long seqPessoa, TipoPessoa tipoPessoa, long seqPessoaAtuacao)
        {
            return PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(seqPessoa, tipoPessoa, seqPessoaAtuacao);
        }

        /// <summary>
        /// Retorna todas as instituições de ensino que uma pessoa faz parte
        /// </summary>
        /// <param name="filter">Filtro a ser aplicado para busca das pessoas</param>
        /// <returns>Lista de instituições de ensino</returns>
        public List<SMCDatasourceItem> BuscarInstituicoesEnsinoPessoaSelect(PessoaFiltroData filter)
        {
            return PessoaDomainService.BuscarInstituicoesEnsinoPessoaSelect(filter.Transform<PessoaFilterSpecification>());
        }

        /// <summary>
        /// Método para buscar o webmail da pessoa
        /// </summary>
        /// <param name="codigoPessoa">Código de pessoa para recuperar o webmail</param>
        /// <returns>Webmail encontrado ou NULL</returns>
        public string BuscarWebmail(int codigoPessoa)
        {
            string emailPessoa = null;

            var webmails = PessoaCADService.BuscarWebmail(codigoPessoa);
            if (webmails != null)
            {
                foreach (var email in webmails.Split(','))
                {
                    if (email.EndsWith(TERMINACAO_EMAIL.PUCMINAS))
                        emailPessoa = email;
                }
            }

            return emailPessoa;
        }

        /// <summary>
        /// Retorna a lista de credenciais de acesso de uma pessoa para uso no MOBILE
        /// 
        /// Obs.: Foi criado tudo com texto fixo. No futuro o melhor seria ter um cadastro para isso
        /// até para facilitar manutenções.
        /// Não foi utilizado o resource que a página utiliza.
        /// 
        /// </summary>
        /// <param name="codPessoaCAD">Código de pessoa no CAD</param>
        /// <returns>Lista de credenciais de acesso da pessoa</returns>
        public List<CredenciaisAcessoData> BuscarCredenciaisAcessoPessoa(int codPessoaCAD)
        {
            // Recupera o e-mail de acesso do @sga.pucminas.br
            var emailPessoa = BuscarWebmail(codPessoaCAD);

            List<CredenciaisAcessoData> retorno = new List<CredenciaisAcessoData>();

            // Inclui no retorno o item "Qual minha credencial de acesso?"
            var qualMinhaCredencial = new CredenciaisAcessoData()
            {
                Ordem = 1,
                Titulo = "Qual minha credencial de acesso?",
                Descricao = "A credencial de acesso é chamada de “Código de Pessoa”. A Universidade concede uma a cada usuário no momento do vínculo, destinando esta para a pessoa por tempo indeterminado. Ainda que, ao concluir um curso venha a fazer outro em momento oportuno, o código será o mesmo.",
                TextoCredencial = string.Format("Seu código de pessoa é: {0}", codPessoaCAD),

            };
            retorno.Add(qualMinhaCredencial);

            // Inclui no retorno o item "PUC Mail", caso exista um criado para o usuário
            if (!string.IsNullOrEmpty(emailPessoa))
            {
                var acessoPUCMail = new CredenciaisAcessoBotaoData { Label = "Acessar", Link = "https://mail.google.com/a/sga.pucminas.br" };
                var pucMail = new CredenciaisAcessoData()
                {
                    Ordem = 2,
                    Titulo = "PUC Mail",
                    Descricao = "O PUC Mail é uma parceria entre a PUC Minas e o Google, é uma maneira segura de garantir o recebimento dos comunicados da PUC Minas e as informações necessárias para acesso aos sistemas institucionais. É importante que faça o primeiro acesso para a troca de senha. Se desejar, pode ser configurado o encaminhamento automático das mensagens para outro e-mail.",
                    TextoCredencial = string.Format("Login: {0}", emailPessoa),
                    Botoes = new CredenciaisAcessoBotaoData[] { acessoPUCMail },
                    TrocaSenhaTelaInterna = true,
                };
                retorno.Add(pucMail);
            }

            //As propriedades ImagemCredencial estão hospedadas no sharepoint e são gerenciadas pela equipe de design.
            //Elas são usadas na tela de credenciais do novo aplicativo do pucmobile.
            //Caso seja necessário alterações nessas, será necessário contactar os designers para a realização da alteração.

            // Inclui no retorno o item "Office 365 / Teams"
            var acessoOffice365 = new CredenciaisAcessoBotaoData { Label = "Acessar", Link = "https://login.microsoftonline.com/?whr=sga.pucminas.br" };
            var manualOffice365 = new CredenciaisAcessoBotaoData { Label = "Orientações de acesso", Link = "http://portal.pucminas.br/documentos/gti/office365.pdf" };
            var manualTeams = new CredenciaisAcessoBotaoData { Label = "Manual de uso do Teams", Link = "https://smcgtipucminas.sharepoint.com/:b:/s/gtitie/Ef28TPwwAmFFiEgp4X7E6cIBYF6tBuBCvTba493KGu0T5g?e=jdXmpOa&download=1" };
            var office365 = new CredenciaisAcessoData()
            {
                Ordem = 3,
                Titulo = "Office 365 / Teams",
                Descricao = "Tenha acesso ao Office 365 Educacional, que inclui pacote Office Online, OneNote, armazenamento no OneDrive, Microsoft Teams, além de outras ferramentas para a sala de aula. Após troca de senha, aguarde 15 minutos para realizarmos a sincronização na nuvem.",
                TextoCredencial = string.Format("Login: {0}{1}", codPessoaCAD, TERMINACAO_EMAIL.PUCMINAS),
                Botoes = new CredenciaisAcessoBotaoData[] { acessoOffice365 },
                BotoesDeManual = new CredenciaisAcessoBotaoData[] { manualOffice365, manualTeams },
                SuporteTecnico = true,
                LinkTrocaSenha = "https://icei.pucminas.br/sgl/",
                MensagemTrocaSenha = "As aplicações Canvas, Office 365, Rede Wifi – Eduroam, Portal CAPES, e Laboratórios acadêmicos, compartilham a mesma senha. Em caso de troca de senha, os demais sistemas também serão afetados.",
              
            };
            retorno.Add(office365);

            // Incluir no retorno o item "Canvas"
            var acessoCanvas = new CredenciaisAcessoBotaoData { Label = "Acessar", Link = "https://pucminas.instructure.com/login/microsoft" };
            var canvas = new CredenciaisAcessoData()
            {
                Ordem = 4,
                Titulo = "Canvas",
                Descricao = "O Canvas é o Ambiente Virtual de Aprendizagem adotado pela PUC Minas para realização de suas atividades acadêmicas. Ele facilita o uso, a criação e o compartilhamento de conteúdo por professores e alunos. A plataforma também disponibiliza o acesso a todo material dos seus cursos através do aplicativo em tablets e/ou smartphones. Assim você pode acompanhar suas aulas a qualquer momento e local. Clique no botão “Acessar“ acima para abrir o Canvas. O acesso ao Canvas é realizado com as credenciais do Office 365. Na tela de login, clique em “Fazer login com Microsoft“, utilize o login indicado e a senha cadastrada para o Office 365. Para cadastrar uma nova senha, clique no botão “Trocar senha”.",
                TextoCredencial = string.Format("Login Microsoft: {0}{1}", codPessoaCAD, TERMINACAO_EMAIL.PUCMINAS),
                Botoes = new CredenciaisAcessoBotaoData[] { acessoCanvas },
                SuporteTecnico = true,
                LinkTrocaSenha = "https://icei.pucminas.br/sgl/",
                MensagemTrocaSenha = "As aplicações Canvas, Office 365, Rede Wifi – Eduroam, Portal CAPES, e Laboratórios acadêmicos, compartilham a mesma senha. Em caso de troca de senha, os demais sistemas também serão afetados.",
                
            };
            retorno.Add(canvas);

            // Inclui no retorno o item "Rede Wifi"
            var manualRedeWifi = new CredenciaisAcessoBotaoData { Label = "Manual do produto", Link = "https://pucminas.br/wifi" };
            var wifi = new CredenciaisAcessoData()
            {
                Ordem = 5,
                Titulo = "Rede Wifi",
                Descricao = "A PUC Minas disponibiliza acesso aos alunos e professores em duas redes sem fio, eduroam e PUCMinas, que compartilham a mesma infraestrutura. A rede PUCMinas atende somente a comunidade interna e a rede eduroam (education roaming), alem de atender a comunidade interna, permite acesso também para pesquisadores e estudantes de outras instituições, nacionais e internacionais, que façam parte desse projeto. A senha para acesso é a mesma utilizada nos laboratórios da Universidade. É possível realizar troca de senha clicando no link ”trocar senha”.",
                TextoCredencial = string.Format("Login: {0}@pucminas.br", codPessoaCAD),
                BotoesDeManual = new CredenciaisAcessoBotaoData[] { manualRedeWifi },
                LinkTrocaSenha = "https://icei.pucminas.br/sgl/",
                MensagemTrocaSenha = "As aplicações Canvas, Office 365, Rede Wifi – Eduroam, Portal CAPES, e Laboratórios acadêmicos, compartilham a mesma senha. Em caso de troca de senha, os demais sistemas também serão afetados.",
                
            };
            retorno.Add(wifi);

            // Incluir no retorno o item "Portal CAPES"
            var manualCapes = new CredenciaisAcessoBotaoData { Label = "Manual do produto", Link = "http://portal.pucminas.br/documentos/gti/cafe.pdf" };
            var portalCapes = new CredenciaisAcessoData()
            { 
                Ordem = 6,
                Titulo = "Portal CAPES",
                Descricao = "O Portal de Periódicos, da Coordenação de Aperfeiçoamento de Pessoal de Nível Superior (Capes), é uma biblioteca virtual que reúne e disponibiliza a instituições de ensino e pesquisa no Brasil o melhor da produção científica internacional. A senha para acesso é a mesma utilizada nos laboratórios da Universidade. É possível realizar troca de senha clicando no link “trocar senha”.",
                TextoCredencial = string.Format("Login: {0}", codPessoaCAD),
                BotoesDeManual = new CredenciaisAcessoBotaoData[] { manualCapes },
                LinkTrocaSenha = "https://icei.pucminas.br/sgl/",
                MensagemTrocaSenha = "As aplicações Canvas, Office 365, Rede Wifi – Eduroam, Portal CAPES, e Laboratórios acadêmicos, compartilham a mesma senha. Em caso de troca de senha, os demais sistemas também serão afetados.",
              
            };
            retorno.Add(portalCapes);

            // Incluir no retorno item "Laboratórios acadêmicos"
            var laboratorio = new CredenciaisAcessoData()
            { 
                Ordem = 7,
                Titulo = "Laboratórios acadêmicos",
                Descricao = "Esta credencial permite o acesso aos laboratórios físicos de todas as unidades da PUC Minas. Para acessar os computadores remotamente utilize a conexão VPN. Para obter maiores informações e consultar os computadores disponíveis acesse: www.icei.pucminas.br/acessoremoto",
                TextoCredencial = string.Format("Login: {0}", codPessoaCAD),
                LinkTrocaSenha = "https://icei.pucminas.br/sgl/",
                MensagemTrocaSenha = "As aplicações Canvas, Office 365, Rede Wifi – Eduroam, Portal CAPES, e Laboratórios acadêmicos, compartilham a mesma senha. Em caso de troca de senha, os demais sistemas também serão afetados.",
                
            };
            retorno.Add(laboratorio);

            // Incluir no retorno o item "Microsoft Azure Dev Tools For Teaching"
            var acessoAzure = new CredenciaisAcessoBotaoData { Label = "Acessar", Link = "https://portal.azure.com/#home" };
            var manualAzure = new CredenciaisAcessoBotaoData { Label = "Manual do produto", Link = "http://portal.pucminas.br/documentos/gti/imagine.pdf" };
            var azure = new CredenciaisAcessoData()
            {
                Ordem = 8,
                Titulo = "Microsoft Azure Dev Tools For Teaching",
                Descricao = "Em parceria com a Microsoft, a PUC Minas disponibiliza a todos os seus alunos e professores acesso gratuito a diversos softwares para uso em seus computadores pessoais.",
                TextoCredencial = "Login: Usuário de e-mail@sga.pucminas.br. Em caso de problemas acesse: http://icei.pucminas.br/microsoft-azure",
                Botoes = new CredenciaisAcessoBotaoData[] { acessoAzure },
                BotoesDeManual = new CredenciaisAcessoBotaoData[] { manualAzure },
            };
            retorno.Add(azure);

            return retorno;
        }
    }
}