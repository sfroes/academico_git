using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurso { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public string DescricaoComplementar { get; set; }

        public long NumeroSequencial { get; set; }

        public List<CurriculoCursoOfertaData> CursosOferta { get; set; }

        /// <summary>
        /// Configuração recuperada no InstituicaoNivel do mesmo nível de ensino do curso informado em SeqCurso
        /// </summary>
        public bool PermiteCreditoComponenteCurricular { get; set; }
    }
}
