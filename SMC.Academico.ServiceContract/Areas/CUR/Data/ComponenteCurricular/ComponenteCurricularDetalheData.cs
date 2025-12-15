using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularDetalheData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoComponente { get; set; }

        public string DescricaoTipoComponente { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public string DescricaoCompleta { get; set; }

        public bool Ativo { get; set; }

        public TipoOrganizacao? DescricaoTipoOrganizacao { get; set; }

        public List<ComponenteCurricularNivelEnsinoData> NiveisResponsavel { get; set; }

        public List<ComponenteCurricularNivelEnsinoData> NiveisEnsino { get; set; }

        public List<ComponenteCurricularEntidadeResponsavelData> EntidadesResponsaveis { get; set; }

        public string Observacao { get; set; }

        public bool PermiteEmenta { get; set; }

        public List<ComponenteCurricularEmentaData> Ementas { get; set; }

        public List<ComponenteCurricularDetalheConfiguracoesData> Configuracoes { get; set; }
    }
}
