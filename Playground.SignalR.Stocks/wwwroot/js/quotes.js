"use strict";

(function() 
{
    var quoteConn = new signalR.HubConnectionBuilder()
                               .withUrl("/quoteHub")
                               .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
                               .build();
    quoteConn.serverTimeoutInMilliseconds = 30000;

    var selectSymbols = document.querySelector('#selectSymbols');
    
    var currentSymbol = '';
    selectSymbols.disabled = true;
    selectSymbols.addEventListener("focus", function(event) 
    {
        currentSymbol = event.target.value;
    });

    selectSymbols.addEventListener("change", function(event) 
    {
        quoteConn.invoke("ChangeSubscription", { CurrentSymbol: currentSymbol, NewSymbol: event.target.value })
                 .catch(function(error) 
                 {
                   console.error(error.toString());
                   spanError.innerHTML = 'Falha ao registrar seu pedido de atualização de cotações';
                 });
                    
        currentSymbol = selectSymbols.value;
    });

    var spanSymbol = document.querySelector('#spanSymbol');
    var spanTime = document.querySelector('#spanTime');
    var spanPrice = document.querySelector('#spanPrice');
    var spanError = document.querySelector('#spanError');

    quoteConn.on("SendQuote", function (quote) 
    {
        spanSymbol.innerHTML = quote.Symbol;
        spanPrice.innerHTML = parseFloat(quote.Price).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL', mininumFractionDigits: 2, maximumFractionDigits: 2 });
        spanTime.innerHTML = quote.Time.toLocaleTimeString('pt-BR');
    });

    quoteConn.start()
             .then(function ()
              {
                selectSymbols.disabled = false;
              })
              .catch(function (error) 
              {
                spanError.innerHTML = 'Falha ao iniciar conexão com o servidor. Aperte F5.';
              });

    quoteConn.onclose(function(error)
    {
      spanError.innerHTML = 'Conexão com o servidor perdida. Aperte F5.';
    });
    
})();