using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MyGuiButtom))]
public class MyGuiButtomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var botao = target as MyGuiButtom;

        if (GUILayout.Button("Default Collider size"))
            botao.CorrigirBoxCollider();

        //botao.TipoBotão = (MyGuiButtom.ButtomType) EditorGUILayout.EnumPopup("Tipo de Botão", botao.TipoBotão);
        //botao.SomClick = EditorGUILayout.ObjectField("Som click", botao.SomClick, botao.SomClick.GetType(), true) as AudioClip;

        //switch (botao.TipoBotão)
        //{
        //    case MyGuiButtom.ButtomType.TrocaSprites:
        //        botao.Pressed = EditorGUILayout.ObjectField("Ao clicar", botao.Pressed, typeof(Sprite), false) as Sprite;
        //        break;
        //    case MyGuiButtom.ButtomType.MudaCores:
        //        botao.CorPressionado = EditorGUILayout.ColorField("Ao clicar", botao.CorPressionado);
        //        break;
        //    default:
        //        throw new ArgumentOutOfRangeException();
        //}

        base.OnInspectorGUI();
    }
}
