using UnityEngine;

namespace Runtime.Data
{
    public class User
    {
        public string UserName
        {
            get => PlayerPrefs.GetString("UserName", "DefaultName");
            set
            {
                PlayerPrefs.SetString("UserName", value);
                PlayerPrefs.Save();
            }
        }
    }
}