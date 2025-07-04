using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
	public class Money : IEquatable<Money>
	{
		public decimal Amount { get; }
		public string Currency { get; } = "EGY";

		public Money(decimal amount, string currency)
		{
			Amount = amount;
			Currency = currency;
		}

		// Equality by value
		public override bool Equals(object obj) => Equals(obj as Money);
		public bool Equals(Money other) => other != null && Amount == other.Amount && Currency == other.Currency;
		public override int GetHashCode() => HashCode.Combine(Amount, Currency);

		// Addition
		public static Money operator +(Money a, Money b)
		{
			if (a.Currency != b.Currency)
				throw new InvalidOperationException("Cannot add Money with different currencies.");
			return new Money(a.Amount + b.Amount, a.Currency);
		}

		// Subtraction
		public static Money operator -(Money a, Money b)
		{
			if (a.Currency != b.Currency)
				throw new InvalidOperationException("Cannot subtract Money with different currencies.");
			return new Money(a.Amount - b.Amount, a.Currency);
		}
	}
}
