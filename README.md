# FactoryExercise

We are building a system for Factory X, which produces various products. This factory has specific business rules that we will implement by practicing TDD, OOP, and SOLID principles. For simplicity, we will use only unit tests to test our class library. Later on, we may expand upon it. Let's get started!





### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or newer).
- A development environment like [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio](https://visualstudio.microsoft.com/).

## Objectives
- Develop a system for Factory X
- Demonstrate practical use of:
  - Most significant features of C#'s **OOP** paradigm
  - **SOLID Principles**
  - **Test-Driven Development (TDD)**

## Folder Structure
```
CleanCodersCom/
 src/
    Application/
 tests/
```
## Architecture
We are starting with ***CreateWorkOrder*** use case and we build entities around it. First scenario is to create an work order and to prevent creation of work order if same already exists in released state. Later on we may add more features.

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/lapec/factoryexercise.git
   cd factoryexercise

2. Run NUnit tests

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
