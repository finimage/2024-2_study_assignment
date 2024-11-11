using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int a;
    public int b;
    public int i = 0;
    [SerializeField]
    private int c;

    public GameObject sphere;

    private void Awake()
    {

    }
    void Start()
    {
        sphere = GameObject.Find("Sphere");
        sphere.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
