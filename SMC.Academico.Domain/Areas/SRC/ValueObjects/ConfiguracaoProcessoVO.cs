using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoProcessoVO : ISMCMappable
    {
        #region Processo

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }
     
        public bool ProcessoEncerrado { get; set; }

        #endregion
       
        public long Seq { get; set; }
     
        public string Descricao { get; set; }
      
        public List<ConfiguracaoProcessoNivelEnsinoVO> NiveisEnsino { get; set; }
      
        public List<ConfiguracaoProcessoTipoVinculoAlunoVO> TiposVinculoAluno { get; set; }
      
        public List<ConfiguracaoProcessoCursoVO> Cursos { get; set; }

    }
}
