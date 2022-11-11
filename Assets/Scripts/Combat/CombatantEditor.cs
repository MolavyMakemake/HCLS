using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Combatant))]
public class CombatantEditor : Editor
{
    Combatant combatant;

    private void OnEnable()
    {
        combatant = (Combatant)target;
    }

    protected void OnSceneGUI()
    {
        base.OnInspectorGUI();

        Handles.color = Color.green;
        Handles.DrawWireDisc(combatant.transform.position, Vector3.up, combatant.hbRadius, 0.2f);
    }
}

[CustomEditor(typeof(Player))]
public class PlayerEditor : CombatantEditor
{

}

[CustomEditor(typeof(Raider))]
public class RaiderEditor : CombatantEditor
{
}


