# Tests list

 try to use canon TDD
 https://tidyfirst.substack.com/p/canon-tdd
 
## What I need to do

You are to design a new system that can handle EID

EID rules
EID stands for "Elf Identifier" it is a unique id representing an elf composed by X characters.

Elves are magically created each year but limited to 999 birth maximum.

Here are the requirements:

| Positions | Meaning                                                                                        | Possible values                     |
|-----------|------------------------------------------------------------------------------------------------|-------------------------------------|
| 1         | Sex : 1 for `Sloubi`, 2 for `Gagna`, 3 for `Catact`                                            | 1, 2 or 3                           |
| 2, 3      | Last two digits of the year of birth (which gives the year to the nearest century)             | From 00 to 99                       |
| 4, 5, 6   | "Serial number": birth order                                                                   | From 001 to 999                     |
| 7, 8      | control key = complement to 97 of the number formed by the first 6 digits of the EID modulo 97 | From 01 to 97                       |


## Building Test list

- [X] an EID can't be null
- [X] an EID can't be empty
- [X] An EID must be 8 characters long
- [X] an EID can't be white space
- [X] An EID must be 8 digit characters long
- [X] First char is an int between 1 and 3
- [X] Second and third chars are an int representation between 00 and 99
- [X] Fifth to Sixth chars are an int representation between 001 and 999
- [X] Seventh and Height chars are an int representation between 01 and 97
- [X] Control must be rest mod 97
 
Removed, outofscope [O] an EID should Be unique

## Kata

1. First go with implementation of test list one by one

- [X] an EID can't be null
- [X] an EID can't be empty
- [X] An EID must be 8 characters long
- [X] an EID can't be white space
- [X] An EID must be 8 digit characters long
- [X] First char is an int between 1 and 3

Using Theory for each test, 
and adding why it's a error in test, so that it s easy to know what I'm testing


2. Then I found a test I don't how to check, already covered by others: Second and third chars are an int representation between 00 and 99

3. I Decided to create 2 test with a valid EID, my objective was to have a test that stay green all the time
4. As I moved forward it became hard to really know why my test are failing
5. I decided to move to a monadic  
but I not familiar with yet, my method `bool IsValid(string ied)` became a `Either<Error, string> Parse(string? ied)` get my test green
6. Continue with SN, Test for Error are green, but valid are not  
 I'm adding 2 theory I've missed something :) (I'll commit here)
```csharp
[InlineData("10000164", "SN can't be 000")]
[InlineData("30000177", "should fail")]//fails with "SN can't be 000" 
[InlineData("12345672", "should fail")]//fails with "SN can't be 000" 
public void Not_Be_Valid(string? eid, string because)
```
7. Found 3 problems thanks to the use of monad and Error,  
sex wasn't well recognized,  
my test 10000164 is actually valid at this time  
I'm terrible at regex
8. Continue with Control must be rest mod 97, test passed  
but adding Jerceval show it's not valid (it's my validation part that broke - thanks the use of monad, it's really easy to copy/paste on invalid part to see what's going wrong)
9. 