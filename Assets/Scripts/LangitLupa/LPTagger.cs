using UnityEngine;
using System;
using System.Collections;

public interface ITaggable
{
    void OnTagged();
}
public class LPTagger : MonoBehaviour
{
    public float tagRange = 2f;
    public KeyCode tagKey = KeyCode.E;
    private bool canTag = true;
    private float tagCooldown = 2f;

    public event Action<GameObject> OnTagSuccess;
    public LPUIManager uiManager; // Assign in Inspector

    private void Start()
    {
        OnTagSuccess += (taggedObject) => uiManager.ShowTagAlert($"Tagged: {taggedObject.name}!");
    }

    private void Update()
    {
        if (canTag && Input.GetKeyDown(tagKey))
        {
            AttemptTag();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tagRange);
    }
    private void AttemptTag()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, tagRange);
        foreach (var hit in hitColliders)
        {
            if (hit.gameObject.TryGetComponent<ITaggable>(out ITaggable taggable))
            {
                Debug.Log($"Tagged: {hit.gameObject.name}");
                taggable.OnTagged();
                OnTagSuccess?.Invoke(hit.gameObject);
                StartCoroutine(TagCooldown());
                break;
            }
        }
    }

    private IEnumerator TagCooldown()
    {
        canTag = false;
        yield return new WaitForSeconds(tagCooldown);
        canTag = true;
    }
}