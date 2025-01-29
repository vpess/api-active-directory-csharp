using ActiveDirectoryExplorer.Extensions;
using ActiveDirectoryExplorer.Models.Interfaces;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ActiveDirectoryExplorer.Models.Abstract
{
    [DataContract]
    public abstract class ActiveDirectoryObject : IActiveDirectoryObject
    {
        private string? _whenCreated;
        private string? _whenChanged;

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 4)]
        public string DistinguishedName { get; set; }

        [DataMember(Order = 9)]
        public string WhenCreated
        {
            get => _whenCreated;
            set
            {
                _whenCreated = value.BuildDateString();
            }
        }

        [DataMember(Order = 10)]
        public string WhenChanged
        {
            get => _whenChanged;
            set
            {
                _whenChanged = value.BuildDateString();
            }
        }
    }
}
