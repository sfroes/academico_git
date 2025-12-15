using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoCursoOfertaGrupoData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public long SeqGrupoCurricular { get; set; }

        public CurriculoCursoOfertaGrupoComponenteObrigatorio Obrigatorio { get; set; }

        public bool ExibidoHistoricoEscolar { get; set; }

        public bool DesconsiderarIntegralizacao { get; set; }

        public CurriculoCursoOfertaGrupoValorData QuantidadesDisponiveis { get; set; }
    }
}