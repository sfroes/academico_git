using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class TipoDivisaoComponenteData : ISMCMappable
    {       
        public long Seq { get; set; }
                
        public long SeqTipoComponenteCurricular { get; set; }
               
        public string Descricao { get; set; }
               
        public long SeqModalidade { get; set; }
                
        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get; set; }

        public bool GeraOrientacao { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }

        public bool? Artigo { get; set; }

        public bool PermitirCargaHorariaGrade { get; set; }
    }
}
