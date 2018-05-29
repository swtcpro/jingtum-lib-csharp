# jingtum-lib-csharp
The jingtum-lib-csharp library to be used for interacting with jingtum blockchain network. 
This is the c# version.

The jingtum-lib-csharp library is based on the ws protocol to connect with jingtum system. 
The Remote class provides public APIs to create two kinds of objects: Request object by GET
method, and Transaction object by POST method. And then can submit data to server through 
Submit() method.

##How to use
1) Create a new instance of Remote class.  
'var remote = new Remote("ws://123.57.219.57:5020");'

2) Connect to server.  
'remote.Connect();'

3) Create a Request object by calling the Request*** methods. You can get the result data 
by the callback function.  
'remote.RequestServerInfo((result)=&gt;
{
	// log result.Exception or result.Message
});'

4) Close the connection.  
'remote.Disconnect();'

##Remote class
Main handler for backend system. It creates a handle with jingtum, makes request to jingtum, 
subscribes event to jingtum, and gets info from jingtum.

##Request class
Request server and account info without secret. Request is used to get server, account, orderbook 
and path info. Request is not secret required, and will be public to every one. All request is 
asynchronized and should provide a callback. Each callback has two parameter, one is error and 
the other is result.

##Transaction&lt;T&gt; class
Post request to server with account secret. Transaction is used to make transaction and collect 
transaction parameter. Each transaction is secret required, and transaction can be signed local 
or remote. Now remote sign and local sign are supported. All transaction is asynchronized and 
should provide a callback. Each callback has two parameter, one is error and the other is result.

##Events
You can listen events of the server.  
* Listening all transactions occur in the system. (Remote.Transactions event)
* Listening all last closed ledger event. (Remote.LedgerClosed event)
* Listening all server status change event. (Remote.ServerStatusChanged event)
* Listening all events for specific account. (Remote.CreateAccountStub method)
* Listening all events for specific orderbook pair. (Remote.CreateOrderBooksStub method)

##Source code
Source Code is in "src/jingtum-lib" folder. 
* src/jingtum-lib  
The main classes are defined in this folder, like: Remote class, Request class, Transaction&lt;T&gt;
class and so on.
* src/jingtum-lib/Core  
The implementation of encryption algorithm of local sign.
* src/jingtum-lib/Models  
The models which are used for requests and responses of calling jingtum APIs.
* src/jingtum-lib/Serialization  
The JSON converters for JSON serialization and deserialization.
* src/jingtum-lib/SerializedTypes  
The blob serialization which is used for transaction local sign.
