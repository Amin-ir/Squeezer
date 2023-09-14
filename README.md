# Squeezer : A URL Shortening Service with .NET

![Capture](https://github.com/Amin-ir/Squeezer/assets/91383421/0ad5e193-087e-434a-b6eb-de71630c2f02)

This is a simple URL shortening project built with .NET Core that also incorporates authentication using a claim-based approach. Note that this project is not intended for production use; it's primarily a training exercise and playground to test Docker files.

## Features

- Generate short aliases for long URLs through MD5 Encryption & 62-Based Encoding
- User registration and authentication
- Custom short URLs based on available keywords
- User dashboard to manage generated URLs
- Usage analytics for shortened URLs
- Claim-based authorization for protected actions
- Docker support for easy deployment

## Getting Started

These instructions will help you get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

To run this project, you need to have the following installed on your machine:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/Amin-ir/Squeezer.git
   ```

2. Navigate to the project directory:

   ```bash
   cd Squeezer
   ```

3. Build and run the Docker containers:

   ```bash
   docker-compose up --build
   ```

4. Once the containers are up and running, you can access the URL shortener service at `http://localhost:5234`.

## Usage

1. **Shorten a URL:**

   - To shorten a URL, simply navigate to the web interface at `http://localhost:5234`.
   - You may remain anonymously or you can simply log in using your credentials, after signing up through the profile badge at the corner.
   - Enter the long URL you want to shorten and submit the form.
   - You will receive a shortened URL that you can share.

2. **Redirect to the Original URL:**

   - To use a shortened URL, simply enter it in your browser's address bar.
   - You will be redirected to the original long URL.

## Authentication

This project uses a claim-based authentication approach. It includes a basic user management system where users can sign up, log in, and log out. Authentication is required to access the URL shortening and management features.

## Docker

This project is Dockerized, making it easy to deploy and test in containers. It includes Docker Compose configuration for setting up the application along with its dependencies.

To build and run the Docker containers, follow the installation instructions above.

## Contributing

This project is intended for personal use and learning. However, if you find a bug or have suggestions for improvements, feel free to open an issue or submit a pull request.

## License

This project is licensed under the GNU-3 License.

## Acknowledgments

- This project was created as a training exercise to learn about .NET Core, Docker, and claim-based authentication.
- Inspiration for the URL shortening logic was drawn from various online tutorials and resources.

**Note:** This project is not meant for production use, and it's important to consider security and scalability aspects when building a production-grade URL shortening service.
