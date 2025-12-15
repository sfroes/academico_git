using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoBloqueioDesbloqueioData : ISMCMappable
    {
        public long Seq { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public TipoDesbloqueio TipoDesbloqueio { get; set; }

        public DateTime DataDesbloqueio { get; set; }

        public string ResponsavelDesbloqueio { get; set; }

        public string JustificativaDesbloqueio { get; set; }

        public List<PessoaAtuacaoBloqueioCompovanteData> Comprovantes { get; set; }

        public List<PessoaAtuacaoBloqueioCompovanteData> ComprovantesOpcional { get; set; }

        public bool MotivoObrigatorioAnexoDesbloqueio { get; set; }

        [SMCMapProperty("MotivoBloqueio.PermiteItem")]
        public bool PermiteItem { get; set; }

        [SMCMapProperty("MotivoBloqueio.PermiteDesbloqueioTemporario")]
        public bool PermiteDesbloqueioTemporario { get; set; }

        public List<PessoaAtuacaoBloqueioItemData> Itens { get; set; }
    }
}