using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteListaVO : ISMCMappable
    {
        #region [ DadosPessoais ]

        public long Seq { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public DateTime? DataNascimento { get; set; }

        public SituacaoIngressante SituacaoIngressante { get; set; }

        public OrigemIngressante OrigemIngressante { get; set; }

        public bool Falecido { get; set; }

        #endregion [ DadosPessoais ]

        #region [ DadosAcademicos ]

        public long SeqNivelEnsino { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public string Campanha { get; set; }

        public string ProcessoSeletivo { get; set; }

        public string GrupoEscalonamento { get; set; }

        public TermoIntercambio TermoIntercambio { get; set; }

        public string DescricaoTipoTermoIntercambio { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public DateTime? DataInicioTermoIntercambio { get; set; }

        public DateTime? DataFimTermoIntercambio { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public string Vinculo { get; set; }

        public string FormaIngresso { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public IEnumerable<string> OfertasCampanha { get; set; }

        public string MatrizCurricularOferta { get; set; }

        public List<string> FormacoesEspecificas { get; set; }

        public IEnumerable<long> FormacoesEspecificasIngressante { get; set; }

        public string TipoOrientacao { get; set; }

        public IEnumerable<IngressanteOrientacaoVO> OrientadoresIngressante { get; set; }

        public IEnumerable<string> Orientadores { get; set; }

        public bool PossuiSituacaoImpeditivaIngressante { get; set; }

        public bool NaoExigeOfertaMatrizCurricular { get; set; }

        public bool NaoPossuiVinculoAssociacaoOrientador { get; set; }

        public bool NaoPermiteAssociacaoOrientador { get; set; }

        public bool TipoVinculoAlunoExigeCurso { get; set; }

        public bool ExigePeriodoIntercambioTermo { get; set; }

        public bool ExibirLiberacaoMaricula { get; set; }

        public bool PermitirLiberacaoMatricula { get; set; }

        public string DescricaoInstituicaoTransferenciaExterna { get; set; }

        public string CursoTransferenciaExterna { get; set; }

        public bool VinculoInstituicaoNivelEnsinoExigeCurso { get; set; }

        #endregion [ DadosAcademicos ]
    }
}