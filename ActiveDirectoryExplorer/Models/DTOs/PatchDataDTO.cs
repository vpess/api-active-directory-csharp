using ActiveDirectoryExplorer.Models.Enums;

namespace ActiveDirectoryExplorer.Models.DTOs
{
    public class PatchDataDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ActiveDirectoryOperation Action { get; set; }
        public string Group { get; set; }
        public string Domain { get; set; }
    }
}
