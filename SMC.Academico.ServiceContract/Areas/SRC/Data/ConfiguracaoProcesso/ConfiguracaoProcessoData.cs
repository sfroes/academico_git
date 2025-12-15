using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoProcessoData : ISMCMappable
    {
        #region Processo

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public bool ProcessoEncerrado { get; set; }

        #endregion

        public long Seq { get; set; }

        public string Descricao { get; set; }

        public List<ConfiguracaoProcessoNivelEnsinoData> NiveisEnsino { get; set; }

        public List<ConfiguracaoProcessoTipoVinculoAlunoData> TiposVinculoAluno { get; set; }

        public List<ConfiguracaoProcessoCursoData> Cursos { get; set; }
    }
}
