Source for using HttpClient
------------------------

https://www.c-sharpcorner.com/article/calling-web-api-using-httpclient/
https://medium.com/@kova98/request-response-in-net-with-httpclient-5ed73941e037


Source for using RestSharp to pass authorization bearer token from header

-------------------------

Add Bearer Token Authorization Header to HTTP Request in .NET
https://jasonwatmore.com/c-restsharp-add-bearer-token-authorization-header-to-http-request-in-net#:~:text=Alternatively%2C%20the%20same%20bearer%20token%20auth%20header%20can,new%20JwtAuthenticator%20%28%22my-token%22%29%3B.%20For%20more%20info%20see%20https%3A%2F%2Frestsharp.dev%2Fauthenticators.html.


This project is aim for how to consume api service by using HttpClient and RestSharp in .Net 6


*** Note ***

if you face the SSL connection could not be established error when call to api service by using
httpclient, you have to use this code below


HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

//Pass the handler to httpclient(from you are calling api)
HttpClient client = new HttpClient(clientHandler);