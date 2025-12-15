using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AlunoListaData : ISMCMappable
    {
        #region [ Dados Pessoais ]

        public long Seq { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public DateTime DataNascimento { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public bool Falecido { get; set; }

        public string TipoVinculoAluno { get; set; }

		public bool? AlunoDI { get; set; }

		public bool VinculoAlunoAtivo { get; set; }

        public List<string> Emails { get; set; }

        #endregion [ Dados Pessoais ]

        #region [ Dados Acadêmicos ]

        public string DescricaoEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string DadosVinculo { get; set; }

        public DateTime? DataInicioTermoIntercambio { get; set; }

        public DateTime? DataFimTermoIntercambio { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public bool ExigeParceriaIntercambioIngresso { get; set; }

        public bool ExigePeriodoIntercambioTermo { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public DateTime DataAdmissao { get; set; }

        public DateTime? DataPrevisaoConclusao { get; set; }

        public DateTime? DataLimiteConclusao { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }

        public string TipoOrientacao { get; set; }

        public List<AlunoOrientacaoListaData> Orientacoes { get; set; }

        public List<string> FormacoesEspecificas { get; set; }

        public bool PermiteFormacaoEspecifica { get; set; }

        public string NomeInstituicaoTransferenciaExterna { get; set; }

        public string CursoTransferenciaExterna { get; set; }

        //Relatório
        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoSituacaoMatricula { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public bool PermiteCancelarMatricula { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        public int CodigoPessoaCAD { get; set; }

        public bool PermiteAssociacaoBeneficio { get; set; }

        public bool VinculoInstituicaoNivelEnsinoExigeCurso { get; set; }
        public bool ConcedeFormacao { get; set; }

        #endregion [ Dados Acadêmicos ]
    }
}