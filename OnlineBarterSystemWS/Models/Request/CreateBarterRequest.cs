using OnlineBarterSystemWS.Generic.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace OnlineBarterSystemWS.Models.Request
{
    public class CreateBarterRequest : AEntityRequest, IValidatableObject
    {
        public long InitiatorId { get; set; }
        public long GiveTypeId { get; set; }
        public long ReceiveTypeId { get; set; }
        public string? Description { get; set; }
        public double? GiveValue { get; set; }
        public double? ReceiveValue { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GiveValue <= 0)
            {
                yield return new ValidationResult(
                    $"Give Value should be greater than 0.",
                    new[] { nameof(GiveValue) });
            }
            if (ReceiveValue <= 0)
            {
                yield return new ValidationResult(
                    $"Receive Value should be greater than 0.",
                    new[] { nameof(ReceiveValue) });
            }
        }
    }
}
