using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.APR.Models.EntregaOnline
{
    public class CabecalhoEntregaOnlineViewModel2 : ISMCMappable
    {
        public long SeqAplicacaoAvaliacao { get; set; }

        public string DescricaoOrigemAvaliacao { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public short? QuantidadeMaximaPessoasGrupo { get; set; }

        [SMCHideLabel]
        public string Instrucao { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public long? SeqArquivoAnexadoInstrucao { get; set; }

        public Guid? UidArquivoAnexadoInstrucao { get; set; }

        public string Data
        {
            get
            {
                string retorno;
                retorno = $"{DataInicio.ToString()} {(DataFim.HasValue ? $"- {DataFim.Value.ToString()}":"")}";
                return retorno;
            }
        }
    }
}