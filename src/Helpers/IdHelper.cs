using System;

namespace myBURGUERMANIA_API.Helpers
{
    public static class IdHelper
    {
        public static string GenerateRandomId()
        {
            return Guid.NewGuid().ToString(); // Gerar GUID como string
        }
    }
}
