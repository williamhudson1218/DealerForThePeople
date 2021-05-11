# DealerforthePeople

Dealer for the People is a tool used to identify positive reviews from the `dealerrater.com` website. The scoring rules used to determine the most positive reviews is outlined below.

## Installation

Clone the `DealerForThePeople` repository to your machine

*.NET Core 5.0 is required for installation

## Usage

```bash
# To run console application
In Visual Studio
File -> Open -> DealerForThePeople.sln
F5 to run the project
In console/terminal you will see the 3 most severe reviews listed along with the score they were given.

# Run tests MacOS
Run -> Unit Tests
Test Results panel will appear at the bottom of the application

#Run Tests Windows
View -> Test Explorer -> Run All Tests In View
```

## Scoring
###### Note: Ratings on `dealerrater.com` are on a scale 1-50 and are represented visually to users on a x/5 scale.

- Rating - The score for rating is calculated by dividing the 1-50 rating by 5. `i.e. 50/5 would be 10 points`
- `!`'s are awarded 1 point each up to 3 maximum points.
- Every 100 characters in the body of the review is worth 1 point each for a maximum of 10 points.
- Usage of the words `love`, `excellent`, and `fantastic` are each rewarded 2 points.

## License
[MIT](https://choosealicense.com/licenses/mit/)