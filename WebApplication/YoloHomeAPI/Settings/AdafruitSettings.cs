namespace YoloHomeAPI.Settings
{
    public class AdafruitSettings
    {
        public string AdafruitUsername { get; set; } = null!;
        public string AdafruitKey { get; set; } = null!;
        
        public string AdafruitFeedName { get; set; } = null!;
        public string AdafruitAnnounceFeedName { get; set; } = null!;
        public string AdafruitSensorFeedName { get; set; } = null!;
        public string AdafruitLightFeedName { get; set; } = null!;
        public string AdafruitTemperatureFeedName { get; set; } = null!;
        public string AdafruitMoistureFeedName { get; set; } = null!;
        
        public string AnnounceTopicPath => AdafruitUsername + "/feeds/" + AdafruitFeedName + "." + AdafruitAnnounceFeedName;
        public string SensorTopicPath => AdafruitUsername + "/feeds/" + AdafruitFeedName + "." + AdafruitSensorFeedName;
        public string LightTopicPath => AdafruitUsername + "/feeds/" + AdafruitFeedName + "." + AdafruitLightFeedName;
        public string TemperatureTopicPath => AdafruitUsername + "/feeds/" + AdafruitFeedName + "." + AdafruitTemperatureFeedName;
        public string MoistureTopicPath => AdafruitUsername + "/feeds/" + AdafruitFeedName + "." + AdafruitMoistureFeedName;


    }
}