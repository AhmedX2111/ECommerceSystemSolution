using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
	public class Address : IEquatable<Address>
	{
		public string Street { get; }
		public string City { get; }
		public string State { get; }
		public string ZipCode { get; }
		public string Country { get; }

		public Address(string street, string city, string state, string zipCode, string country)
		{
			Street = street;
			City = city;
			State = state;
			ZipCode = zipCode;
			Country = country;
		}

		// Equality by value
		public override bool Equals(object obj) => Equals(obj as Address);
		public bool Equals(Address other) =>
			other != null &&
			Street == other.Street &&
			City == other.City &&
			State == other.State &&
			ZipCode == other.ZipCode &&
			Country == other.Country;
		public override int GetHashCode() => HashCode.Combine(Street, City, State, ZipCode, Country);
	}
}
