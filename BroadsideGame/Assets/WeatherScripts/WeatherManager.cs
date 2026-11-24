using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// RESPONSIBLE FOR WEATHER BEHAVIOUR, TO MAKE AN OBJECT INTERACT WITH IT NAME IT "Moveable" AND GIVE IT A RIGIDBODY
/// </summary>

public class WeatherManager : MonoBehaviour, WeatherEvents
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject moveableObject;
    private bool playWindLeftBool = false;
    private bool playWindRightBool = false;
    private bool playRainBool = false;
    private bool clearSkiesBool = false;
    private bool fireOnce = false;

    //Particle block references
    [SerializeField] private ParticleSystem windParticlesLeft;
    [SerializeField] private ParticleSystem windParticlesRight;
    [SerializeField] private ParticleSystem rainParticles;

    //below variables are not required in the script but add a level of user customisation
    [SerializeField] private int averageWeatherDuration = 4;
    [SerializeField] private int timeBetweenWeatherEvent = 2;

    private int randomNumber = 0;


    void Start()
    {
        //sets particles to false at start so they dont just start playing 
        if (windParticlesLeft != null && windParticlesRight != null && rainParticles != null)
        {
            windParticlesLeft.gameObject.SetActive(false);
            windParticlesRight.gameObject.SetActive(false);
            rainParticles.gameObject.SetActive(false);
        }

        WeatherChanceEngine(); //runs the chance engine at the beggining of the game 
    }

    // Update is called once per frame
    void Update()
    {
        #region particle Engine
        //RAIN PARTICLES
        if (fireOnce == true && playRainBool == true) //fires exactly once to turn left wind particles on and not waste memory
        {
            rainParticles.gameObject.SetActive(true);
            Debug.Log("Rain turned on");

            StartCoroutine(TimeBetweenEvents()); //calls the next weather pattern countdown
            fireOnce = false;
        }

        //LEFT WIND PARTICLES
        if (fireOnce == true && playWindLeftBool == true) //fires exactly once to turn left wind particles on and not waste memory
        {
            windParticlesLeft.gameObject.SetActive(true);
            Debug.Log("Wind Left turned on");

            StartCoroutine(TimeBetweenEvents()); //calls the next weather pattern countdown
            fireOnce = false;
        }

        //RIGHT WIND PARTICLES
        if (fireOnce == true && playWindRightBool == true) //fires exactly once to turn right wind particles on and not waste memory
        {
            windParticlesRight.gameObject.SetActive(true);
            Debug.Log("Wind Right turned on");

            StartCoroutine(TimeBetweenEvents()); //calls the next weather pattern countdown
            fireOnce = false;
        }
        //CLEAR SKIES
        if (fireOnce == true && clearSkiesBool == true) //in case of clear skies
        {
            Debug.Log("Clear Skies Called");

            StartCoroutine(TimeBetweenEvents()); //calls the next weather pattern countdown
            fireOnce = false;
        }
        #endregion

        #region per Frame Behaviours
        if (playRainBool == true) //runs intended behaviour of rain per frame
        {
            PlayRain();
        }
        if (playWindLeftBool == true) //runs intended behaviour of wind Left per frame
        {
            PlayWindLeft();
        }
        if (playWindRightBool == true) //runs intended behaviour of wind Right per frame
        {
            PlayWindRight();
        }
        //THERE ARE NO CLEAR SKIES PER FRAME BEHAVIOUR SO NO IF STATEMENT FOR IT
        #endregion





    }

    private void WeatherChanceEngine()
    {
        randomNumber = UnityEngine.Random.Range(1, 10);
        if (randomNumber >= 5) { //if random is greater than or equal to 5 (50% chance of rain)
            playRainBool = true;
            fireOnce = true; //calls fire once for particles so it can fire once
            Debug.Log(randomNumber);

        }
        else if (randomNumber == 1) //if random is exactly 1 
        {
            clearSkiesBool = true;
            fireOnce = true;
            Debug.LogWarning(randomNumber);
            PlayClearSkies();
        }
        else if (randomNumber < 3) //if random is less than 3 (only happens if its 2)
        {
            playWindLeftBool = true;
            fireOnce = true; //calls fire once for particles so it can fire once
            Debug.Log(randomNumber);
    

        }
        else if(randomNumber < 5) //if random is less than 5 (only happens if number is 4 or 3)
        {
            playWindRightBool = true;
            fireOnce = true; //calls fire once for particles so it can fire once
            Debug.Log(randomNumber);

        }
    }

    IEnumerator TimeBetweenEvents()
    {
        Debug.Log("Coroutine Called");
        yield return new WaitForSeconds((randomNumber * 2) + averageWeatherDuration); //waits for a random time depending on the event * 2 for extra length with a minimum of 4 seconds (also waits 0 seconds at start to call immediately)

        //clears all current weather events
        #region weather event clear
        playWindLeftBool = false;
        playWindRightBool = false;
        playRainBool = false;
        if (windParticlesLeft != null && windParticlesLeft != null && rainParticles != null) //collective nullcheck just to make sure everything is assigned
        {
            windParticlesLeft.gameObject.SetActive(false);
            windParticlesRight.gameObject.SetActive(false);
            rainParticles.gameObject.SetActive(false);
        }
        #endregion

        yield return new WaitForSeconds(timeBetweenWeatherEvent); // a second smaller pause before continuing on the loop and firing your next weather event

        //runs weather chance engine again so loop can be established
        WeatherChanceEngine(); 
        Debug.Log("Coroutine Fired");
    }

    //remove below debug logs to get rid of per frame calling
    private void PlayWindLeft()
    {
        moveableObject = GameObject.Find("Moveable");
        Rigidbody rb = moveableObject.GetComponent<Rigidbody>();
        //important lines, references moveableObject through name and then finds it's rigidbody

        if (moveableObject != null) //checks if there is indeed a moveable
        {
            if (rb != null) //checks if the moveable has a rigidbody
            {
                rb.AddForce(Vector3.back * 2f); //LINE CONTROLS DIRECTION OF WIND CHANGE IF YOU WANT A DIFF DIRECTION
                //Debug.Log("wind left playing");
            }
        }
    }//Left Wind Behaviour

    private void PlayWindRight()
    {
        moveableObject = GameObject.Find("Moveable");
        Rigidbody rb = moveableObject.GetComponent<Rigidbody>();
        //important lines, references moveableObject through name and then finds it's rigidbody

        if (moveableObject != null) //checks if there is indeed a moveable
        {
            if (rb != null) //checks if the moveable has a rigidbody
            {
                rb.AddForce(Vector3.forward * 2f); //LINE CONTROLS DIRECTION OF WIND CHANGE IF YOU WANT A DIFF DIRECTION
                //Debug.Log("wind right playing");
            }
        }
    }//Right Wind Behaviour


    private void PlayRain()
    {
        //put future behaviour of rain in here
       // Debug.Log("rain playing");
    }

    private void PlayClearSkies()
    {
        playWindLeftBool = false;
        playWindRightBool = false;
        playRainBool = false;
        Debug.Log("clear skies"); //debug only calls once as it is not per frame behaviour so don't worry about disabiling this log
    }



    //below are the GETTER methods to access from outside the script the current state of each weather event
    public bool getIsWindLeftPlaying()
    {
        return playWindLeftBool;
    }
    public bool getIsWindRightPlaying()
    {
        return playWindRightBool;
    }
    public bool getIsRainPlaying()
    {
        return playRainBool;
    }
    public bool getIsClearSkies()
    {
        return clearSkiesBool;
    }

}
