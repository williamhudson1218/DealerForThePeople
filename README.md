# DealerforthePeople

Dealer for the People is a tool used to identify positive reviews from the `dealerrater.com` website. The scoring rules used to determine the most positive reviews are outlined below.

## Installation

Clone the `DealerForThePeople` repository to your machine

*.NET Core 5.0 is required for installation

[.NET Core for macOS](https://docs.microsoft.com/en-us/dotnet/core/install/macos)

[.NET Core for Windows](https://dotnet.microsoft.com/download)

## Usage

```bash
# To run console application
In Visual Studio
File -> Open -> DealerForThePeople.sln
F5 to run the project
In console/terminal you will see the 3 most severely positive reviews listed along with the score they were given.

# Run tests MacOS
Run -> Unit Tests
Test Results panel will appear at the bottom of the application

#Run Tests Windows
View -> Test Explorer -> Run All Tests In View
```

*Note: if you download .NET core after loading the project in Visual Studio you may need to restart Visual Studio for the project to compile.*

## Scoring
*Note: Ratings on `dealerrater.com` are on a scale 1-50 and are represented visually to users on a x/5 scale.*

- Rating - The score for rating is calculated by dividing the 1-50 rating by 5. `i.e. 50/5 would be 10 points`
- `!`'s are awarded 1 point each up to 3 maximum points.
- Every 100 characters in the body of the review is worth 1 point each for a maximum of 10 points.
- The appsettings.json file contains an array of both `positiveWords` and `negativeWords`. Each `positiveWord` = +2 points. Each `negativeWord` = -2 points.

## License
[MIT](https://choosealicense.com/licenses/mit/)
