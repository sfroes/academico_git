using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DeclaracaoGenericaHistoricoListarVO : ISMCMappable
    {     
        public long SeqDocumento { get; set; }
        public string DescSituacao { get; set; }
        public DateTime? DataEmissao { get; set; } //Exibir a data de criação no formato dd/mm/aa hh:mm
        public string UsuarioResponsavel { get;set; } //Exibir o usuário de criação --Conferir com o Analista de onde obter
        public string Observacoes { get; set; }

    }

}
