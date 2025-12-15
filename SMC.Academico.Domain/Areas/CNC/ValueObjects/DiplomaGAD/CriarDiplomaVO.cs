using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class CriarDiplomaVO : ISMCMappable
    {
        public CriarDiplomaVO()
        {
            Degree = new DocumentoDiplomaVO()
            {
                Diploma = new CriarDadosDiplomaVO()
                {
                    DadosDiploma = new DadosDiplomaVO()
                    {
                        Diplomado = new DiplomadoVO()
                        {
                            Naturalidade = new NaturalidadeVO(),
                            Rg = new RgVO(),
                            OutroDocumentoIdentificacao = new OutroDocumentoIdentificacaoVO()
                        },
                        DadosCurso = new DadosCursoVO()
                        {
                            InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO(),
                            NomesHabilitacao = new List<string>(),
                            Habilitacoes = new List<HabilitacaoVO>(),
                            Enfases = new List<string>(),
                            TituloConferido = new TituloConferidoVO(),
                            EnderecoCurso = new EnderecoVO(),
                            Polo = new PoloVO()
                            {
                                Endereco = new EnderecoVO(),
                                InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO()
                            },
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
                        Assinantes = new List<InformacaoAssinanteVO>(),
                        IesEmissora = new IesEmissoraVO()
                        {
                            Endereco = new EnderecoVO(),
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
                            },
                            Mantenedora = new MantenedoraVO()
                            {
                                Endereco = new EnderecoVO()
                            }
                        },
                        DeclaracoesAcercaProcesso = new List<string>(),
                        DadosIesOriginalCursoPTA = new DadosIesOriginalCursoPtaVO()
                        {
                            Endereco = new EnderecoVO(),
                            Descredenciamento = new AtoRegulatorioBaseVO()
                        }
                    },
                    DadosRegistro = new DadosRegistroVO()
                    {
                        IesRegistradora = new IesRegistradoraVO()
                        {
                            Endereco = new EnderecoVO(),
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
                            },
                            AutorizacaoRegistro = new AtoRegulatorioBaseVO(),
                            Mantenedora = new MantenedoraVO()
                            {
                                Endereco = new EnderecoVO()
                            }
                        },
                        LivroRegistro = new LivroRegistroVO()
                        {
                            ResponsavelRegistro = new ResponsavelRegistroVO()
                        },
                        Assinantes = new List<InformacaoAssinanteVO>(),
                        InformacoesProcessoJudicial = new InformacoesProcessoJudicialVO()
                    }
                },
                DocumentacaoAcademica = new CriarDadosDocumentacaoAcademicaVO()
                {
                    Registro = new RegistroVO()
                    {
                        DadosPrivadosDiplomado = new DadosPrivadosDiplomadoVO()
                        {
                            CargaHorariaCursoV2 = new CargaHorariaVO(),
                            Filiacao = new List<FiliacaoVO>(),
                            Historico = new HistoricoVO()
                            {
                                CargaHorariaCursoIntegralizadaV2 = new CargaHorariaVO(),
                                CargaHorariaCurso = new CargaHorariaVO(),
                                SituacaoAtualDiscente = new SituacaoDiscenteVO()
                                {
                                    Intercambio = new SituacaoIntercambioVO(),
                                    Formado = new SituacaoFormadoVO()
                                },
                                SituacaoEnade = new SituacaoAlunoEnadeVO(),
                                ParticipacoesEnade = new List<ParticipacaoEnadeVO>()
                                {
                                   new ParticipacaoEnadeVO()
                                   {
                                       Informacoes = new InformacaoEnadeVO(),
                                       NaoHabilitado = new EnadeNaoHabilitadoVO()
                                   }
                                },
                                MatrizCurricular = new List<MatrizCurricularVO>()
                                {
                                    new MatrizCurricularVO()
                                    {
                                        CargaHorariaV2 = new CargaHorariaVO(),
                                        Docentes = new List<DocenteVO>()
                                    }
                                },
                                ElementosHistorico = new List<ElementoHistoricoVO>()
                                {
                                    new ElementoHistoricoVO()
                                    {
                                        Disciplina = new DisciplinaV2VO()
                                        {
                                            Aprovacao = new AprovacaoDisciplinaVO(),
                                            CargaHorariaV2 = new CargaHorariaVO(),
                                            Docentes = new List<DocenteVO>()
                                        },
                                        AtividadeComplementar = new AtividadeComplementarVO()
                                        {
                                            Docentes = new List<DocenteVO>()
                                        },
                                        Estagio = new EstagioVO()
                                        {
                                            Concedente = new ConcedenteEstagioVO(),
                                            Docentes = new List<DocenteVO>()
                                        },
                                        SituacaoDiscente = new SituacaoDiscenteVO()
                                        {
                                            Intercambio = new SituacaoIntercambioVO(),
                                            Formado = new SituacaoFormadoVO()
                                        }
                                    }
                                },
                                Ingresso = new IngressoVO(),
                                Areas = new List<AreaVO>()
                            },
                            Ingresso = new IngressoVO()
                        },
                        TermoResponsabilidade = new TermoResponsabilidadeVO(),
                        DocumentacaoComprobatoria = new List<DocumentacaoComprobatoriaVO>()
                    }
                }
            };
        }

        public string UsuarioInclusao { get; set; }

        public long SeqConfiguracaoDiploma { get; set; }

        public long SeqConfiguracaoHistorico { get; set; }

        public DocumentoDiplomaVO Degree { get; set; }
    }
}
