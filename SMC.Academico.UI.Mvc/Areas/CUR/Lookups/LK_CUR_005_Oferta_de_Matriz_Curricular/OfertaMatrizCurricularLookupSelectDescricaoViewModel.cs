using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class OfertaMatrizCurricularLookupSelectDescricaoViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long Seq { get; set; }

        public long SeqMatrizCurricular { get; set; }

        /// <summary>
        /// [Descrição da matriz curricular]+ "-" + [Descrição complementar] + "-" + [localidade] + "-" + [Turno]
        /// </summary>
        [SMCDescription]
        public string OfertaCompleto
        {
            get
            {
                return string.IsNullOrEmpty(DescricaoComplementarMatrizCurricular) ?
                        $"{DescricaoMatrizCurricular} - {DescricaoLocalidade} - {DescricaoTurno}"
                      : $"{DescricaoMatrizCurricular} - {DescricaoComplementarMatrizCurricular} - {DescricaoLocalidade} - {DescricaoTurno}";
            }
        }

        public string DescricaoUnidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string Codigo { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public List<MatrizCurricularOfertaExcecaoLocalidadeData> ExcecoesLocalidade { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }
    }

    public class OfertaMatrizCurricularLookupSelectDescricaoFixBinderViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long Seq { get; set; }

        /// <summary>
        /// [Descrição da matriz curricular]+ "-" + [Descrição complementar] + "-" + [localidade] + "-" + [Turno]
        /// </summary>
        [SMCDescription]
        public string OfertaCompleto { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public long SeqMatrizCurricular { get; set; }
    }
}
