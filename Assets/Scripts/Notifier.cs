using System;
using Unity.Notifications.Android;


public static class Notifier
{
    public static void MakeNotification(DateTime whenCreate, string title, string message)
    {
#if UNITY_ANDROID
        string id = "AndroidNotifierId";

        var androidSettingsChanel = new AndroidNotificationChannel
        {
            Id = id,
            Name = "Reward Channel",
            Description = "Test channel for rewards",
            Importance = Importance.High,
            CanBypassDnd = true,
            CanShowBadge = true,
            EnableLights = true,
            EnableVibration = true,
            LockScreenVisibility = LockScreenVisibility.Public
        };

        AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChanel);

        var androidSettingsNotification = new AndroidNotification
        {
            FireTime = whenCreate,
            Title = title,
            Text = message,
            ShowTimestamp = true,
            ShouldAutoCancel = true,
        };

        AndroidNotificationCenter.SendNotification(androidSettingsNotification, id);
    }
#endif

}
