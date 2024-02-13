using UnityEditor.Compilation;
using UnityEngine;

public abstract class Panels
{
    public virtual void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
}
    