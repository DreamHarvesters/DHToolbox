using DHToolbox.Runtime.DHToolboxAssembly;
using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;
using UnityEditor;
using UnityEngine;

namespace DHToolbox.Editor
{
    public class CreateResourceSetupEditorWindow : EditorWindow
    {
        private string idValue;
        private Sprite icon;
        private string uiName;


        [MenuItem("Tools/DH/Resource Setup Editor")]
        public static void ShowWindow()
        {
            GetWindow<CreateResourceSetupEditorWindow>("Resource Setup");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create Resource Setup", EditorStyles.boldLabel);

            idValue = EditorGUILayout.TextField("ID Value", idValue);
            icon = EditorGUILayout.ObjectField("Icon", icon, typeof(Sprite), false) as Sprite;
            uiName = EditorGUILayout.TextField("UI Name", uiName);

            if (GUILayout.Button("Create Resource Setup"))
            {
                CreateAsset();
            }
        }

        private void CreateAsset()
        {
            // Create Id asset
            Id idAsset = Id.From(idValue);
            string idAssetPath = $"{Constants.IdAssetsFolderPath}/{idValue}.asset";
            CreateFoldersRecursively(idAssetPath);
            AssetDatabase.CreateAsset(idAsset, idAssetPath);

            // Create ResourceSetup asset
            ResourceSetup resourceSetup = ResourceSetup.Create(idAsset, icon, uiName);
            string resourceSetupPath = $"{Constants.ResourceSetupsFolderPath}/{idValue}Setup.asset";
            CreateFoldersRecursively(resourceSetupPath);
            AssetDatabase.CreateAsset(resourceSetup, resourceSetupPath);

            // Move the ResourceSetup asset to the Resources folder
            AssetDatabase.MoveAsset(resourceSetupPath, $"{Constants.ResourceSetupsFolderPath}/{idValue}.asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Resource Setup created successfully!");
        }

        private void CreateFoldersRecursively(string assetPath)
        {
            string[] folders = assetPath.Split('/');
            string currentFolderPath = "Assets";

            for (int i = 1; i < folders.Length - 1; i++)
            {
                string folderName = folders[i];
                string folderPath = $"{currentFolderPath}/{folderName}";

                if (!AssetDatabase.IsValidFolder(folderPath))
                {
                    AssetDatabase.CreateFolder(currentFolderPath, folderName);
                }

                currentFolderPath = folderPath;
            }
        }
    }
}