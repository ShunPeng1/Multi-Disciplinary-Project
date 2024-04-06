namespace YoloHomeAPI.Settings
{
    public class AdafruitSettings
    {
        public string AdafruitUsername { get; set; } = null!;
        public string AdafruitKey { get; set; } = null!;
        
        public string AdafruitGroupName { get; set; } = null!;
        public string AdafruitFanFeedName { get; set; } = null!;
        public string AdafruitDoorFeedName { get; set; } = null!;
        public string AdafruitLightFeedName { get; set; } = null!;
        public string AdafruitTemperatureFeedName { get; set; } = null!;
        public string AdafruitHumidityFeedName { get; set; } = null!;

        public string FanTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitFanFeedName;
        public string DoorTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitDoorFeedName;
        public string LightTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitLightFeedName;
        public string TemperatureTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitTemperatureFeedName;
        public string HumidityTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitHumidityFeedName;


    }
}