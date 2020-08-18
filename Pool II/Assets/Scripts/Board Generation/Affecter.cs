using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AffecterSetting { Self, Children, All, WithName, Selected, None };
public delegate void ApplyDel<T>(T obj);

public class Affecter : MonoBehaviour
{
    public AffecterSetting affecterSetting;
    public List<Transform> selected_targets = new List<Transform>();
    public string targetName;

    public virtual List<T> GetTargets<T>() where T : Component
    {
        List<T> targets = new List<T>();
        switch (affecterSetting)
        {
            case AffecterSetting.Self:
                targets.Add(GetComponent<T>());
                break;
            case AffecterSetting.Children:
                targets.AddRange(gameObject.FindComponents<T>(in_parent: false, ignore_self: true));
                break;
            case AffecterSetting.All:
                targets.AddRange(GetComponentsInChildren<T>());
                break;
            case AffecterSetting.WithName:
                foreach (var child in transform.FindDeepChildren(targetName))
                    targets.Add(child.GetComponent<T>());
                break;
            case AffecterSetting.Selected:
                foreach (var selected in selected_targets)
                {
                    print(selected);
                    targets.Add(selected.GetComponent<T>());
                }
                break;
            case AffecterSetting.None:
                break;
        }
        return targets;
    }

    public virtual void Apply<T>(ApplyDel<T> application) where T : Component
    {
        foreach (var target in GetTargets<T>())
            application(target);
    }
}
