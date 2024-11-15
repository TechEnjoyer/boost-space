using System.Threading.Tasks;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 100; //Задает скорость поворота
    [SerializeField] float _boostValue = 100; //Задает силу полета
    [SerializeField] AudioClip _boostSound; //Звук полета


    Rigidbody _rb; //Rigidbody компонент
    AudioSource _audioSource; //Компонент звука
    RigidbodyConstraints _constraints; //Изначальное ограничение движения/вращения rigidbody

    //Unity-метод - выполняется 1 раз в начале игры
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _constraints = _rb.constraints;
    }

    //Unity-Метод - выполняется фиксированное количество раз
    //Использовать его только для Физики(rigidbody)
    void FixedUpdate()
    {
        ProcessRotation();
        ProcessBoost();
    }

    //Метод отвечающий за обработку поворота ракеты
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-_rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(_rotationSpeed);
        }
    }

    //Метод отвечающий за обработку полета ракеты
    void ProcessBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddRelativeForce(Vector3.up * _boostValue * Time.fixedDeltaTime);
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_boostSound);
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    //Метод отвечающий за применение вращения ракеты
    void ApplyRotation(float rotationSpeed)
    {
        _rb.freezeRotation = true; //отключаем вращение у rigidbody
        transform.Rotate(Vector3.right * rotationSpeed * Time.fixedDeltaTime);
        _rb.constraints = _constraints; //устанавливаем изначальные ограничения rigidbody
    }
}
