## [1.0.2] - 03/05/2023
### Fixed
- O campo privado `[t:CancellationTokenSource]NoWarning.source` foi substituido por `[t:bool]NoWarning.cancel` por provocar referencia nula inesperadamente apos fechar a janela.
- Agora o Suppressor Warning verifica a pasta Assets em busca por Assemblies.
## [1.0.0] - 01/05/2023
- Pacote `com.cobilas.unity.warningsuppressor` criado.