using System.Collections.Generic;

namespace Financial.Chat.Domain.Shared.Entity
{
    public class ApiBadReturn
    {
        public bool success { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}
