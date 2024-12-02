using System.Runtime.Serialization;

namespace Siruis_Project.Core.Entities
{
    public enum Plan
    {
        [EnumMember(Value ="Nova")]
        Nova,
        [EnumMember(Value = "pulsar")]
        Pulsar,
        [EnumMember(Value = "Stellar Cluster")]
        stellarCluster,
        [EnumMember(Value = "Nebula")]
        Nebula

    }
}