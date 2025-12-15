using Newtonsoft.Json;
using SMC.Framework.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ChancelaProjectionVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoOfertaCurso { get; set; }

        public string NumeroProtocolo { get; set; }

        public string NomeSolicitante { get; set; }

        public string NomeSocialSolicitante { get; set; }

        public long SeqTemplateProcessoSGF { get; set; }

        public long SeqSituacaoEtapaSGF { get; set; }

        public string PeriodoChancela { get; set; }

        public string BloqueiosJson { get; set; }

        public bool EtapaChancelaLiberada { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string TokenEtapaSGF { get; set; }

        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public bool PermiteVisualizarPlanoEstudo { get; set; }

        public bool ChancelaVigente { get; set; }

        public List<string> Bloqueios
        {
            get
            {
                return string.IsNullOrEmpty(BloqueiosJson) ? null : (JsonConvert.DeserializeObject<List<DescricaoBloqueio>>(BloqueiosJson).Select(x => x.DescricaoMotivoBloqueio).ToList());
            }
        }

        private class DescricaoBloqueio
        {
            public string DescricaoMotivoBloqueio { get; set; }
        }

        public long? SeqUsuarioResponsavelAtendimentoSas { get; set; }

        public bool ProcessoEtapaFiltroOrientador { get; set; }
    }
}