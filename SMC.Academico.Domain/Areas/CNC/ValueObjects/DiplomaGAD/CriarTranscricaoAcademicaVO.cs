using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class CriarTranscricaoAcademicaVO : ISMCMappable
    {
        public CriarTranscricaoAcademicaVO()
        {
            AcademicTranscript = new TranscricaoAcademicaVO()
            {
                Aluno = new DiplomadoVO()
                {
                    Naturalidade = new NaturalidadeVO(),
                    Rg = new RgVO(),
                    OutroDocumentoIdentificacao = new OutroDocumentoIdentificacaoVO()
                },
                DadosCurso = new DadosMinimosCursoVO()
                {
                    InformacoesTramitacaoEmec = new InformacoesTramitacaoEmecVO(),
                    NomesHabilitacao = new List<string>(),
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
                IesEmissora = new DadosMinimosIesEmissoraVO()
                {
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
                    },
                    Endereco = new EnderecoVO()
                },
                HistoricoEscolar = new HistoricoVO()
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
                                CargaHorariaComEtiqueta = new List<CargaHorariaComEtiquetaVO>(),
                                CargaHorariaV2 = new CargaHorariaVO(),
                                Docentes = new List<DocenteVO>()
                            },
                            AtividadeComplementar = new AtividadeComplementarVO()
                            {
                                CargaHorariaEmHoraRelogioComEtiqueta = new List<HoraRelogioComEtiquetaVO>(),
                                Docentes = new List<DocenteVO>()
                            },
                            Estagio = new EstagioVO()
                            {
                                Concedente = new ConcedenteEstagioVO(),
                                Docentes = new List<DocenteVO>(),
                                CargaHorariaEmHoraRelogioComEtiqueta = new List<HoraRelogioComEtiquetaVO>()
                            },
                            SituacaoDiscente = new SituacaoDiscenteVO()
                            {
                                Intercambio = new SituacaoIntercambioVO(),
                                Formado = new SituacaoFormadoVO()
                            }
                        }
                    },
                    Ingresso = new IngressoVO() 
                    { 
                        FormasAcesso = new List<string>()
                    }
                },
            };
        }

        public string UsuarioInclusao { get; set; }
        public long SeqConfiguracaoHistorico { get; set; }
        public string Type { get; set; } // enum Partial, Final, Copy 
        public TranscricaoAcademicaVO AcademicTranscript { get; set; }
    }
}

