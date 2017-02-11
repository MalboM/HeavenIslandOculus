using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{

    // Use this for initialization
    public int shells;
    public int apples;
    public GameObject variabiligameobject;

    void Start()
    {
        if (SteamManager.Initialized)
        {


            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SteamManager.Initialized)
        {


            if (shells == 1)
                SteamUserStats.SetAchievement("Shell Primal collector");

            if (shells == 5)
                SteamUserStats.SetAchievement("Shell Great collector");

            if (shells == 15)
                SteamUserStats.SetAchievement("Shell Zen collector");


            if (shells == 30)
                SteamUserStats.SetAchievement("Shell Meditation collector");

            if (shells == 60)
                SteamUserStats.SetAchievement("Shell Master collector");


            if (shells == 70)
               SteamUserStats.SetAchievement("Shell Master Zen collector");


            //APPLES
            if (apples == 1)
                SteamUserStats.SetAchievement("Apple Primal collector");

            if (apples == 5)
                SteamUserStats.SetAchievement("Apple Great collector");

            if (apples == 15)
                SteamUserStats.SetAchievement("Apple Zen collector");


            if (apples == 30)
                SteamUserStats.SetAchievement("Apple Meditation collector");

            if (apples == 60)
                SteamUserStats.SetAchievement("Apple Master collector");

            if (apples == 70)
                SteamUserStats.SetAchievement("Apple Master Zen collector");
        }

        shells = variabiligameobject.GetComponent<Actions>().shellraccolte;

        apples = variabiligameobject.GetComponent<Actions>().appleraccolte;
    }
}
