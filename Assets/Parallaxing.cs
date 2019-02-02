
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] background;
    private float[] parallaxScale;
    public float smoothing= 1f;

    private Transform cam;
    private Vector3 previousCamPos;


    void Awake()
    {
    
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;
        parallaxScale = new float[background.Length];
        for (int i =0; i < background.Length; i++)
        {
            parallaxScale[i] = background[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0;i<background.Length; i++)
        {

            float parallax = (previousCamPos.x - cam.position.x) * parallaxScale[i];
            float backgroundTargetPosX = background[i].position.x + parallax;
            Vector3 backgoundTargetPos = new Vector3 (backgroundTargetPosX, background[i].position.y, background[i].position.z);
            background[i].position = Vector3.Lerp(background[i].position, backgoundTargetPos,smoothing * Time.deltaTime);
                          
        }
        previousCamPos = cam.position;
    }
}
