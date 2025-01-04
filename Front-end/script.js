/*async function login() {
    // Get input values
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // Combine inputs into a single string payload
    const payload = `${username}:${password}`; // This matches the [FromBody]string test

    try {
        // Make the API call
        const response = await fetch('https://localhost:7020/User/login', {
            method: 'POST', // Must match the HTTP verb expected by the API
            headers: {
                'Content-Type': 'application/json',
                credentials: "include", // Tell the server this is JSON
            },
            body: JSON.stringify(payload), // Send the payload as JSON
        });

        if (response.ok) {
            const data = await response.text(); // The API returns "Super"
            document.getElementById('responseMessage').textContent = `Response: ${data}`;
            document.getElementById('errorMessage').textContent = '';
        } else {
            const errorText = await response.text();
            document.getElementById('responseMessage').textContent = '';
            document.getElementById('errorMessage').textContent = `Error: ${errorText}`;
        }
    } catch (error) {
        document.getElementById('responseMessage').textContent = '';
        document.getElementById('errorMessage').textContent = `Error: ${error.message}`;
    }
}*/
