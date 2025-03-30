using UnityEngine;
using System;
public class LPRunner : MonoBehaviour, ITaggable
{
    public event Action OnTaggedEvent;

    public void OnTagged()
    {
        OnTaggedEvent?.Invoke();
    }
}
