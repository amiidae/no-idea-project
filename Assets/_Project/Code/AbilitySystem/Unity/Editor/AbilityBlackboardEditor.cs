using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Code.AbilitySystem.Unity.Editor
{
    [CustomEditor(typeof(AbilityBlackboard))]
    public class AbilityBlackboardEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AbilityBlackboard abilityBlackboard = (AbilityBlackboard)target;

            using (new EditorGUI.DisabledScope(true))
            {
                DrawSection("Axes", abilityBlackboard.DebugAxes, (x, y) => EditorGUILayout.FloatField(x, y));
                DrawSection("Axes 2D", abilityBlackboard.DebugAxes2D, (x, y) => EditorGUILayout.Vector2Field(x, y));
                DrawSection("States", abilityBlackboard.DebugStates, (x, y) => EditorGUILayout.Toggle(x, y));
            }
        }
        
        public override bool RequiresConstantRepaint()
        {
            return Application.isPlaying;
        }

        private void DrawSection<TValue>(string header, IReadOnlyDictionary<int, TValue> values, Func<string, TValue, TValue> drawValue)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"{header} ({values.Count})", EditorStyles.boldLabel);

            if (values.Count == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Empty");
                EditorGUI.indentLevel--;
                return;
            }

            EditorGUI.indentLevel++;

            foreach (KeyValuePair<int, TValue> pair in values.OrderBy(x => x.Key))
            {
                drawValue(GetLabel(pair.Key), pair.Value);
            }

            EditorGUI.indentLevel--;
        }

        private string GetLabel(int id)
        {
            return $"{(ControlTypeId)id} ({id})";
        }
    }
}
