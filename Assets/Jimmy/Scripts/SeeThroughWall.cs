using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWall : MonoBehaviour
{
    public GameObject myCamera;
    public GameObject target;

    public GameObject blue;
    public GameObject blueTransparent;
    public GameObject green;
    public GameObject greenTransparent;
    public GameObject grey;
    public GameObject greyTransparent;
    public GameObject orange;
    public GameObject orangeTransparent;
    public GameObject red;
    public GameObject redTransparent;
    public GameObject pink;
    public GameObject pinkTransparent;


    Material blueMat;
    Material blueTransparentMat;
    Material greenMat;
    Material greenTransparentMat;
    Material greyMat;
    Material greyTransparentMat;
    Material orangeMat;
    Material orangeTransparentMat;
    Material redMat;
    Material redTransparentMat;
    Material pinkMat;
    Material pinkTransparentMat;

    private List<GameObject> raycastResults;
    private List<Material> raycastResultMaterials;

    private void Awake()
    {
        raycastResults = new List<GameObject>();
        raycastResultMaterials = new List<Material>();

        blueMat = blue.GetComponent<MeshRenderer>().material;
        blueTransparentMat = blueTransparent.GetComponent<MeshRenderer>().material;
        greenMat = green.GetComponent<MeshRenderer>().material;
        greenTransparentMat = greenTransparent.GetComponent<MeshRenderer>().material;
        greyMat = grey.GetComponent<MeshRenderer>().material;
        greyTransparentMat = greyTransparent.GetComponent<MeshRenderer>().material;
        orangeMat = orange.GetComponent<MeshRenderer>().material;
        orangeTransparentMat = orangeTransparent.GetComponent<MeshRenderer>().material;
        redMat = red.GetComponent<MeshRenderer>().material;
        redTransparentMat = redTransparent.GetComponent<MeshRenderer>().material;
        pinkMat = pink.GetComponent<MeshRenderer>().material;
        pinkTransparentMat = pinkTransparent.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(myCamera.transform.position, target.transform.position);
            Ray ray = new Ray(myCamera.transform.position, (target.transform.position - myCamera.transform.position).normalized);
            RaycastHit[] hits = Physics.RaycastAll(myCamera.transform.position, (target.transform.position - myCamera.transform.position).normalized, distance);

            //Debug.Log(hits[0].collider.gameObject);

            if (raycastResults != null)
            {
                foreach (GameObject gameObject in raycastResults)
                {
                    bool same = false;

                    for (int i = 0; i < hits.Length; ++i)
                    {
                        if (hits[i].collider.gameObject == gameObject)
                        {
                            same = true;
                            Debug.Log(same);
                        }
                    }

                    if (!same)
                    {
                        int index = GetIndexOfObjectInList(gameObject, raycastResults);
                        Debug.Log(raycastResults[index]);
                        if (index >= 0)
                        {
                            raycastResults[index].GetComponent<MeshRenderer>().material = raycastResultMaterials[index];
                        }
                    }
                }
            }

            //raycastResults = new List<GameObject>();
            //raycastResultMaterials = new List<Material>();

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Wall")
                {

                    if (hit.collider.gameObject.GetComponent<MeshRenderer>().material.color == blueMat.color)
                    {
                        raycastResults.Add(hit.collider.gameObject);
                        raycastResultMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().material);
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = blueTransparentMat;
                    }
                    else if (hit.collider.gameObject.GetComponent<MeshRenderer>().material.color == greenMat.color)
                    {
                        raycastResults.Add(hit.collider.gameObject);
                        raycastResultMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().material);
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = greenTransparentMat;

                    }
                    else if (hit.collider.gameObject.GetComponent<MeshRenderer>().material.color == greyMat.color)
                    {
                        raycastResults.Add(hit.collider.gameObject);
                        raycastResultMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().material);
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = greyTransparentMat;
                    }
                    else if (hit.collider.gameObject.GetComponent<MeshRenderer>().material.color == orangeMat.color)
                    {
                        raycastResults.Add(hit.collider.gameObject);
                        raycastResultMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().material);
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = orangeTransparentMat;
                    }
                    else if (hit.collider.gameObject.GetComponent<MeshRenderer>().material.color == redMat.color)
                    {
                        raycastResults.Add(hit.collider.gameObject);
                        raycastResultMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().material);
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = redTransparentMat;
                    }
                    else if (hit.collider.gameObject.GetComponent<MeshRenderer>().material.color == pinkMat.color)
                    {
                        raycastResults.Add(hit.collider.gameObject);
                        raycastResultMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().material);
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = pinkTransparentMat;
                    }
                }
            }
        }
    }

    private int GetIndexOfObjectInList(GameObject gameObject, List<GameObject> array)
    {
        int index = -1;
        for (int i = 0; i < array.Count; ++i)
        {
            if (gameObject == array[i])
            {
                index = i;
            }
        }
        return index;
    }
}
