using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularDetalheVO : ISMCMappable
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

        public IList<ComponenteCurricularNivelEnsinoVO> NiveisResponsavel { get; set; }

        public IList<ComponenteCurricularNivelEnsinoVO> NiveisEnsino { get; set; }

        public IList<ComponenteCurricularEntidadeResponsavelVO> EntidadesResponsaveis { get; set; }

        public string Observacao { get; set; }

        public bool PermiteEmenta { get; set; }

        public IList<ComponenteCurricularEmenta> Ementas { get; set; }

        public IList<ComponenteCurricularDetalheConfiguracoesVO> Configuracoes { get; set; }
    }
}