const API_URL = "/api/";

const token = localStorage.getItem("token");

async function request(method, url, body) {
  const opts = {
    method: method,
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
  };
  if (body) {
    opts.body = JSON.stringify(body);
  }
  const res = await fetch(API_URL + url, opts);
  if (res.ok) {
    return res.json();
  } else {
    // handle failure
  }
}

export default {
  fetch: (url) => request("GET", url),
  post: (url, body) => request("POST", url, body),
  put: (url, body) => request("PUT", url, body),
  patch: (url, body) => request("PATCH", url, body),
  delete: (url, body) => request("DELETE", url, body),
};
