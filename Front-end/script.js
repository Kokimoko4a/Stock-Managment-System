


function AuthorizeOnLoad()
{

   
        const token = localStorage.getItem('jwtToken');
        if (!token) {
            window.location.href = 'login.html';  // Redirect to login if no token
            return;
        }
    
        fetch('https://localhost:7020/Manager/managerDashboard', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
        .then(response => {
            if (response.ok) {
                // If authorized, hide the loading message and show the main content
                document.getElementById('loading').style.display = 'none';
                document.getElementById('mainContent').style.display = 'block';
            } else {
                window.location.href = 'access-denied.html'; // Redirect to access-denied page if not authorized
            }
        })
        .catch(error => {
            console.error('Authorization check failed:', error);
            window.location.href = 'access-denied.html'; // Redirect to access-denied page in case of error
        });

}

