using System;

namespace PwcWebApiDemo.Domain
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime StartFrom { get; set; }
    }
}
