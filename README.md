# Arbitrary Portable

ArbitraryPortable is an attempt to create Arbitrary-precision arithmetic 
in a portable library approach. The reason of this project is a conversion 
MD5 hex hash to a base62 string for shortening, and backwards. 

  Example: "c367b3eb9df3bd5bdca9a3516af2d4da" -> "5WITyx9Hj07ZJvDzZcZHrI"

Current approach covers only signed integers as this was enough for project reasons. 
Current approach uses "school" math methods which are quite slow.
Current code is done from scratch during couple of evenings, contains no code
  from other sources and requires much optimization work to be done before
  could be used in production. There are [lots of other](https://en.wikipedia.org/wiki/List_of_arbitrary-precision_arithmetic_software) approaches you might find
  more compatible with your needs.

## Compatibility
- .NET Framework 4.5
- Silverlight 5
- .NET for Windows Store apps
- .NET for Windows Phone 8 apps
- Windows Phone Silverlight 8
- Portable Class Libraries
  
## TODO
- Fallback to int/long where possible.
- Fallback to BigInteger where possible.
- Use arrays instead of strings for internal calculations.
- Optimize arithmetic methods using Karatsuba, Schönhage–Strassen, Knuth, Furier Transform.
- Use base of size of Int32 to "shorten" sizes of numbers during multiplication 
 (within multiplication speed being kept).
- Deliver as a NuGet package.
 
## Installation
Clone and build.

## Usage Examples.

You can use ALong objects as normal C# numbers. 
In the following examples "short human readable" numbers were used for simplicity, all these
examples will work with "big" numbers. 

In case you are using strings as big numbers, make sure to use these on the right side of the 
operator. (e.g. "1" + ALong(2) won't work, but ALong(2) + "1" will.)

```C#
	var a = new ALong(10);      // Initialize with 10
	var b = new ALong("10");    // Initialize with 10

	// Sum
	a += 20;                    // 30
	a += 20L;                   // 50
	a += "20";                  // 70
	a += new ALong("20");       // 90
	a -= b;                     // 80
	a += b;                     // 90
	a -= -10;                   // 100

	// Comparing
	var cmp = a < b;            // false
	cmp = a <= b;               // false
	cmp = a > b;                // true
	cmp = a > 200;              // false
	cmp = a >= "100";           // true

	// Multiplication
	a *= 10;                    // 1000
	a *= b;                     // 10000
	a = a * b;                  // 100000
	a = a * "-10";              // -1000000

	// Division
	a /= -1000;                 // 1000
	a /= b;                     // 100

	// Remainder
	var c = a % (b + 3);        // 9
	c = a % 13;                 // 9
	c = b % 5;                  // 0
	c = b % 3;                  // 1

	// Power
	c = AMath.Pow(a, b);        // 100^10 = 100000000000000000000
	
	// Base Conversion
	string d;
	d = "c367b3eb9df3bd5bdca9a3516af2d4da".FromArbitraryBase(16).ToArbitraryBase(62);   // "5WITyx9Hj07ZJvDzZcZHrI"
	d = d.FromArbitraryBase(62).ToArbitraryBase(16);                                    // "c367b3eb9df3bd5bdca9a3516af2d4da"
```

See more examples in ArbitraryPortableTests project.

## Contributing

1. Fork, create feature branch, commit your changes and push to the branch.
2. Submit a pull request.

## History

- Initial version.

## Credits

Vladislav Kuzmin
https://ee.linkedin.com/pub/vladislav-kuzmin/65/972/2aa

## License

This software is distributed under LGPL3 License, please see LICENSE file for details.

## References
https://en.wikipedia.org/wiki/Arbitrary-precision_arithmetic
https://en.wikipedia.org/wiki/List_of_arbitrary-precision_arithmetic_software
