// APLICA O SHADOW ROOT PARA ISOLAR O CSS VINDO DO TEMPLATE DO EMAIL
$(document).ready(function () {
    // Container dinâmico (#mensagemNotificacao) será inserido dentro de um elemento com ID fixo:
    const alvoEstavel = document.getElementById('wizardEnvioNotificacao') || document.body;

    const observer = new MutationObserver(() => {
        const container = document.getElementById('mensagemNotificacao');

        if (container && !container.shadowRoot && container.innerHTML.trim() !== '') {
            const emailHtml = container.innerHTML;
            container.innerHTML = '';

            try {
                const shadowRoot = container.attachShadow({ mode: 'open' });
                shadowRoot.innerHTML = emailHtml;
            } catch (e) {
                console.warn('Erro ao aplicar Shadow DOM:', e);
            }
        }
    });

    observer.observe(alvoEstavel, { childList: true, subtree: true });
});