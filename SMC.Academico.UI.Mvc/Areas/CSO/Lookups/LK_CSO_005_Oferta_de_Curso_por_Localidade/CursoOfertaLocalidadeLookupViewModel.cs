using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoOfertaLocalidadeLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSortable(true, true, "CursoOferta.Curso.NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }

        [SMCHidden]
        public long? SeqNivelEnsino { get; set; }

        [SMCHidden]
        public long? SeqEntidadeResponsavel { get; set; }

        [SMCHidden]
        public long? SeqCursoOferta { get; set; }

        [SMCHidden]
        public long? SeqLocalidade { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSortable(true, false, "CursoOferta.Curso.Nome")]
        public string DescricaoCurso { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        [SMCSortable(true, false, "CursoOferta.Descricao")]
        public string DescricaoOfertaCurso { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public bool Ativo { get; set; }

        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCSortable(true, false, "HierarquiasEntidades.ItemSuperior.Entidade.Nome")]
        public string NomeLocalidade { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCSortable(true, false, "Modalidade.Descricao")]
        public string DescricaoModalidade { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string DescricaoFormatada
        {
            get
            {
                if (string.IsNullOrEmpty(this.DescricaoOfertaCurso) || string.IsNullOrEmpty(this.NomeLocalidade))
                {
                    return null;
                }

                return $"{DescricaoOfertaCurso} - {NomeLocalidade}";
            }
        }
    }
}
