# Taxually technical test

![](https://github.com/guillermosaez/developer-test/actions/workflows/dotnet.yml/badge.svg)

# 🧪 How to Run the Taxually Technical Test

Follow these steps to set up the environment and test the API endpoints locally:

## ✅ Step 1: Open a terminal and navigate to the project root

Open your terminal and move to the root directory of the project:

```bash
cd /path/to/your/project
```

## ✅ Step 2: Start the services using Docker

Run the following command to start all required infrastructure (e.g., databases):

```bash
docker compose up
```
This will launch all services defined in the docker-compose.yml file.

## ✅ Step 3: Send test requests

Once all services are up and running:

1. Open the file `./Taxually.TechnicalTest/Taxually.Api/Controllers/VatRegistration.http`
2. Execute the HTTP requests defined in the file, making sure to select the local-docker environment.

⚠️ This file is intended to be used with tools such as the REST Client extension in VS Code.

-----

This solution contains an [API endpoint](https://github.com/Taxually/developer-test/blob/main/Taxually.TechnicalTest/Taxually.TechnicalTest/Controllers/VatRegistrationController.cs) to register a company for a VAT number. Different approaches are required based on the country where the company is based:

- UK companies can register via an API
- French companies must upload a CSV file
- German companies must upload an XML document

We'd like you to refactor the existing solution with the following in mind:

- Readability
- Testability
- Adherance to SOLID principles

We'd also like you to add some tests to show us how you'd test your solution, although we aren't expecting exhaustive test coverage.

We don't expect you to implement the classes for making HTTP calls and putting messages on queues.

We'd like you to spend not more than a few hours on the exercise.

To develop and submit your solution please follow these steps:

1. Create a public repo in your own GitHub account and push the technical test there
2. Develop your solution and push your changes to your own public GitHub repo
3. Once you're happy with your solution send us a link to your repo
