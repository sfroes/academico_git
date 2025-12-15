function GerarRelatorio() {
    var newWin = window.open(window.location.pathname + "/ApresentarRelatorio");

    if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
        alert('Janelas pop-up bloqueadas. Favor desbloquear abertura de janelas pop-up no seu navegador para exibir o relatório.');
    }
}