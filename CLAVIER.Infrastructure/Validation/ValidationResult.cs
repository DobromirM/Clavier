using System.Collections.Generic;

namespace CLAVIER.Infrastructure.Validation
{
    public class ValidationResult
    {
        public bool Success { get; set; }
        public IEnumerable<IValidationMessage> Messages { get; set; }
    }
}
