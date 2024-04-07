function FetchRequest(url, method, body, successCallback, errorCallback) {
  let options = {
    method: method,
    headers: {
      'Content-Type': 'application/json'
    }
  };

  if (method === 'GET' || method === 'HEAD') {
    const params = new URLSearchParams(body).toString();
    url += '?' + params;
  } else {
    options.body = JSON.stringify(body);
  }

  return fetch(url, options)
    .then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
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
