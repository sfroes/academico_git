using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularDetalheViewModel : SMCViewModelBase, ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoTipoComponente { get; set; }

        public string DescricaoCompleta { get; set; }

        public bool Ativo { get; set; }

        public TipoOrganizacao? DescricaoTipoOrganizacao { get; set; }

        public SMCMasterDetailList<ComponenteCurricularNivelEnsinoViewModel> NiveisResponsavel { get; set; }

        public SMCMasterDetailList<ComponenteCurricularNivelEnsinoViewModel> NiveisEnsino { get; set; }

        public SMCMasterDetailList<ComponenteCurricularEntidadeResponsavelViewModel> EntidadesResponsaveis { get; set; }

        public string Observacao { get; set; }

        public bool PermiteEmenta { get; set; }

        public SMCMasterDetailList<ComponenteCurricularEmentaViewModel> Ementas { get; set; }

        public SMCMasterDetailList<ComponenteCurricularDetalheConfiguracoesViewModel> Configuracoes { get; set; }
    }
}