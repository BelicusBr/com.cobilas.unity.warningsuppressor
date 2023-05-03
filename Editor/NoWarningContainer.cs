using System;
using System.Collections.Generic;

[Serializable]
public sealed class NoWarningContainer {
    public byte status;
    public bool showNoVisibles;
    public string globalNoWarning;
    [NonSerialized] public bool isCompleted;

    public List<IndivNoWar> IndividualNoWarning;

    public NoWarningContainer() {
        status = 0;
        globalNoWarning = string.Empty;
        IndividualNoWarning = new List<IndivNoWar>();
    }

    public bool ContainsAssembly(string name) {
        foreach (var item in IndividualNoWarning)
            if (item.assemblyDefinitionName == name)
                return true;
        return false;
    }

    public int IndexOf(string name) {
        for (int I = 0; I < IndividualNoWarning.Count; I++)
            if (IndividualNoWarning[I].assemblyDefinitionName == name)
                return I;
        return -1;
    }

    [Serializable]
    public sealed class IndivNoWar {
        public bool applay;
        public bool isVisible;
        public string NoWarning;
        public bool applayglobalNoWarning;
        public string assemblyDefinitionName;
        [NonSerialized] public string assemblyDefinitionPath;

        public IndivNoWar() {
            applay =
                isVisible =
                applayglobalNoWarning = true;
            NoWarning =
                assemblyDefinitionName =
                assemblyDefinitionPath = string.Empty;
        }
    }
}
