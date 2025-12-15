(function ($) {

    //Ready da página
    $(document).ready(function () {

        var btnPesquisar = $('form.smc-form button.smc-btn-pesquisar-icotext-destaque');
        var btnNovaRaiz = $('form.smc-form a.smc-btn-custom.smc-btn-novo-icotext');

        //Change do Select
        var InstituicaoTipoEntidadeChange = function () {

            var valorSelecionado = smc.core.uicontrol('SeqInstituicaoTipoEntidade').getValue();
 
            //Esconde / Exibe o botão Nova raiz
            if (valorSelecionado) {
                btnNovaRaiz.show();

                //Executa o click do pesquisar
                btnPesquisar.click();
            }
            else
                btnNovaRaiz.hide();
        }

        //Inicializa os eventos e efetua algumas configurações
        var Inicializar = function () {

            //Escondo o botão Nova Raiz se o select não possuir um valor selecionado
            if (!smc.core.uicontrol('SeqInstituicaoTipoEntidade').getValue())
                btnNovaRaiz.hide();

            //Atribuindo o evento
            $('#SeqInstituicaoTipoEntidade').change(InstituicaoTipoEntidadeChange);
        }();

    });

})(jQuery);

