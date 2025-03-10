using UnityEngine;

public class cloudtranslate : MonoBehaviour
{
    private float _speed = 2f;
    private float _endPosX;
    void Start()
    {
        
    }

    public void StartFloating(float speed, float endPosX)
    {
        _speed = speed;
        _endPosX = endPosX;

    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (transform.position.x >= _endPosX)
        {
            Destroy(gameObject);
        }
    }
}
