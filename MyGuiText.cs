using UnityEngine;

[ExecuteInEditMode]
public class MyGuiText : MonoBehaviour
{
    public GUIStyle Estilo;
    public string Texto;
    public int TextSize;
    public float Largura;
    public float Altura;

    private void OnGUI()
    {
        var pos = FindObjectOfType<Camera>().WorldToScreenPoint(transform.position);

        var posiçãoRect = new Rect(pos.x, -pos.y + Screen.height, CalcularPosicaoRelativa.Width(Largura), CalcularPosicaoRelativa.Height(Altura));

        if (TextSize > 0)
            Estilo.fontSize = Screen.height / TextSize;

        if (!string.IsNullOrEmpty(Texto))
            GUI.Label(posiçãoRect, Texto, Estilo);
    }
}

