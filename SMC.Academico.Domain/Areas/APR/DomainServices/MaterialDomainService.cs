using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Models;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class MaterialDomainService : AcademicoContextDomain<Material>
    {
        #region [ DomainServices ]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();

        private OrigemMaterialDomainService OrigemMaterialDomainService
        {
            get { return Create<OrigemMaterialDomainService>(); }
        }

        private DivisaoTurmaDomainService DivisaoTurmaDomainService
        {
            get { return Create<DivisaoTurmaDomainService>(); }
        }

        private EntidadeDomainService EntidadeDomainService
        {
            get { return Create<EntidadeDomainService>(); }
        }

        private OrientacaoDomainService OrientacaoDomainService
        {
            get { return Create<OrientacaoDomainService>(); }
        }

        private MensagemDomainService MensagemDomainService
        {
            get { return Create<MensagemDomainService>(); }
        }

        private CursoDomainService CursoDomainService
        {
            get { return Create<CursoDomainService>(); }
        }

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return Create<HierarquiaEntidadeDomainService>(); }
        }

        #endregion [ DomainServices ]

        /// <summary>
        /// Caso a origem em questão (divisão de turma, orientação ou entidade) já possua o sequencial da origem de material preenchido,
        /// o link que irá chamar essa tela irá passar como parâmetro apenas o seq_origem_material. Caso contrário (seq_origem_material não preenchido),
        /// informar os outros dois parâmetros com o tipo de origem de material e o sequencial da origem(divisão de turma, orientação ou entidade),
        /// que permitirá o cadastro de uma origem de material
        /// </summary>
        /// <param name="filtro">Dados para realizar a busca no banco de dados.</param>
        /// <returns>Lista de materiais para montar a treeview.</returns>
        public List<Material> ListarMateriais(MaterialFilterSpecification filtro)
        {
            if (filtro.SeqOrigemMaterial == 0 && (filtro.TipoOrigemMaterial == TipoOrigemMaterial.Nenhum || filtro.SeqOrigem == 0))
            {
                throw new ListarException();
            }

            try
            {
                if (filtro.SeqOrigem.GetValueOrDefault() > 0 && filtro.TipoOrigemMaterial.GetValueOrDefault() != TipoOrigemMaterial.Nenhum && filtro.SeqOrigemMaterial.GetValueOrDefault() == 0)
                {
                    filtro.SeqOrigemMaterial = ObterOrigemMaterial(filtro.TipoOrigemMaterial.GetValueOrDefault(), filtro.SeqOrigem.GetValueOrDefault(), filtro.DescricaoOrigem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao buscar a origem do material: {0}", ex.Message));
            }

            //Buscar os materiais para formar a TreeView.
            return SearchBySpecification(filtro, a => a.OrigemMaterial, a => a.ArquivoAnexado).ToList();
        }

        public long SalvarMaterial(MaterialVO materialVO)
        {
            if (materialVO.SeqOrigemMaterial == 0)
            {
                materialVO.SeqOrigemMaterial = ObterOrigemMaterial(materialVO.TipoOrigemMaterial.GetValueOrDefault(), materialVO.SeqOrigem.GetValueOrDefault(), materialVO.DescricaoOrigem);
            }

            //Validando o arquivo anexado
            if (materialVO.TipoMaterial == TipoMaterial.Arquivo)
            {
                if (materialVO.ArquivoAnexado != null)
                {
                    string extensao = Path.GetExtension(materialVO.ArquivoAnexado.Name);
                    if (materialVO.ArquivoAnexado != null && materialVO.ArquivoAnexado.Size > VALIDACAO_ARQUIVO_ANEXADO.TAMANHO_MAXIMO_10MB)
                    {
                        throw new TamanhoMaximoException();
                    }

                    if (string.IsNullOrEmpty(extensao) || VALIDACAO_ARQUIVO_ANEXADO.EXTENSOES_ARQUIVOS_EXECUTAVEIS.Contains(extensao))
                    {
                        throw new TipoArquivoException(materialVO.ArquivoAnexado.Name);
                    }
                }
                else if (materialVO.SeqArquivoAnexado.GetValueOrDefault() == 0)
                {
                    throw new ArquivoNaoAnexadoException();
                }
            }
            else
            {
                materialVO.SeqArquivoAnexado = null;
            }

            //Validando se tem material com mesmo nome na mesma hierarquia.
            //Não é permitida a inclusão de material com a mesma descrição no mesmo nível da hierarquia.
            //Em caso de violação, abortar a operação e exibir a mensagem:
            //"<Inclusão | Alteração> não permitida. Nome da pasta / material já existente".
            MaterialFilterSpecification filtroDuplicado = new MaterialFilterSpecification();
            filtroDuplicado.DescricaoExata = materialVO.Descricao;
            filtroDuplicado.SeqOrigemMaterial = materialVO.SeqOrigemMaterial;
            filtroDuplicado.SeqSuperior = materialVO.SeqSuperior;

            var materialDuplicado = SearchBySpecification(filtroDuplicado).ToList();
            if (materialDuplicado != null && materialDuplicado.Count == 1 && materialDuplicado.FirstOrDefault().Seq != materialVO.Seq)
            {
                string tipo = materialVO.Seq == 0 ? "Inclusão" : "Alteração";
                throw new MaterialExistenteException(tipo, materialVO.Descricao);
            }

            /// Comentado por causa do Bug 30939
            ////Se o tipo de material for pasta, remove as visualizações.
            //if (materialVO.TipoMaterial == TipoMaterial.Pasta && materialVO.Visualizacoes != null && materialVO.Visualizacoes.Count > 0)
            //{
            //	materialVO.Visualizacoes = new List<MaterialVisualizacaoVO>();
            //}

            //Adicionando o 'http://' caso o link não começe com este endereço e não seja um endereço de FTP.
            if (materialVO.TipoMaterial == TipoMaterial.Link && !materialVO.UrlLink.StartsWith("ftp://") && !materialVO.UrlLink.StartsWith("http://") && !materialVO.UrlLink.StartsWith("https://"))
            {
                materialVO.UrlLink = "http://" + materialVO.UrlLink;
            }

            using (var unit = SMCUnitOfWork.Begin())
            {
                //Enviar mensagem aos alunos da divisão de turma em questão.
                if (materialVO.TipoMaterial != TipoMaterial.Pasta && materialVO.TipoOrigemMaterial == TipoOrigemMaterial.DivisaoTurma)
                {
                    if (materialVO.SeqOrigem.GetValueOrDefault() == 0)
                    {
                        DivisaoTurmaFilterSpecification specDT = new DivisaoTurmaFilterSpecification
                        {
                            SeqOrigemMaterial = materialVO.SeqOrigemMaterial
                        };
                        var divisaoTurma = DivisaoTurmaDomainService.SearchByKey(specDT);

                        if (divisaoTurma == null)
                        {
                            // Verifica se os dados enviados pelo usuário estão corretos.
                            throw new OrigemMaterialNaoEncontradoException();
                        }

                        materialVO.SeqOrigem = divisaoTurma.Seq;
                    }

                    MensagemDomainService.EnviarMensagemMaterialDidatico(materialVO.SeqOrigem.Value, materialVO.Descricao, materialVO.Seq == 0);
                }

                Material material = materialVO.Transform<Material>();
                EnsureFileIntegrity(material, m => m.SeqArquivoAnexado, m => m.ArquivoAnexado);
                SaveEntity(material);

                unit.Commit();

                return material.Seq;
            }
        }

        private long ObterOrigemMaterial(TipoOrigemMaterial tipoOrigemMaterial, long seqOrigem, string descricaoOrigem)
        {
            OrigemMaterial o = new OrigemMaterial();

            //Procurar o sequencial do OrigemMaterial na respectiva entidade ANTES de criar um novo OrigemMaterial e associar ao Material
            switch (tipoOrigemMaterial)
            {
                case TipoOrigemMaterial.DivisaoTurma:
                    var divisao = DivisaoTurmaDomainService.SearchByKey(new SMCSeqSpecification<DivisaoTurma>(seqOrigem));
                    if (divisao.SeqOrigemMaterial.GetValueOrDefault() > 0)
                    {
                        o = OrigemMaterialDomainService.SearchByKey(new SMCSeqSpecification<OrigemMaterial>(divisao.SeqOrigemMaterial.GetValueOrDefault()));
                    }
                    break;

                case TipoOrigemMaterial.Entidade:
                    var entidade = EntidadeDomainService.SearchByKey(new SMCSeqSpecification<Entidade>(seqOrigem));
                    if (entidade.SeqOrigemMaterial.GetValueOrDefault() > 0)
                    {
                        o = OrigemMaterialDomainService.SearchByKey(new SMCSeqSpecification<OrigemMaterial>(entidade.SeqOrigemMaterial.GetValueOrDefault()));
                    }
                    break;

                case TipoOrigemMaterial.Orientacao:
                    var orientacao = OrientacaoDomainService.SearchByKey(new SMCSeqSpecification<Orientacao>(seqOrigem));
                    if (orientacao.SeqOrigemMaterial.GetValueOrDefault() > 0)
                    {
                        o = OrigemMaterialDomainService.SearchByKey(new SMCSeqSpecification<OrigemMaterial>(orientacao.SeqOrigemMaterial.GetValueOrDefault()));
                    }
                    break;

                default:
                    break;
            }

            //Vai entrar aqui se a origem não foi encontrada
            if (o.Seq == 0)
            {
                //Cadastrar uma nova origem com base no Tipo Origem do Material e no sequencial da respectiva entidade.
                o.TipoOrigemMaterial = tipoOrigemMaterial;
                o.Descricao = "Descrição temporária";
                OrigemMaterialDomainService.SaveEntity(o);

                //Atualiza a entidade correspondente, com o seq da origem do material.
                switch (tipoOrigemMaterial)
                {
                    case TipoOrigemMaterial.DivisaoTurma:
                        DivisaoTurma d = DivisaoTurmaDomainService.SearchByKey(new SMCSeqSpecification<DivisaoTurma>(seqOrigem));
                        d.SeqOrigemMaterial = o.Seq;
                        var cabecalho = DivisaoTurmaDomainService.BuscarDivisaoTurmaCabecalho(seqOrigem);
                        o.Descricao = cabecalho.TurmaCodigo + "." + cabecalho.TurmaNumero + "." + cabecalho.NumeroGrupo + "." + cabecalho.DescricaoConfiguracaoComponente + ": " + cabecalho.TipoDivisaoDescricao + "-" + cabecalho.CargaHoraria + "-" + descricaoOrigem;
                        DivisaoTurmaDomainService.UpdateFields(d, a => a.SeqOrigemMaterial);
                        break;

                    case TipoOrigemMaterial.Entidade:
                        Entidade e = EntidadeDomainService.SearchByKey(new SMCSeqSpecification<Entidade>(seqOrigem));
                        e.SeqOrigemMaterial = o.Seq;
                        o.Descricao = e.Nome;
                        EntidadeDomainService.UpdateFields(e, a => a.SeqOrigemMaterial);
                        break;

                    case TipoOrigemMaterial.Orientacao:
                        Orientacao orientacao = OrientacaoDomainService.SearchByKey(new SMCSeqSpecification<Orientacao>(seqOrigem));
                        orientacao.SeqOrigemMaterial = o.Seq;
                        o.Descricao = descricaoOrigem;
                        OrientacaoDomainService.UpdateFields(orientacao, a => a.SeqOrigemMaterial);
                        break;

                    default:
                        break;
                }
                OrigemMaterialDomainService.UpdateEntity(o);
            }
            return o.Seq;
        }

        /// <summary>
        /// - Se tipo de atuação for "Aluno":
        /// buscar o curso do aluno logado e listar todas as origens de material das entidades superiores a este
        /// curso nas hierarquias da visão Gestão Organizacional e Visão Localidade.
        ///
        /// - Se tipo de atuação for "Professor":
        /// buscar todos os cursos que o professor logado está associado e listar todas as origens de material das
        /// entidades superiores a estes cursos nas hierarquias da "Visão Gestão Organizacional" e "Visão
        /// Localidade".
        ///
        /// - Se tipo de atuação não for informado (SGAAdministrativo):
        /// buscar as entidades permitidas através de filtro de dados para a pessoa logada e listar todas as origens
        /// de material destas entidades e das entidades superiores nas hierarquias da visão Gestão
        /// Organizacional e Visão Localidade.
        /// </summary>
        /// <param name="parametros">Parâmetros para busca dos materiais didáticos</param>
        /// <returns>Array de materiais para montar a treeview.</returns>
        public Dictionary<long, MaterialVO[]> ListarMateriaisParaDownload(DownloadMaterialVO parametros)
        {
            if (parametros.TipoOrigemMaterial == TipoOrigemMaterial.Nenhum)
            {
                throw new TipoOrigemMaterialException();
            }

            if ((parametros.TipoOrigemMaterial == TipoOrigemMaterial.DivisaoTurma || parametros.TipoOrigemMaterial == TipoOrigemMaterial.Orientacao) && (string.IsNullOrEmpty(parametros.SeqsOrigemMaterial)))
            {
                throw new SeqOrigemMaterialException();
            }

            Dictionary<long, MaterialVO[]> materiais = new Dictionary<long, MaterialVO[]>();
            List<long> seqsOrigens = string.IsNullOrEmpty(parametros.SeqsOrigemMaterial) ? new List<long>() : parametros.SeqsOrigemMaterial.Split(',').Select(Int64.Parse).ToList();

            // Listar as pastas e suas subpastas e materiais (link ou arquivo) disponíveis para as origens informadas como
            // parâmetro ou de acordo com a RN_APR_032 - Origens para download de material
            if (seqsOrigens.Count == 0)
            {
                switch (parametros.TipoAtuacao.GetValueOrDefault())
                {
                    case TipoAtuacao.Aluno:
                        parametros.SeqNivelEnsino = NivelEnsinoDomainService.BuscarNivelEnsinoAluno(parametros.SeqAluno.GetValueOrDefault()).Seq;
                        seqsOrigens.AddRange(OrigensMaterialPorAluno(parametros.SeqAluno.Value));
                        break;

                    case TipoAtuacao.Colaborador:
                        seqsOrigens.AddRange(OrigensMaterialPorProfessor(parametros.SeqColaborador.Value));
                        parametros.SeqsNiveisEnsino = NivelEnsinoDomainService.BuscarNiveisEnsinoProfessor(parametros.SeqColaborador.GetValueOrDefault());
                        break;

                    default:
                        seqsOrigens.AddRange(OrigensMaterialPorEntidades(parametros.SeqInstituicaoEnsino.Value));
                        break;
                }
            }

            foreach (var origem in seqsOrigens)
            {
                MaterialFilterSpecification specOrigens = parametros.Transform<MaterialFilterSpecification>();
                specOrigens.SeqOrigemMaterial = origem;
                //var resultadoBusca = SearchBySpecification(specOrigens, a => a.OrigemMaterial, a => a.ArquivoAnexado).ToList();

                var resultadoBusca = SearchProjectionBySpecification(specOrigens, x => new MaterialVO
                {
                    ArquivoAnexado = x.SeqArquivoAnexado.HasValue ? new SMCUploadFile
                    {
                        GuidFile = x.ArquivoAnexado.UidArquivo.ToString(),
                        Name = x.ArquivoAnexado.Nome,
                        Size = x.ArquivoAnexado.Tamanho,
                        Type = x.ArquivoAnexado.Tipo
                    } : null,
                    DataAlteracao = x.DataAlteracao,
                    DataInclusao = x.DataInclusao,
                    Descricao = x.Descricao,
                    DescricaoOrigem = x.OrigemMaterial.Descricao,
                    Observacao = x.Observacao,
                    Seq = x.Seq,
                    SeqArquivoAnexado = x.SeqArquivoAnexado,
                    UidArquivoAnexado = x.SeqArquivoAnexado.HasValue ? x.ArquivoAnexado.UidArquivo : null,
                    SeqOrigemMaterial = x.SeqOrigemMaterial,
                    SeqSuperior = x.SeqSuperior,
                    TipoMaterial = x.TipoMaterial,
                    TipoOrigemMaterial = x.OrigemMaterial.TipoOrigemMaterial,
                    UrlLink = x.UrlLink
                }).ToArray();

                if (resultadoBusca.Length > 0)
                    materiais.Add(origem, resultadoBusca);
            }

            return materiais;
        }

        /// <summary>
        /// Buscaa as entidades permitidas através de filtro de dados para a pessoa logada e lista todas as origens de material destas
        /// entidades e das entidades superiores nas hierarquias da visão Gestão Organizacional e Visão Localidade.
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade.</param>
        /// <returns>Lista de sequenciais de OrigemMaterial.</returns>
        private List<long> OrigensMaterialPorEntidades(long seqInstituicaoEnsino)
        {
            //Buscando as entidades permitidas do usuário logado.
            List<SMCDatasourceItem> e = EntidadeDomainService.BuscarEntidadesSelect(string.Empty, seqInstituicaoEnsino);

            //Buscando as entidades superiores
            List<SMCDatasourceItem> listaEntidades = new List<SMCDatasourceItem>();
            listaEntidades.AddRange(HierarquiaEntidadeDomainService.BuscarEntidadesSuperioresSelect(e.Select(a => a.Seq).ToList(), TipoVisao.VisaoOrganizacional));
            listaEntidades.AddRange(HierarquiaEntidadeDomainService.BuscarEntidadesSuperioresSelect(e.Select(a => a.Seq).ToList(), TipoVisao.VisaoLocalidades));
            listaEntidades.AddRange(e);

            //Buscando e retornando os sequenciais das origens dos materiais de cada entidade encontrada.
            EntidadeOrigemMaterialSpecification specEntidade = new EntidadeOrigemMaterialSpecification();
            specEntidade.SeqsEntidades = listaEntidades.Select(a => a.Seq).ToList();
            if (!specEntidade.SeqsEntidades.Contains(seqInstituicaoEnsino))
                specEntidade.SeqsEntidades.Add(seqInstituicaoEnsino);

            return EntidadeDomainService.SearchProjectionBySpecification(specEntidade, a => a.SeqOrigemMaterial).Where(a => a != null).Select(a => a.Value).ToList();
        }

        /// <summary>
        /// Busca todos os cursos que o professor logado está associado e lista todas as origens de material das entidades superiores a
        /// estes cursos nas hierarquias da "Visão Gestão Organizacional" e "Visão Localidade".
        /// </summary>
        /// <param name="seqColaborador">Sequencial do professor</param>
        /// <returns>Lista de sequenciais de OrigemMaterial.</returns>
        private List<long> OrigensMaterialPorProfessor(long seqColaborador)
        {
            //Buscando os cursos dos professor
            List<Curso> c = CursoDomainService.BuscarCursosAssociadosAoProfessor(seqColaborador);

            //Buscando as entidades superiores
            List<SMCDatasourceItem> listaEntidades = new List<SMCDatasourceItem>();
            listaEntidades.AddRange(HierarquiaEntidadeDomainService.BuscarEntidadesSuperioresSelect(c.Select(a => a.Seq).ToList(), TipoVisao.VisaoOrganizacional));
            listaEntidades.AddRange(HierarquiaEntidadeDomainService.BuscarEntidadesSuperioresSelect(c.Select(a => a.Seq).ToList(), TipoVisao.VisaoLocalidades));

            //Buscando e retornando os sequenciais das origens dos materiais de cada entidade encontrada.
            EntidadeOrigemMaterialSpecification specEntidade = new EntidadeOrigemMaterialSpecification();
            specEntidade.SeqsEntidades = listaEntidades.Select(a => a.Seq).ToList();

            // Busca a instituição de ensino
            var seqInstituicaoEnsino = PessoaAtuacaoDomainService.SearchProjectionByKey(seqColaborador, x => x.Pessoa.SeqInstituicaoEnsino);
            if (!specEntidade.SeqsEntidades.Contains(seqInstituicaoEnsino))
                specEntidade.SeqsEntidades.Add(seqInstituicaoEnsino);

            return EntidadeDomainService.SearchProjectionBySpecification(specEntidade, a => a.SeqOrigemMaterial).Where(a => a != null).Select(a => a.Value).ToList();
        }

        /// <summary>
        /// Busca o curso do aluno logado e lista todas as origens de material das entidades superiores a este curso
        /// nas hierarquias da visão Gestão Organizacional e Visão Localidade.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno.</param>
        /// <returns>Lista de sequenciais de OrigemMaterial.</returns>
        private List<long> OrigensMaterialPorAluno(long seqPessoaAtuacao)
        {
            // Buscando o curso do aluno
            var seqsCurso = CursoDomainService.BuscarCursoDoAluno(seqPessoaAtuacao);

            // Busca a instituição de ensino
            var seqInstituicaoEnsino = PessoaAtuacaoDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => x.Pessoa.SeqInstituicaoEnsino);

            // Buscando as entidades superiores
            List<long> listaEntidades = new List<long> { seqInstituicaoEnsino };
            if (seqsCurso.SeqCurso.HasValue)
            {
                // Inclui o curso na lista de entidades para origem
                listaEntidades.Add(seqsCurso.SeqCurso.Value);

                // Busca as entidades superiores do curso na hierarquia de entidade organizacional
                var hierarquia = HierarquiaEntidadeDomainService.BuscarEntidadesSuperioresSelect(new List<long>() { seqsCurso.SeqCurso.Value }, TipoVisao.VisaoOrganizacional);
                listaEntidades.AddRange(hierarquia.Select(h => h.Seq));
            }
            if (seqsCurso.SeqCursoOfertaLocalidade.HasValue)
            {
                var hierarquia = HierarquiaEntidadeDomainService.BuscarEntidadesSuperioresSelect(new List<long>() { seqsCurso.SeqCursoOfertaLocalidade.Value }, TipoVisao.VisaoLocalidades);
                listaEntidades.AddRange(hierarquia.Select(h => h.Seq));
            }

            // Buscando e retornando os sequenciais das origens dos materiais de cada entidade encontrada.
            EntidadeOrigemMaterialSpecification specEntidade = new EntidadeOrigemMaterialSpecification()
            {
                SeqsEntidades = listaEntidades
            };
            return EntidadeDomainService.SearchProjectionBySpecification(specEntidade, a => a.SeqOrigemMaterial).Where(a => a != null).Select(a => a.Value).ToList();
        }

        /// <summary>
        /// Gera um arquivo .zip para download.
        /// </summary>
        /// <param name="seqsMateriais">Sequenciais dos materiais do tipo arquivo para extrair os arquivos e gerar o arquivo .zip.</param>
        /// <returns>Arquivo .zip dos materiais em questão para download.</returns>
        public SMCFile DownloadMateriais(List<long> seqsMateriais)
        {
            List<SMCFile> arquivos = new List<SMCFile>();

            foreach (var seqMaterial in seqsMateriais)
            {
                Material m = SearchByKey(new SMCSeqSpecification<Material>(seqMaterial), a => a.ArquivoAnexado);
                if (m != null && m.TipoMaterial == TipoMaterial.Arquivo)
                {
                    SMCFile file = m.ArquivoAnexado.Transform<SMCFile>();

                    //Para evitar que os arquivos tenham o mesmo nome dentro do .zip
                    if (arquivos.Any(a => a.Nome == file.Nome))
                    {
                        file.Nome = m.Seq + " - " + m.ArquivoAnexado.Nome;
                    }

                    arquivos.Add(file);
                }
            }

            return (arquivos.Count > 0) ?
                new SMCFile() { Conteudo = SMCFileHelper.ZipFiles(arquivos.ToArray()), Nome = "ArquivosAnexados.zip" } :
                new SMCFile();
        }

        public long BuscarSequencialArquivoAnexado(long seqMaterial)
        {
            try
            {
                Material m = SearchByKey(new SMCSeqSpecification<Material>(seqMaterial));

                if (m == null || m.Seq == 0 || (m.SeqArquivoAnexado.GetValueOrDefault() == 0 && m.TipoMaterial != TipoMaterial.Pasta))
                {
                    throw new ArquivoInvalidoException();
                }

                if (m.TipoMaterial == TipoMaterial.Pasta)
                {
                    throw new PastaSemArquivoException();
                }

                return m.SeqArquivoAnexado.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Guid BuscarUidArquivoAnexado(long seqMaterial)
        {
            try
            {
                var dadosMaterialArquivo = SearchProjectionByKey(new SMCSeqSpecification<Material>(seqMaterial), m => new
                {
                    m.Seq,
                    m.SeqArquivoAnexado,
                    m.TipoMaterial,
                    m.ArquivoAnexado.UidArquivo
                });

                if (dadosMaterialArquivo == null || dadosMaterialArquivo.Seq == 0 || (!dadosMaterialArquivo.UidArquivo.HasValue && dadosMaterialArquivo.TipoMaterial != TipoMaterial.Pasta))
                {
                    throw new ArquivoInvalidoException();
                }

                if (dadosMaterialArquivo.TipoMaterial == TipoMaterial.Pasta)
                {
                    throw new PastaSemArquivoException();
                }

                return dadosMaterialArquivo.UidArquivo.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ExcluirMaterial(long seq)
        {
            //Obtendo o registro do banco para verificar o seu tipo
            Material m = SearchByKey(new SMCSeqSpecification<Material>(seq), a => a.Visualizacoes, a => a.ArquivoAnexado);

            //Verificando se tem filhos
            if (m.TipoMaterial == TipoMaterial.Pasta)
            {
                ExcluirArvore(seq);
            }
            DeleteEntity(m);
        }

        private void ExcluirArvore(long seq)
        {
            MaterialFilterSpecification spec = new MaterialFilterSpecification();
            spec.SeqSuperior = seq;
            var listaFilhos = SearchBySpecification(spec, a => a.Visualizacoes, a => a.ArquivoAnexado).ToList();

            if (listaFilhos != null && listaFilhos.Count > 0)
            {
                foreach (var filho in listaFilhos)
                {
                    if (filho.TipoMaterial == TipoMaterial.Pasta)
                    {
                        //Verificando se tem filhos
                        ExcluirArvore(filho.Seq);
                    }
                    DeleteEntity(filho);
                }
            }
        }
    }
}