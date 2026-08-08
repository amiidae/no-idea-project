
using Code.AbilitySystem.Core;
using UnityEditor;
using UnityEngine;

namespace Code.AbilitySystem.Unity.Editor
{
    [CustomEditor(typeof(AbilityUser))]
    public class AbilityUserEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            AbilityUser abilityUser = (AbilityUser)target;

            if (abilityUser.Layers == null)
            {
                return;
            }

            EditorGUILayout.LabelField($"Layers ({abilityUser.Layers.Count})", EditorStyles.boldLabel);

            for (int i = 0; i < abilityUser.Layers.Count; i++)
            {
                DrawLayer(i, abilityUser.Layers[i]);
            }
        }

        public override bool RequiresConstantRepaint()
        {
            return Application.isPlaying;
        }

        private void DrawLayer(int index, AbilityLayer abilityLayer)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.LabelField($"Layer {index}", $"Active: {GetName(abilityLayer.ActiveAbility)}");

            EditorGUI.indentLevel++;

            foreach (IAbility ability in abilityLayer.Abilities)
            {
                bool isActive = ability == abilityLayer.ActiveAbility;

                EditorGUILayout.LabelField(isActive ? $"> {GetName(ability)}" : $"   {GetName(ability)}",
                    isActive ? EditorStyles.boldLabel : EditorStyles.label);
            }

            EditorGUI.indentLevel -= 2;
        }

        private string GetName(IAbility ability)
        {
            return ability == null ? "None" : ability.GetType().Name;
        }
    }
}
