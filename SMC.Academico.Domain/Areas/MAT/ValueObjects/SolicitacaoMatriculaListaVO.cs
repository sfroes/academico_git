using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaListaVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoVinculo { get; set; }

        public long SeqInstituicao { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqSituacaoAtual { get; set; }

        public long? SeqSolicitacaoHistoricoSituacaoAtual { get; set; }

        public string NomeInstituicaoEnsino { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string DescricaoOferta { get; set; }

        public List<string> DescricoesOfertas { get; internal set; }

        public IEnumerable<long> SeqsTipoTermoIntercambioAssociados { get; set; }

        public string CicloLetivo { get; set; }

        public string DescricaoSituacao { get; set; }

        public bool ExibirVinculo { get; set; }

        public bool ExibirCicloLetivo { get; set; }

        public List<SolicitacaoServicoEtapa> Etapas { get; set; }
        
        public bool ExigeCurso { get; set; }

        public DateTime? DataFimProcesso { get; set; }
    }
}