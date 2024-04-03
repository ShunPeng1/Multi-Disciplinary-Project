

function FetchRequest(url, method, body, successCallback, errorCallback) {
    return fetch(url, {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            // Check the Content-Type of the response
            const contentType = response.headers.get("content-type");
            if (contentType && contentType.indexOf("application/json") !== -1) {
                return response.json();
            } else {
                return response.text();
            }
        })
        .then(data => {
            successCallback(data);
            return data;
        })
        .catch((error) => {
            errorCallback(error);
            return error;
        });
}

export default FetchRequest;