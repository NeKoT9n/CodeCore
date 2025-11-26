using Assets.CodeCore.Scripts.Game.Helpers;
using Assets.CodeCore.Scripts.Game.Services;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(10);
     
            LevelData levelData = (LevelData)target;
      
            if (GUILayout.Button("Find All Spawn Points in Scene", GUILayout.Height(30)))
            {
                FindAndAssignPoints(levelData);
            }
        }

        private void FindAndAssignPoints(LevelData levelData)
        {
    
            SpawnPointMarker[] foundPoints = FindObjectsByType<SpawnPointMarker>(FindObjectsSortMode.None);
            Undo.RecordObject(levelData, "Assign Spawn Points");

            List<SpawnPoint> spawnPoints = new();
            
            foreach(var point in foundPoints)
            {
                spawnPoints.Add(new(point.Position, point.EntityType));
            }
            levelData.SetSpawnPoints(spawnPoints);     
            EditorUtility.SetDirty(levelData);

            Debug.Log($"Successfully found and assigned {foundPoints.Length} spawn points!");
        }
    }
}
