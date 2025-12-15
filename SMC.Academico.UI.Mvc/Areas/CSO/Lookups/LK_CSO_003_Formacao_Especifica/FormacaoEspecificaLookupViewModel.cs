using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class FormacaoEspecificaLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCMappable
    {
        [SMCKey]
        public long? Seq { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqCurso { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("SeqFormacaoEspecificaSuperior")]
        public long? SeqPai { get; set; }

        public bool? Folha { get; set; }

        [SMCMapProperty("ApenasAtivos")]
        public bool? Ativo { get; set; }

        public bool? ExibeGrauDescricaoFormacao { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }

        [SMCDescription]
        public string DescricaoCompleta
        {
            get
            {
                if (ExibeGrauDescricaoFormacao.HasValue)
                    if ((bool)ExibeGrauDescricaoFormacao && !string.IsNullOrEmpty(DescricaoGrauAcademico))
                        return !string.IsNullOrEmpty(Descricao) ? $"[{DescricaoTipoFormacaoEspecifica}] {Descricao} ({DescricaoGrauAcademico})" : null;

                return !string.IsNullOrEmpty(Descricao) ? $"[{DescricaoTipoFormacaoEspecifica}] {Descricao}" : null;
            }
        }

        public bool? Selecionavel { get; set; }
    }
}