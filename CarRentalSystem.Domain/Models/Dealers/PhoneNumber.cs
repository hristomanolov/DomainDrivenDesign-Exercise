using CarRentalSystem.Domain.Common;
using CarRentalSystem.Domain.Exceptions;
using System.Text.RegularExpressions;

using static CarRentalSystem.Domain.Models.ModelConstants.PhoneNumber;

namespace CarRentalSystem.Domain.Models.Dealers
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string number)
        {
            this.Validate(number);

            if (!Regex.IsMatch(number, PhoneNumberRegularExpression))
            {
                throw new InvalidPhoneNumberException("Phone number must start with a '+' and contain only digits afterwards.");
            }

            this.Number = number;
        }

        public string Number { get; }

        public static implicit operator string(PhoneNumber number) => number.Number;

        public static implicit operator PhoneNumber(string number) => new PhoneNumber(number);

        private void Validate(string number)
            => Guard.ForStringLength<InvalidDealerException>(
                number,
                MinPhoneNumberLength,
                MaxPhoneNumberLength,
                nameof(PhoneNumber));
    }
}
