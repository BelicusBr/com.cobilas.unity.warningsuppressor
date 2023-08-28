using System.IO;
using UnityEditor;
using UnityEngine;
using System.Text;
using System.Collections;
using UnityEditorInternal;
using UnityEditor.PackageManager;
using Unity.EditorCoroutines.Editor;
using UnityEditor.PackageManager.Requests;

public class NoWarningWindow : EditorWindow {
    [MenuItem("Window/No Warning")]
    static void DoIt() {
        NoWarningWindow war = GetWindow<NoWarningWindow>();
        war.titleContent = GUIContentTemp.lb_title;
        war.minSize = new Vector2(650f, 400f);
        war.Show();
    }

    private Vector2 scrollView1;
    private Vector2 scrollView2;
    [SerializeField] 
    private NoWarningContainer container;

    //IDE0015;IDE0051>~M:NoWarningWindow.tds;
    private string ContainerPath {
        get {
            string folderPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "NoWar");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, "NoWar.json");
        }
    }

    private void OnEnable() {
        if (!File.Exists(ContainerPath)) {
            container = new NoWarningContainer();
            Unload();
        }
        container = JsonUtility.FromJson<NoWarningContainer>(File.ReadAllText(ContainerPath));
        _ = EditorCoroutineUtility.StartCoroutine(GetAssembles(this), this);
    }

    private void OnDisable()
        => Unload();

    private void OnDestroy() {
        container.cancel = true;
        Unload();
    }

    private void Unload() {
        using (FileStream file = File.Open(ContainerPath, FileMode.OpenOrCreate)) {
            file.SetLength(0L);
            byte[] bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(container, true));
            file.Write(bytes, 0, bytes.Length);
        }
    }

    private void ApplayNoWarning(NoWarningContainer noWar) {
        string pj_path = Path.GetDirectoryName(Application.dataPath);
        foreach (var item in noWar.IndividualNoWarning) {
            string filePath = Path.Combine(pj_path, item.assemblyDefinitionPath);
            if (File.Exists(filePath)) {
                string suppressor = item.applay ? item.NoWarning : item.applayglobalNoWarning ? noWar.globalNoWarning : string.Empty;
                CreateWarningSuppressorFile(filePath, suppressor);
            } else {
                filePath = $"{filePath}.nowar.cs";
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }
        AssetDatabase.Refresh();
    }

    private void CreateWarningSuppressorFile(string filePath, string suppressor) {
        filePath = $"{filePath}.nowar.cs";
        if (string.IsNullOrEmpty(suppressor) && File.Exists(filePath))
            File.Delete(filePath);

        using (FileStream file = File.Open(filePath, FileMode.OpenOrCreate)) {
            file.SetLength(0L);
            string[] modules = suppressor.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System.Diagnostics.CodeAnalysis;\r\n");

            foreach (var item in modules) {
                if (item.Contains(">")) {
                    string[] submodules = item.Split(new char[] { '>' }, System.StringSplitOptions.RemoveEmptyEntries);
                    builder.AppendFormat("[assembly: SuppressMessage(\"\", \"{0}\", Scope = \"member\", Target = \"{1}\")]\r\n", submodules[0].Trim(), submodules[1].Trim());
                } else {
                    builder.AppendFormat("[assembly: SuppressMessage(\"\", \"{0}\", Scope = \"module\")]\r\n", item.Trim());
                }
            }
            byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
            file.Write(bytes, 0, bytes.Length);
        }
    }

    private void OnGUI() {
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.Width(160f));
                EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandHeight(true));
                    EditorGUI.BeginDisabledGroup(!container.isCompleted);
                        scrollView1 = EditorGUILayout.BeginScrollView(scrollView1);
                            if (GUILayout.Button(GUIContentTemp.bt_save))
                                OnDisable();
                            if (GUILayout.Button(GUIContentTemp.bt_applayNoWar))
                                ApplayNoWarning(container);
                            if (GUILayout.Button(GUIContentTemp.bt_globalNoWar))
                                container.status = 0;
                            if (GUILayout.Button(GUIContentTemp.bt_IndividualNoWar))
                                container.status = 1;
                        EditorGUILayout.EndScrollView();
                    EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndVertical();
                GUILayout.Box(EditorGUIUtility.TrTempContent($"Timer: {container.timer}t"), GUIContentTemp.HelpBoxBlood);
            EditorGUILayout.EndVertical();
            EditorGUI.BeginDisabledGroup(!container.isCompleted);
                scrollView2 = EditorGUILayout.BeginScrollView(scrollView2);
                    switch (container.status) {
                        case 0:
                            DrawGlobalNoWar(container);
                            break;
                        case 1:
                            DrawIndividualNoWar(container);
                            break;
                    }
                EditorGUILayout.EndScrollView();
            EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
    }

    private void DrawIndividualNoWar(NoWarningContainer container) {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField(GUIContentTemp.bt_IndividualNoWar, EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();
        foreach (var item in container.IndividualNoWarning) {
            if (item.isVisible || container.showNoVisibles) {
                EditorGUI.BeginDisabledGroup(!item.isVisible);

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField(GUIContentTemp.lb_title, EditorStyles.boldLabel);
                ++EditorGUI.indentLevel;
                EditorGUILayout.LabelField(EditorGUIUtility.TrTempContent(item.assemblyDefinitionName));
                item.applay = EditorGUILayout.ToggleLeft(GUIContentTemp.tg_applayNoWar, item.applay);
                item.applayglobalNoWarning = EditorGUILayout.ToggleLeft(GUIContentTemp.tg_applayglobalNoWar, item.applayglobalNoWarning);
                EditorGUILayout.LabelField(GUIContentTemp.tbx_noWar);
                ++EditorGUI.indentLevel;
                EditorGUI.BeginDisabledGroup(!item.applay);
                if (item.applay)
                    item.NoWarning = EditorGUILayout.TextField(item.NoWarning);
                else _ = EditorGUILayout.TextField(container.globalNoWarning);
                EditorGUI.EndDisabledGroup();
                --EditorGUI.indentLevel;
                --EditorGUI.indentLevel;
                EditorGUILayout.EndVertical();

                EditorGUI.EndDisabledGroup();
            }
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawGlobalNoWar(NoWarningContainer container) {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField(GUIContentTemp.bt_globalNoWar, EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField(GUIContentTemp.lb_title, EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        EditorGUILayout.LabelField(GUIContentTemp.tbx_noWar);
        ++EditorGUI.indentLevel;
        container.globalNoWarning = EditorGUILayout.TextField(container.globalNoWarning);
        --EditorGUI.indentLevel;
        --EditorGUI.indentLevel;
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }

    private IEnumerator GetAssembles(NoWarningWindow container) {
        ListRequest search = Client.List(true);
        container.container.isCompleted = false;
        foreach (var item in container.container.IndividualNoWarning)
            item.isVisible = container.container.isCompleted;
        container.container.timer = 0UL;

        yield return new WaitWhile(() => {
            if (++container.container.timer % 30 == 0)
                container.Repaint();
            return !search.IsCompleted && !container.container.cancel;
        });
        if (!container.container.cancel) {
            string pj_path = Path.GetDirectoryName(Application.dataPath);
            string[] paths = new string[1];
            paths[0] = "Assets";

            foreach (var item2 in AssetDatabase.FindAssets($"t:{nameof(AssemblyDefinitionAsset)}", paths)) {
                paths[0] = AssetDatabase.GUIDToAssetPath(item2);
                if (container.container.ContainsAssembly(Path.GetFileName(paths[0]))) {
                    string name = Path.GetFileName(paths[0]);
                    int index = container.container.IndexOf(name);
                    container.container.IndividualNoWarning[index].isVisible = true;
                    container.container.IndividualNoWarning[index].assemblyDefinitionName = name;
                    container.container.IndividualNoWarning[index].assemblyDefinitionPath = paths[0];
                } else {
                    NoWarningContainer.IndivNoWar noWarning = new NoWarningContainer.IndivNoWar();
                    noWarning.assemblyDefinitionPath = paths[0];
                    noWarning.assemblyDefinitionName = Path.GetFileName(paths[0]);
                    container.container.IndividualNoWarning.Add(noWarning);
                }
            }

            foreach (var item in search.Result) {
                if (item.source != PackageSource.Embedded) continue;
                paths[0] = item.resolvedPath.Replace(pj_path, string.Empty).TrimStart('/', '\\');
                foreach (var item2 in AssetDatabase.FindAssets($"t:{nameof(AssemblyDefinitionAsset)}", paths)) {
                    paths[0] = AssetDatabase.GUIDToAssetPath(item2);
                    if (container.container.ContainsAssembly(Path.GetFileName(paths[0]))) {
                        string name = Path.GetFileName(paths[0]);
                        int index = container.container.IndexOf(name);
                        container.container.IndividualNoWarning[index].isVisible = true;
                        container.container.IndividualNoWarning[index].assemblyDefinitionName = name;
                        container.container.IndividualNoWarning[index].assemblyDefinitionPath = paths[0];
                    } else {
                        NoWarningContainer.IndivNoWar noWarning = new NoWarningContainer.IndivNoWar();
                        noWarning.assemblyDefinitionPath = paths[0];
                        noWarning.assemblyDefinitionName = Path.GetFileName(paths[0]);
                        container.container.IndividualNoWarning.Add(noWarning);
                    }
                }
            }

            foreach (var item in container.container.IndividualNoWarning) {
                string filePath = Path.Combine(pj_path, item.assemblyDefinitionPath);
                if (!File.Exists(filePath) && File.Exists($"{filePath}.nowar.cs"))
                    File.Delete($"{filePath}.nowar.cs");
            }
            container.container.isCompleted = true;
            container.Repaint();
        }
    }

    internal static class GUIContentTemp {
        internal static readonly GUIContent tbx_noWar = new GUIContent("No War", "Exp:IDE0076;IDE0051>~M:UnityEngine.Object.Destroy(UnityEngine.Object);");
        internal static readonly GUIContent bt_save = new GUIContent("Save", "Save the warning suppression settings.");
        internal static readonly GUIContent lb_title = new GUIContent("Warning Suppressor");
        internal static readonly GUIContent bt_applayNoWar = new GUIContent("Applay no warning", "Apply warning suppression.");
        internal static readonly GUIContent tg_applayNoWar = new GUIContent("Applay no warning", "Apply local warning suppressor.");
        internal static readonly GUIContent bt_globalNoWar = new GUIContent("Global no warning", "Global warning suppression settings.");
        internal static readonly GUIContent bt_IndividualNoWar = new GUIContent("Individual no warning", "Local warning suppression settings where each automaker can receive its own suppression.");
        internal static readonly GUIContent tg_applayglobalNoWar = new GUIContent("Applay global no warning",
            "If 'Apply no warning' is not checked, the global warning suppressor will be used.");
        private static GUIStyle helpBoxBlood = (GUIStyle)null;

        internal static GUIStyle HelpBoxBlood {
            get {
                if (helpBoxBlood == null) {
                    helpBoxBlood = new GUIStyle(EditorStyles.helpBox);
                    helpBoxBlood.fontSize = 12;
                    helpBoxBlood.fontStyle = FontStyle.Bold;
                    helpBoxBlood.alignment = TextAnchor.MiddleLeft;
                }
                return helpBoxBlood;
            }
        }
    }
}