namespace YoloHomeAPI.Settings
{
    public class AdafruitSettings
    {
        public string AdafruitUsername { get; set; } = null!;
        public string AdafruitKey { get; set; } = null!;
        
        public string AdafruitGroupName { get; set; } = null!;
        public string AdafruitAnnounceFeedName { get; set; } = null!;
        public string AdafruitSensorFeedName { get; set; } = null!;
        public string AdafruitLightFeedName { get; set; } = null!;
        public string AdafruitTemperatureFeedName { get; set; } = null!;
        public string AdafruitMoistureFeedName { get; set; } = null!;
        
        public string AnnounceTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitAnnounceFeedName;
        public string SensorTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitSensorFeedName;
        public string LightTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitLightFeedName;
        public string TemperatureTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitTemperatureFeedName;
        public string MoistureTopicPath => AdafruitUsername + "/feeds/" + AdafruitGroupName + "." + AdafruitMoistureFeedName;


    }
}