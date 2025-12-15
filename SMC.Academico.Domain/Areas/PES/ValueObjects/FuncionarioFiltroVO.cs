using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class FuncionarioFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NumeroPassaporte { get; set; }
        public long? SeqTipoFuncionario { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public long? SeqEntidade { get; set; }
        public bool IgnorarFiltros { get; set; }
        public long SeqInstituicaoEnsino { get; set; }
        public string DescricaoEntidadeCadastrada { get; set; }
    }
}