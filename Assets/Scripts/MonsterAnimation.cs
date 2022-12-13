using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public SkinnedMeshRenderer body;
    public Transform head;
    public bool yTargetHeadSynk;
    public Transform target;



    //--------------
    float blendTime = 0.4f;
    float towards = 5.0f;
    float weightMul = 1;
    float clampWeight = 0.5f;
    Vector3 weight = new Vector3(0.4f, 0.8f, 0.9f);

    Transform tr;
    Animator anim;
    AudioSource music;
    Vector3 lookAtTargetPosition, lookAtPosition;
    float lookAtWeight;
    float timeFaceCh, facepWeight = 100, timeFace = 10;
    bool faceCh = true;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Start()
    {
        //--------------

        tr = transform;
        anim = GetComponent<Animator>();
        music = GetComponent<AudioSource>();
        lookAtTargetPosition = target.position + tr.forward;
        lookAtPosition = head.position + tr.forward;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    void Update()
    {
        //--------------

        lookAtTargetPosition = target.position + tr.forward;

        //--------------

        if (faceCh == true && timeFace < Time.time)
        {
            timeFaceCh += Time.deltaTime * 80;
            if (timeFaceCh >= facepWeight * 2)
            {
                timeFaceCh = 0;
                faceCh = true;
                timeFace = Time.time + Random.Range(3.0f, 6.0f);
                music.pitch = Random.Range(0.8f, 1.0f);
            }
            float var0 = Mathf.PingPong(timeFaceCh, facepWeight);
            body.SetBlendShapeWeight(0, var0);
            music.volume = var0 * 0.05f;
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnAnimatorIK()
    {
        //--------------

        if (yTargetHeadSynk == false) lookAtTargetPosition.y = head.position.y;
        Vector3 curDir = lookAtPosition - head.position;
        curDir = Vector3.RotateTowards(curDir, lookAtTargetPosition - head.position, towards * Time.deltaTime, float.PositiveInfinity);
        lookAtPosition = head.position + curDir;
        lookAtWeight = Mathf.MoveTowards(lookAtWeight, 1, Time.deltaTime / blendTime);
        anim.SetLookAtWeight(lookAtWeight * weightMul, weight.x, weight.y, weight.z, clampWeight);
        anim.SetLookAtPosition(lookAtPosition);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}