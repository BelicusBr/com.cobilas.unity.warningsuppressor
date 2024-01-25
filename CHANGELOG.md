# ChangeLog
## [1.2.1] - 08/09/2023
###Fixed
- Correction in documentation.
## [1.2.0] - 02/09/2023
### Added
- Documentation has been added.
### Changed
- The `README.md` file has been changed.
## [1.1.0] - 29/08/2023
### Changed
- You hear a change in the author of the MIT license.
## [1.0.3] - 28/08/2023
###Fixed
- Now the `NoWar.json` file is written to the project root folder.
- Now when creating the nowar file, the `assembly` attributes are written line by line instead of a single line.
## [1.0.2] - 03/05/2023
###Fixed
- The private field `[t:CancellationTokenSource]NoWarning.source` was replaced by `[t:bool]NoWarning.cancel` as it caused an unexpected null reference after closing the window.
- Suppressor Warning now checks the Assets folder for Assemblies.
## [1.0.0] - 01/05/2023
- Package `com.cobilas.unity.warningsuppressor` created.