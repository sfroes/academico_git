(function ($) {
    // Ready da página
    $(document).ready(function () {
        var lookupsHierarquia = $('[id^=Hierarquias_][id$=__Classificacoes]');
        var mdEntidades = $('#HierarquiasEntidades');

        $(mdEntidades).on("change", function () {
            carregarSeqsHierarquia();
        });


        // Carrega o valor de todas seqs itens superriores selecionadas nos selects do mestre detalhe na variável SeqsHierarquiaEntidadeItem
        var carregarSeqsHierarquia = function () {
            // Apenas se o mestre detalhe de hierarquia de entidades estiver na tela
            if (mdEntidades.length > 0) {
                var seqItensSuperiores = '';
                $('[name=SeqsHierarquiaEntidadeItem]').remove();
                $('[name^=HierarquiasEntidades][name$=SeqItemSuperior]').not('[tabindex="-1"]').each(function (i, v) {
                    seqItensSuperiores += '<input name="SeqsHierarquiaEntidadeItem" type="hidden" value="' + ( $(v).val() || "0" ) + '" />';
                });
                $('#SeqsHierarquiaEntidadeItem').append($(seqItensSuperiores));
            }
        }

        // Configura um handler para rcuperar entidades responsaveis no evento de abertura de modal
        lookupsHierarquia.each(function (i, v) {
            $(v).on('smcbeforelookupmodal', function () {
                carregarSeqsHierarquia();
            });
        });
    });
})(jQuery);