using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorFiltroData : SMCPagerFilterData, ISMCMappable
    {

        [SMCKeyModel]
        public long? Seq { get; set; }

        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqEntidadeVinculo { get; set; }

        public long? SeqTipoVinculoColaborador { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public SituacaoColaborador? SituacaoColaboradorNaInstituicao { get; set; }

        public bool? Oritentador { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long[] SeqsAlunos { get; set; }

        public OrigemColaborador? OrigemColaborador { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public long? SeqCampanhaOferta { get; set; }

        public bool? TipoEntidadePermiteVinculo { get; set; }

        public bool? VinculoAtivo { get; set; }

        public bool? CriaVinculoInstitucional { get; set; }

        public long? SeqTurma { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        public bool IgnorarFiltros { get; set; }

        public bool? AptoLecionarComponenteTurma { get; set; }

    }
}