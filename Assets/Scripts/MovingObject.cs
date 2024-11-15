using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] Vector3 _movementVector; //Вектор на который хотим переместить обьект
    [SerializeField][Range(1, 3)] float _speed; //Скорость перемещения
    
    Vector3 _startPosition; //Стартовая позиция
    Vector3 _endPosition; //Конечная позиция
    float _movementFactor; //Фактор перемещения

    void Start()
    {
        _startPosition = transform.position; //Устанавливаем изначальное положение
        _endPosition = _startPosition + _movementVector; //Устанавливаем конечное положение
    }

    void Update()
    {
        _movementFactor = Mathf.PingPong(Time.time * _speed, 1f);
        transform.position = Vector3.Lerp(_startPosition, _endPosition, _movementFactor);
    }
}
