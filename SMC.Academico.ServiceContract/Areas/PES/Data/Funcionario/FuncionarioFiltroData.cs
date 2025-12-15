using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class FuncionarioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NumeroPassaporte { get; set; }
        public long? SeqTipoFuncionario { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public long? SeqEntidade { get; set; }

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        public bool IgnorarFiltros { get; set; }
        public long SeqInstituicaoEnsino { get; set; }

        public string DescricaoEntidadeCadastrada { get; set; }
    }
}