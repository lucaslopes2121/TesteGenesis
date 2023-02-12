Feature: Verificar informações no site dos Correios

Scenario: Verificar informações de CEP e rastreamento
  Given que acesso o site dos Correios
  When  realizo uma busca pelo cep invalido
  Then  o resultado deve exibir cep não encontrado
  When  realizo uma busca pelo cep valido
  Then  o resultado deve exibir Rua Quinze de Novembro São PauloSP
  When  realizo uma busca no rastreamento pelo código
  Then  o resultado deve exibir Código não encontrado e o browser deve ser fechado