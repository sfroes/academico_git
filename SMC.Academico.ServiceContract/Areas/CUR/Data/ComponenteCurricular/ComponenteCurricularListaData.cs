using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public bool Ativo { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqInstituicaoNivelResponsavel { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string DescricaoTipoComponenteCurricular { get; set; }

        public string DescricaoCompleta { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public List<ComponenteCurricularNivelEnsinoData> NiveisEnsino { get; set; }

        public List<ComponenteCurricularEntidadeResponsavelData> EntidadesResponsaveis { get; set; }

        public List<ComponenteCurricularLegadoData> ComponentesLegado { get; set; }

        public bool PermiteConfiguracaoComponente { get; set; }

        public ConfiguracaoComponenteCurricular ConfiguracaoComponenteCurricular { get; set; }

        public string DescricaoOfertaCurso { get; set; }

        public string DescricaoCicloLetivo { get; set; }
    }
}