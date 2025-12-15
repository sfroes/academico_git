using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class LancamentoHistoricoEscolarCabecalhoTurmaItemViewModel : ISMCMappable
    {
        public string NomeCursoOfertaLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string CodigoTurma { get; set; }

        public string DescricaoTurmaConfiguracaoComponente { get; set; }

        public string DescricaoOfertaTurno { get { return $"{NomeCursoOfertaLocalidade} - {DescricaoTurno}"; } }

        public string DescricaoTurma { get { return $"{CodigoTurma} - {DescricaoTurmaConfiguracaoComponente}"; } }

        public override string ToString()
        {
            return $"{NomeCursoOfertaLocalidade} - {DescricaoTurno + Environment.NewLine + CodigoTurma} - {DescricaoTurmaConfiguracaoComponente}";
        }
    }
}