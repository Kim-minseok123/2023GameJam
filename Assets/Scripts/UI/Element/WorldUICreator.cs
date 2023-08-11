using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WorldUICreator : MonoBehaviour
{
    [SerializeField]
    private GameObject worldUIPrefab;
    [SerializeField]
    private bool useTargetFollow = false;
    [SerializeField]
    [ShowIf("useTargetFollow")]
    private Transform target;

    [SerializeField]
    private bool isUnique = false;
    private GameObject worldUIObject;

    public void Create()
    {
        if (isUnique && worldUIObject != null)
        {
            worldUIObject.SetActive(true);
            return;
        }

        worldUIObject = UIController.Instance.CreateWorldUI(worldUIPrefab);

        if (useTargetFollow)
        {
            var targetFollower = worldUIObject.GetComponent<UITargetFollower>();

            if (targetFollower != null)
            {
                targetFollower.SetTarget(target);
            }
        }
    }

    public void Hide()
    {
        if (worldUIObject == null)
        {
            return;
        }

        worldUIObject.SetActive(false);
    }

    public void Destroy()
    {
        if (worldUIObject == null)
        {
            return;
        }

        Destroy(worldUIObject);
    }

}
