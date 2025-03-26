# Weather App

## Configuration
1. **Obtain API Key from OpenWeatherMap:**
   - Visit the [OpenWeatherMap](https://openweathermap.org/api) website.
   - Sign up for an account if you don't have one.
   - After logging in, go to the **API keys** section in your account.
   - Create a new API key or copy an existing key.

2. **Add API Key to User Secrets:**
   - Right-click on the project in Visual Studio → select **Manage User Secrets**.
   - A `secrets.json` file will open. Add the following content to the file:
   ```json
   {
     "WeatherAPI": {
       "Key": "your_api_key_here",  // Replace with the API key you obtained
       "BaseUrl": "https://api.openweathermap.org/data/2.5/weather"
     }
   }
   ```

3. **Save the `secrets.json` file and close it.**

4. **Run the application:**
   - When you run the application, it will automatically read the API key from User Secrets and use it to call the API.

### Note:
- **Security:** The `secrets.json` file will not be pushed to Git, so you can be assured that your API key will not be exposed.
- **Configuration for Others:** When others clone the project, they just need to follow the steps above to add their own API key to User Secrets.
