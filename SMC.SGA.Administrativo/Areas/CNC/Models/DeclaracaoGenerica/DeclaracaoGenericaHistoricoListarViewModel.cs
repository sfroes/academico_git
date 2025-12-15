using System;
using SMC.Framework.DataAnnotations;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DeclaracaoGenericaHistoricoListarViewModel : SMCViewModelBase, ISMCMappable
    {

        [SMCHidden]
        public long SeqDocumento { get; set; }        

        [SMCDisplay]        
        public string DescSituacao { get; set; }

        [SMCDisplay]        
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataEmissao { get; set; } //Exibir a data de criação no formato dd/mm/aa hh:mm
        
        [SMCDisplay]        
        public string UsuarioResponsavel { get; set; } //Exibir o usuário de criação 

        [SMCDisplay]        
        public string Observacoes { get; set; }

     
    }
}