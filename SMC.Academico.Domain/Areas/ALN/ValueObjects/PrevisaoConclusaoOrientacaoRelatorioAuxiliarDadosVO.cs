using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PrevisaoConclusaoOrientacaoRelatorioAuxiliarDadosVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidade { get; set; }

        public string DescricaoEntidade { get; set; }

        public string Nome { get; set; }

        public string NomeSocialOuNome { get; set; }

        public DateTime? DataAdmissao { get; set; }

        public DateTime? DataPrevisaoConclusao { get; set; }

        public DateTime? DataLimiteConclusao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public string DescricaoNivel { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public string DescricaoVinculo { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public string DescricaoTipoTermoIntercambio { get; set; }

        public long? SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string TipoParticipacaoOrientacao { get; set; }

        public DateTime? DataInicioOrientacao { get; set; }

        public DateTime? DataFimOrientacao { get; set; }

        public long? SeqCicloLetivoSituacao { get; set; }

        public string DescricaoCicloLetivoSituacao { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }
    }
}
