using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class InstituicaoTipoEntidadeFormacaoEspecificaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqPai { get; set; }

        public long SeqInstituicaoTipoEntidade { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCMapProperty("TipoFormacaoEspecifica.Descricao")]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public bool ObrigatorioAssociacaoIngressante { get; set; }

        public short QuantidadePermitidaAssociacaoIngressante { get; set; }

        public bool ObrigatorioAssociacaoAluno { get; set; }

        public short QuantidadePermitidaAssociacaoAluno { get; set; }

        //Sequenciais dos filhos alinhado para verificar os tipos do colaborador
        public List<long> SeqsFilhos { get; set; }

        /// <summary>
        /// Nível do item na hierarquia de tipos de formação para o tipo de entidade.
        /// Ex:
        /// Área de concentração = nível 1
        /// |- Linha de pesquisa = nível 2
        /// |-- Eixo temático    = nível 3
        /// Área temática        = nível 1
        /// </summary>
        public int NivelHierarquia { get; set; }

        public List<InstituicaoTipoEntidadeFormacaoEspecificaVO> filhos;
    }
}