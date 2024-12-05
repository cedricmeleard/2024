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

- [] an EID can't be null
- [] an EID can't be empty
- [] An EID must be 8 characters long
- [] an EID can't be white space
- [] An EID must be 8 digit characters long
- [] First char is an int between 1 and 3
- [] Second and third chars are an int representation between 00 and 99
- [] Fifth to Sixth chars are an int representation between 001 and 999
- [] Seventh and Height chars are an int representation between 01 and 97
- [] Control must be rest mod 97
- [] an EID should Be unique