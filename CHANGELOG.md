# ChangeLog
## [1.1.0] - 29/08/2023
### Changed
- Ouve uma alteração no autor da liceça MIT.
## [1.0.3] - 28/08/2023
### Fixed
- Agora o arquivo `NoWar.json` é gravado na pasta raiz do projeto.
- Agora na criação do arquivo nowar os atributos `assembly` são escritos linha á linha em vez de uma só linha.
## [1.0.2] - 03/05/2023
### Fixed
- O campo privado `[t:CancellationTokenSource]NoWarning.source` foi substituido por `[t:bool]NoWarning.cancel` por provocar referencia nula inesperadamente apos fechar a janela.
- Agora o Suppressor Warning verifica a pasta Assets em busca por Assemblies.
## [1.0.0] - 01/05/2023
- Pacote `com.cobilas.unity.warningsuppressor` criado.