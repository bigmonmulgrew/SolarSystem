using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Body), true)]
public class BodyEditor : Editor
{
    const string CELESTIAL_SETTINGS_FILE_NAME = "CelestialSettings";

    CelestialSettings celestialSettings;

    private void OnEnable()
    {
        ResolveSettings();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawLocalSettings();
        EditorGUILayout.Space(10);
        DrawGlobalTimeSettings();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawGlobalTimeSettings()
    {
        EditorGUILayout.LabelField("Global Time Settings", EditorStyles.boldLabel);

        if (!celestialSettings)
        {
            EditorGUILayout.HelpBox($"{CELESTIAL_SETTINGS_FILE_NAME} asset not found.", MessageType.Error);
            if (GUILayout.Button("Create Settings Asset")) CreateSettingsAsset();
            return;
        }

        Editor editor = CreateEditor(celestialSettings);
        editor.OnInspectorGUI();
    }

    private void DrawLocalSettings()
    {
        EditorGUILayout.LabelField("Local Body Settings", EditorStyles.boldLabel);
        DrawPropertiesExcluding(
            serializedObject,
            "timeSettings"              // hide internal reference
        );
    }

    private void ResolveSettings()
    {
        var guids = AssetDatabase.FindAssets($"t:{CELESTIAL_SETTINGS_FILE_NAME}");
        if (guids.Length == 0) return;

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        celestialSettings = AssetDatabase.LoadAssetAtPath<CelestialSettings>(path);

        // assign automatically
        var body = target as Body;
        if (!body) return;

        var so = new SerializedObject(body);
        so.FindProperty("celestialSettings").objectReferenceValue = celestialSettings;
        so.ApplyModifiedPropertiesWithoutUndo();
    }

    private void CreateSettingsAsset()
    {
        celestialSettings = ScriptableObject.CreateInstance<CelestialSettings>();
        AssetDatabase.CreateAsset(celestialSettings, $"Assets/Settings/{CELESTIAL_SETTINGS_FILE_NAME}.asset");
        AssetDatabase.SaveAssets();
        ResolveSettings();
    }
}
