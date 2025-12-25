using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BodyRotation))]
public class BodyRotationEditor : Editor
{
    const string TIME_SETTINGS_FILE_NAME = "CelestialTimeSettings";

    CelestialTimeSettings timeSettings;

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

        if (!timeSettings)
        {
            EditorGUILayout.HelpBox($"{TIME_SETTINGS_FILE_NAME} asset not found.", MessageType.Error);
            if (GUILayout.Button("Create Settings Asset")) CreateSettingsAsset();
            return;
        }

        Editor editor = CreateEditor(timeSettings);
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
        var guids = AssetDatabase.FindAssets($"t:{TIME_SETTINGS_FILE_NAME}");
        if (guids.Length == 0) return;

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        timeSettings = AssetDatabase.LoadAssetAtPath<CelestialTimeSettings>(path);

        // assign automatically
        var body = (BodyRotation)target;
        if (!body) return;

        var so = new SerializedObject(body);
        so.FindProperty("timeSettings").objectReferenceValue = timeSettings;
        so.ApplyModifiedPropertiesWithoutUndo();
    }

    private void CreateSettingsAsset()
    {
        timeSettings = ScriptableObject.CreateInstance<CelestialTimeSettings>();
        AssetDatabase.CreateAsset(timeSettings, $"Assets/Settings/{TIME_SETTINGS_FILE_NAME}.asset");
        AssetDatabase.SaveAssets();
        ResolveSettings();
    }
}
