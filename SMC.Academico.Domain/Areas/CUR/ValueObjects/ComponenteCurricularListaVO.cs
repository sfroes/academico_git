using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public bool Ativo { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        [SMCMapMethod(nameof(ComponenteCurricular.RecuperarDescricaoNivelEnsinoResponsavel))]
        public string DescricaoNivelEnsino { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        [SMCMapProperty("TipoComponente.Descricao")]
        public string DescricaoTipoComponenteCurricular { get; set; }

        public string DescricaoCompleta { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public List<ComponenteCurricularNivelEnsinoVO> NiveisEnsino { get; set; }

        public List<ComponenteCurricularEntidadeResponsavelVO> EntidadesResponsaveis { get; set; }

        public List<ComponenteCurricularLegadoVO> ComponentesLegado { get; set; }

        public bool PermiteConfiguracaoComponente { get; set; }

        public bool PossuiConfiguracaoComponente { get; set; }

        public ConfiguracaoComponenteCurricular ConfiguracaoComponenteCurricular { get; set; }

        public string DescricaoOfertaCurso { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string DescricaoAssuntoCompleta { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public bool DisciplinaIsolada { get; set; }

        public long SeqPessoaAtuacao { get; set; }
    }
}