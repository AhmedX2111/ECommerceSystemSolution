using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
	public class Weight : IEquatable<Weight>
	{
		public double Value { get; }
		public string Unit { get; } // e.g., "kg"

		public Weight(double value, string unit = "kg")
		{
			Value = value;
			Unit = unit;
		}

		// Equality by value
		public override bool Equals(object obj) => Equals(obj as Weight);
		public bool Equals(Weight other) => other != null && Value == other.Value && Unit == other.Unit;
		public override int GetHashCode() => HashCode.Combine(Value, Unit);

		// Example: convert to grams
		public double ToGrams()
		{
			return Unit switch
			{
				"kg" => Value * 1000,
				"g" => Value,
				_ => throw new NotSupportedException($"Unit {Unit} not supported.")
			};
		}
	}
}
