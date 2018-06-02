# jingtum-lib-csharp
The jingtum-lib-csharp library to be used for interacting with jingtum blockchain network. 
This is the c# version.

## Source code  
* src/jingtum-lib - The source codes of jingtum lib.
* src/UnitTests - The unit tests for jingtum lib.
* src/TestApp - The WindowsForm application for testing the jingtum lib apis.
* Samples - The samples to use the jingtum lib.
* docs - The documentation for the jingtum lib.

## Development Environment
* Windows 10
* VisualStudio 2017
* C# 7.0
* .NET Framework 4.5

## References:
The following libraries are referenced.
* Newtonsoft.Json (https://github.com/JamesNK/Newtonsoft.Json)
* Portable.BouncyCastle (http://www.bouncycastle.org/csharp/)
* WebSocket4Net (https://github.com/kerryjiang/WebSocket4Net)
* SuperSocket.ClientEngine.Core (https://github.com/kerryjiang/SuperSocket)

## Install
Install from nuget package manager. 

https://www.nuget.org/packages/JingTum.Lib/

## Summary
The jingtum-lib-csharp library is based on the ws protocol to connect with jingtum system. 
The Remote class provides public APIs to create two kinds of objects: Request object by GET
method, and Transaction object by POST method. And then can submit data to server through 
Submit() method.

## How to use
1) Create a new instance of Remote class.  

    var remote = new Remote("ws://123.57.219.57:5020");

2) Connect to server.  

    remote.Connect();

3) Create a Request object by calling the Request*** methods. You can get the result data 
by the callback function.  

    remote.RequestServerInfo.Submit((result)=&gt;  
    {  
	    // log result.Exception or result.Message  
    });  

4) Close the connection.

    remote.Disconnect();

## Remote class
Main handler for backend system. It creates a handle with jingtum, makes request to jingtum, 
subscribes event to jingtum, and gets info from jingtum.

## Request&lt;T&gt; class
Request server and account info without secret. Request is used to get server, account, orderbook 
and path info. Request is not secret required, and will be public to every one. All request is 
asynchronized and should provide a callback. Each callback has two parameter, one is error and 
the other is result.

## Transaction&lt;T&gt; class
Post request to server with account secret. Transaction is used to make transaction and collect 
transaction parameter. Each transaction is secret required, and transaction can be signed local 
or remote. Now remote sign and local sign are supported. All transactions are asynchronized and 
should provide a callback. Each callback provides the json message, exception, and result object.

## Events  
You can listen events of the server.  
* Listening all transactions occur in the system. (Remote.Transactions event)
* Listening all last closed ledger event. (Remote.LedgerClosed event)
* Listening all server status change event. (Remote.ServerStatusChanged event)
* Listening all events for specific account. (Remote.CreateAccountStub method)
* Listening all events for specific orderbook pair. (Remote.CreateOrderBooksStub method)

