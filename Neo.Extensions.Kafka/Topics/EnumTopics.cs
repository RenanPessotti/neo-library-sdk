using System.ComponentModel;

namespace Neo.Extensions.Kafka.Topics
{
    public enum EnumTopics
    {
        [Description("first-topic")]
        FirstTopic,

        [Description("second-topic")]
        SecondTopic,

        [Description("third-topic")]
        ThirdTopic
    }
}
