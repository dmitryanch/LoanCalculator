# LoanCalculator
Coding Challenge

## The problem
You are in charge of creating a loan payment calculator.  A lot of people want to figure out given a loan, downpayment and interest rate what their payments are going to look like and how much of them is going to go towards principle vs interest.  You program is going to get input from the command line in the following format:

```
amount: 100000
interest: 5.5%
downpayment: 20000
term: 30

```
NOTE: the last line of input is a blank line.
The term is givne in years.  The interest can be given a percentage or a digit.

The program needs to process this input including the best way to handle some human errors (upper/lower case, spacing etc) and output a JSON of payment and total interest paid.

```
{
    "monthly payment": 454.23,
    "total interest": 83523.23
    "total payment" 163523.23
}
```

## Solution
Code: c# 7.3

Core Libraries: .NET Standard

Console Application: .NET Core

## Publish

```
cd <solution_derictory>
dotnet publish -c Release -r win10-x64
dotnet publish -c Release -r ubuntu.16.10-x64
```
