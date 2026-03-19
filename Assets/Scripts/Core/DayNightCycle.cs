using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float dayDuration = 120f; // Duration of a full day in seconds
    private float timeOfDay = 0f;

    void Update()
    {
        // Increment time of day
        timeOfDay += Time.deltaTime / dayDuration * 360f; // Convert to degrees
        if (timeOfDay >= 360f)
        {
            timeOfDay -= 360f; // Loop back to the start of the day
        }
        // Rotate the sun based on time of day
        sun.transform.rotation = Quaternion.Euler(new Vector3(timeOfDay - 90f, 170f, 0f));
    }
}
