using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnByBoundary : MonoBehaviour {

    void Update()
    {

        if (transform.position.x < BoundaryInformation.xMin)
        {
            transform.position = new Vector3(BoundaryInformation.xMax, Random.Range(BoundaryInformation.yMin, BoundaryInformation.yMax), 0);
        }
        if (transform.position.x > BoundaryInformation.xMax)
        {
            transform.position = new Vector3(BoundaryInformation.xMin, Random.Range(BoundaryInformation.yMin, BoundaryInformation.yMax), 0);
        }
    }
}
