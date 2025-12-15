using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.FIN;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqGrupoCurricular { get; set; }

        public string TipoConfiguracaoDescricao { get; set; }

        public long Index { get; set; }

        public long SeqCurriculo { get; set; }

        /// <summary>
        /// Sequencial do curso utilizado pela interface como parâmetro para o lookup de formação específica
        /// </summary>
        [SMCMapProperty("Curriculo.SeqCurso")]
        public long SeqEntidadeResponsavelFormacao { get; set; }

        /// <summary>
        /// Sequencial do nível de ensino do curso utilizado pelo datasource de tipos de grupos curriculares
        /// </summary>
        [SMCMapProperty("Curriculo.Curso.SeqNivelEnsino")]
        public long SeqNivelEnsino { get; set; }

        public long? SeqGrupoCurricularSuperior { get; set; }

        [SMCMapProperty("GrupoCurricularSuperior.SeqTipoConfiguracaoGrupoCurricular")]
        public long? SeqTipoConfiguracaoGrupoCurricularSuperior { get; set; }

        public string Descricao { get; set; }

        public long SeqTipoGrupoCurricular { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long SeqTipoConfiguracaoGrupoCurricular { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        public short? QuantidadeItens { get; set; }

        public short? QuantidadeHoraRelogio { get; set; }

        public short? QuantidadeHoraAula { get; set; }

        public short? QuantidadeCreditos { get; set; }

        [SMCMapProperty("Curriculo.SeqCurso")]
        public long? SeqCurso { get; set; }

        public List<BeneficioData> Beneficios { get; set; }

        public virtual List<CondicaoObrigatoriedadeData> CondicoesObrigatoriedade { get; set; }
    }
}