using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float _loadDelay = 1f; //Время задержки перед выполнением функции
    [SerializeField] AudioClip _crashSound; //Звук столкновения
    [SerializeField] AudioClip _successSound; //Звук фишина

    AudioSource _audioSource; //Компонент звука
    bool _isTransitioning; //Отвечает за состояние перехода(Finish/Crash)

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _isTransitioning = false;
    }

    void OnCollisionEnter(Collision other)
    {
        //Проверяем, выполняется ли у нас одно из состояний(Finish/Crash)
        //Если нет, то обрабатываем дальше столкновения
        //Если да, то прекращаем обрабатывать столкновения
        if (!_isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Friend":
                    Debug.Log("Friend");
                    break;
                case "Finish":
                    Finish();
                    break;
                default:
                    Crash();
                    break;
            }
        }
    }

    //Метод, отвечающий за обработку состояние финиша
    void Finish()
    {
        EnableTransition();
        _audioSource.PlayOneShot(_successSound); //Проиграть конкретный звук
    }

    //Метод, отвечающий за обработку состояние столкновения
    void Crash()
    {
        EnableTransition(); 
        _audioSource.PlayOneShot(_crashSound); //Проиграть конкретный звук
        Invoke("ReloadLevel", _loadDelay); //Вызывает функцию с задержкой
    }

    //Метод-помошник, отвечающий за переход состояния
    void EnableTransition()
    {
        _audioSource.Stop(); // Вырубить в данный момент проигрываемый звук
        _isTransitioning = true; // Установить переход
        GetComponent<PlayerMovement>().enabled = false; // Вырубить скрипт, отвечающий за управление
    }

    //Метод, отвечающий за перезагрузку уровня
    void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Получить индекс активной сцены
        SceneManager.LoadScene(currentSceneIndex); //Загрузить сцену
    }
}
