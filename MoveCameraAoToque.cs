using UnityEngine;

public class MoveCameraAoToque : MonoBehaviour
{
    private const float Speed = 10f;
    private const float LerpSpeed = 0.5f;

    private const float LimiteEsquerdo = 1f;
    private const float LimiteDireito = 5.5f;

    private Vector3 _posCamera;

    private void Start()
    {
        _posCamera = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _posCamera, LerpSpeed);

        if (_posCamera.x < LimiteEsquerdo)
            _posCamera = new Vector3(1, _posCamera.y, _posCamera.z);

        if (_posCamera.x > LimiteDireito)
            _posCamera = new Vector3(5.5f, _posCamera.y, _posCamera.z);

        if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Moved)
            return;  

        var distanciaMovimentada = Input.GetTouch(0).deltaPosition;

        var posNova = new Vector3(
            x: _posCamera.x + -(distanciaMovimentada.x/Speed),
            y: _posCamera.y,
            z: _posCamera.z
            );

        _posCamera = Vector3.Lerp(_posCamera, posNova, LerpSpeed);
    }
}
