using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : Singleton<GameManager>
{
    public GameObject BG1;
    public GameObject BG2;

    public SteadyMove engine;
    public GameObject startingPoint;
    public Rope rope;
    public CameraFollow cameraFollow;

    public int  bgLoad {get; set;}

    Dude currentDude;
    public GameObject dude;
    public GameObject dudeBody;

    public bool isDudeDead = false;
    private bool gamePaused = true;

    public RectTransform mainMenu;
    public RectTransform gameplayMenu;

    public RectTransform gameOverMenu;

    private int score;

    // Значение true в этом свойстве требует игнорировать любые повреждения
    // (но показывать визуальные эффекты).
    // Объявление 'get; set;' превращает поле в свойство, что
    // необходимо для отображения в списке методов в инспекторе
    // для Unity Events
    public bool gnomeInvincible { get; set; }

    public float delayAfterDeath = 1.0f;
    public AudioClip dudeDiedSound;
    public AudioClip gameOverSound;

    public AudioClip[] bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        bgLoad = 30;
        StartCoroutine(LoadBg());

        //Reset();
        mainMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("High", 0).ToString();
        SetPaused(true);
        MusicLoad();
    }

    void MusicLoad()
    {
        int musicIndex = UnityEngine.Random.Range(0, bgMusic.Length);
        GetComponent<AudioSource>().clip = bgMusic[musicIndex];
    }

    private void Update()
    {
        if (!gamePaused)
        {
            engine.upwardSpeed += Time.deltaTime * 0.1f;

            if (TrapManager.instance.timerSet > 0.3f)
                TrapManager.instance.timerSet -= Time.deltaTime * 0.03f;
        }

        //        .Log(TrapManager.instance.timerSet);
        if (dude && dudeBody.transform.position.y > BG1.transform.position.y - 10)  //cam.transform.position.y * (1 - parallaxEffect)
        {
           // LoadBg();
        }

        score = (int)dudeBody.transform.position.y;
        gameplayMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = score.ToString();
        if (score > PlayerPrefs.GetInt("High", 0))
        {
            PlayerPrefs.SetInt("High", score);
            mainMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("High", 0).ToString();
        } 
    }

    private IEnumerator LoadBg()
    {
        for (int i = 0; i < bgLoad; i++)
        {
            BG1 = BG1.GetComponent<Parallax>().GenerateNext();
            BG2 = BG2.GetComponent<Parallax>().GenerateNext();
            yield return null;
        }
    }

    public void Reset()
    {
        if (gameOverMenu)
            gameOverMenu.gameObject.SetActive(false);
        if (mainMenu)
            mainMenu.gameObject.SetActive(false);
        if (gameplayMenu)
            gameplayMenu.gameObject.SetActive(true);

        var resetObjects = FindObjectsOfType<Resetable>();

        foreach (Resetable r in resetObjects)
        {
            r.Reset();
        }

        // Создать нового гномика
        //CreateNewGnome();
        // Прервать паузу в игре
        Time.timeScale = 1.0f;

    }


    private void RemoveGnome()
    {
        if (gnomeInvincible)
            return;
        rope.gameObject.SetActive(false);
        cameraFollow.target = null;

        if (currentDude != null)
        {
            currentDude.gameObject.tag = "Untagged";

            foreach (Transform child in currentDude.transform)
            {
                child.gameObject.tag = "Untagged";
            }
            // Установить признак отсутствия текущего гномика
            currentDude = null;
        }
    }

    void KillDude()
    {
        var audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.PlayOneShot(this.dudeDiedSound);
        }

        if (gnomeInvincible == false)
        {
            currentDude.DestroyDude();
            //RemoveGnome();
            StartCoroutine(ResetAfterDelay());
        }
    }

    IEnumerator ResetAfterDelay()
    {
        // Ждать delayAfterDeath секунд, затем вызвать Reset
        yield return new WaitForSeconds(delayAfterDeath);
        Reset();
    }

    // Вызывается, когда гномик касается ловушки
    // с ножами
    public void TrapTouched()
    {
        KillDude();
    }

    public void SetPaused(bool paused)
    {
        if (paused && !isDudeDead)
        {
            gamePaused = true;
            Time.timeScale = 0.0f;
            GetComponent<AudioSource>().Pause();
            mainMenu.gameObject.SetActive(true);
           // gameplayMenu.gameObject.SetActive(false);
        } else if (paused && isDudeDead)
        {
            gamePaused = true;
            Time.timeScale = 0.0f;
            GetComponent<AudioSource>().Pause();
            gameOverMenu.gameObject.SetActive(true);
            gameplayMenu.gameObject.SetActive(false);
        } else
        {
            gamePaused = false;
            Time.timeScale = 1.0f;
            GetComponent<AudioSource>().Play(); // устаревший но работает
            mainMenu.gameObject.SetActive(false);
            gameplayMenu.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Destroy(currentDude.gameObject);
        currentDude = null;
        // Сбросить игру в исходное состояние, чтобы создать нового гномика.
        Reset();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }

    public void MusicToggle(bool activate)
    {
        PlayerPrefs.SetInt("Music", boolToInt(activate));
        GetComponent<AudioSource>().enabled = activate;
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }
}

