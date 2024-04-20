using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = InstanceMiniMapIcon.Instance.transform.rotation;
    }
}
