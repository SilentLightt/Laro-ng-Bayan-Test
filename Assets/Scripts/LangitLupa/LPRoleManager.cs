using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class LPRoleManager : MonoBehaviour
{
    public GameObject currentTaya;
    public List<GameObject> runners = new List<GameObject>();
    private float transitionDelay = 2f;

    public void InitializeRoles(List<GameObject> players, GameObject initialTaya)
    {
        currentTaya = initialTaya;
        runners = new List<GameObject>(players);
        runners.Remove(initialTaya);
    }

    public void SwitchRoles(GameObject newTaya)
    {
        StartCoroutine(HandleRoleTransition(newTaya));
    }

    private IEnumerator HandleRoleTransition(GameObject newTaya)
    {
        GameObject previousTaya = currentTaya;
        currentTaya = null;
        yield return new WaitForSeconds(transitionDelay);
        currentTaya = newTaya;
        runners.Remove(newTaya);
        runners.Add(previousTaya);
    }
}
