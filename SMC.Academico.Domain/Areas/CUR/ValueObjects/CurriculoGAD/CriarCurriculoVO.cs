using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CriarCurriculoVO : ISMCMappable
    {
        public CriarCurriculoVO()
        {
            Curriculum = new CurriculumVO()
            {
                DadosCurso = new DadosMinimosCursoVO()
                {
                    InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO(),
                    Habilitacoes = new List<HabilitacaoVO>(),
                    Autorizacao = new AtoRegulatorioVO()
                    {
                        InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                    },
                    Reconhecimento = new AtoRegulatorioVO()
                    {
                        InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                    },
                    RenovacaoReconhecimento = new AtoRegulatorioVO()
                    {
                        InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                    }
                },
                IesEmissora = new IesEmissoraVO()
                {
                    Endereco = new EnderecoVO(),
                    Mantenedora = new MantenedoraVO()
                    {
                        Endereco = new EnderecoVO()
                    },
                    Credenciamento = new AtoRegulatorioVO()
                    {
                        InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                    },
                    Recredenciamento = new AtoRegulatorioVO()
                    {
                        InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                    },
                    RenovacaoDeRecredenciamento = new AtoRegulatorioVO()
                    {
                        InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                    }
                },
                Etiquetas = new List<DadosEtiquetaVO>(),
                Areas = new List<DadosAreaVO>(),
                EstruturaCurricular = new EstruturaCurricularVO()
                {
                    UnidadesCurriculares = new List<UnidadeCurricularVO>()
                    {
                       new UnidadeCurricularVO()
                       {
                           Ementa = new EmentaVO()
                           {
                               Itens = new List<ItemEmentaVO>()
                           },
                           Equivalencias = new List<UnidadeCurricularEquivalenteVO>()
                           {
                               new UnidadeCurricularEquivalenteVO()
                               {
                                   CodigosUnidadesEquivalentes = new List<string>() 
                               }
                           },
                           PreRequisitos = new List<string>(),
                           Etiquetas = new List<EtiquetaVO>(),
                           Areas = new List<string>()
                       }
                    }
                },
                EstruturaAtividadeComplementar = new EstruturaAtividadeComplementarVO()
                {
                    Categorias = new List<CategoriaAtividadeComplementarVO>()
                    {
                        new CategoriaAtividadeComplementarVO()
                        {
                            Atividades = new List<AtividadeComplementarCurriculoVO>()
                        }
                    }
                },
                CriteriosIntegralizacao = new List<CriterioIntegralizacaoVO>()
                {
                    new CriterioIntegralizacaoVO()
                    {
                        Rotulos = new RotulosIntegralizacaoVO()
                        {
                            Etiquetas = new List<string>(),
                            LimitesCargaHoraria = new LimitesCargaHorariaVO()
                        },
                        Expressao = new ExpressaoIntegralizacaoVO()
                        {
                            SomatorioCodigos = new List<string>(),
                            LimitesCargaHoraria = new LimitesCargaHorariaVO()
                        }
                    }
                }
            };
        }

        public string UsuarioInclusao { get; set; }
        public long SeqConfiguracaoCurriculo { get; set; }
        public CurriculumVO Curriculum { get; set; }
    }
}
