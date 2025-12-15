using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ConfigurarRelatorioDeclaracaoGenericaVO : ISMCMappable
    {
        #region CABECALHO

        public string NomeCivil { get; set; }
        public string NomeSocial { get; set; }
        public string DescricaoTipoDocumentoAcademico { get; set; }
        public string DescricaoNivelEnsino { get; set; }
        public string DescricaoIdioma { get; set; }

        #endregion CABECALHO

        public long SeqNivelEnsinoPorGrupoDocumentoAcademico { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public SMCLanguage IdiomaDocumentoAcademico { get; set; }

        public long SeqAluno { get; set; }

        public long SeqDadosPessoais { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        public string NomeAlunoOficial { get; set; }

        public List<ConfigurarTagsDeclaracaoGenericaVO> Tags { get; set; }
    }
}
