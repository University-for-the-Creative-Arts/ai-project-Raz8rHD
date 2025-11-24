using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODMusicController : MonoBehaviour
{
    // The FMOD Event path for the music
    [Header("FMOD Settings")]
    [SerializeField]
    // Replaced [EventRef] string with the FMODUnity.EventReference struct to fix the CS0618 warning.
    private EventReference _musicEventReference; 

    // The name of the parameter in FMOD Studio you want to control (e.g., "Intensity")
    [SerializeField]
    private string _intensityParameter = "Intensity"; 

    private EventInstance _musicEventInstance;
    
    // Timing constants for the 3-phase cycle
    private const float RampDuration = 10.0f; // 10 seconds for ramp up and ramp down
    private const float HoldDuration = 10.0f;  // 10 seconds for hold
    private const float TotalCycleDuration = RampDuration + HoldDuration + RampDuration; // 30 seconds

    void Start()
    {
        // 1. Create the instance of the FMOD event using the EventReference struct
        // The event is now assigned via the Inspector using the EventReference property.
        _musicEventInstance = RuntimeManager.CreateInstance(_musicEventReference);

        // 2. Start the event
        _musicEventInstance.start();
        // FIXED: Removed the erroneous '.Path' accessor. The EventReference object 
        // will implicitly convert to the path string for logging.
        Debug.Log($"FMOD Music Event ({_musicEventReference}) started.");
    }

    void Update()
    {
        // Get the total time elapsed since the application started,
        // and loop it back using the modulo operator to create a 30-second cycle.
        float timeInCycle = Time.time % TotalCycleDuration;
        float intensityValue = 0.0f;

        // --- PHASE 1: RAMP UP (0 to 10 seconds) ---
        if (timeInCycle < RampDuration)
        {
            // Linearly map the time from 0 to 10 seconds to a value from 0.0 to 1.0
            intensityValue = timeInCycle / RampDuration;
        }

        // --- PHASE 2: HOLD (10 to 20 seconds) ---
        else if (timeInCycle < RampDuration + HoldDuration)
        {
            // Maintain the maximum value of 1.0 for the hold period
            intensityValue = 1.0f;
        }

        // --- PHASE 3: RAMP DOWN (20 to 30 seconds) ---
        else 
        {
            // Calculate the time elapsed within the ramp-down phase (0 to 10 seconds)
            float timeInPhase = timeInCycle - (RampDuration + HoldDuration);

            // Linearly map the time in phase (0 to 10) back down from 1.0 to 0.0
            intensityValue = 1.0f - (timeInPhase / RampDuration);

            // Optional: Clamp the value to ensure it never goes slightly negative due to floating point math
            intensityValue = Mathf.Max(0.0f, intensityValue);
        }

        // 4. Set the parameter on the FMOD event instance
        FMOD.RESULT result = _musicEventInstance.setParameterByName(_intensityParameter, intensityValue);

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError($"FMOD Error setting parameter {_intensityParameter}: {result}");
        }
    }

    void OnDestroy()
    {
        // 5. Clean up the FMOD event instance when the object is destroyed
        if (_musicEventInstance.isValid())
        {
            _musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _musicEventInstance.release();
        }
    }
}