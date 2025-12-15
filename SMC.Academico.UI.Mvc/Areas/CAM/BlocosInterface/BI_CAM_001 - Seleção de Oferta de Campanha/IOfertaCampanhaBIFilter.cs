using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;

namespace SMC.Academico.UI.Mvc.Areas.CAM.InterfaceBlocks
{
    public interface IOfertaCampanhaBIFilter
    {
        long? SeqTipoOferta { get; set; }
        FormacaoEspecificaLookupViewModel SeqFormacaoEspecifica { get; set; }

        long? SeqColaborador { get; set; }

        string Turma { get; set; }

        bool? SelecaoNivelFolha { get; }
    }
}