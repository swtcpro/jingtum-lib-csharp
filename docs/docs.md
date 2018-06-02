# Design Spec

## Features
It has the same funtionalities as jingtum-lib-nodejs. 

https://github.com/swtcpro/jingtum-lib-nodejs

## References
* WebSocket4Net (https://github.com/kerryjiang/WebSocket4Net) The jingtum-lib-csharp library is based on the ws protocol to connect with jingtum system. 
* Portable.BouncyCastle (http://www.bouncycastle.org/csharp/) The jingtum-lib-csharp library local sign depends on ECDSA signature.
* Newtonsoft.Json (https://github.com/JamesNK/Newtonsoft.Json) The json string format is used to communite with jingtum system.

## Models
* The inner Server class performs the websocket communication.
* The Remote class provides public APIs to create two kinds of objects: Request object, and Transaction object.
* The Request class is used to request info.
* The Transaction class is used to operate transactions. 
* Request class and Transacton class both use Submit(callback) method to submit data to server.
* The result can be handled by the callback.

```
|-----------|     |--------------|     |--------|     |--> [ Request Object ]
| WebSocket | --> | Server       | --> | Remote | --> |    
| Protocal  | <-- | Inner Class  | <-- | Class  |     |--> [ Transaction Object]
|-----------|     |--------------|     |--------|
```

## Stubs
* Account stub listen all the transactions in server, and then filter them for specfic account.
* OrderBook stub listen all the transactions in server, and then filter them for specfic gets/pays pair.

## Data
* The json string is sent to server for request operation.
* The json string is sent to server for transaction operation (server sign).
* The transaction data is serialized to blob string and then sent to server for transaction operation (local sign).
* The json string is reveived from server for reqeust and transaction operations.
* The callback result contains:
  * The raw message from server, in json format.
  * The exception message if the operation is refused by the server.
  * The result object if the operation is succeed. It is parsed from the json message.

## Local Sign
The local sign is implemented by serializing the json string into binary blob, and then send the blob string to server. 

The inner Serializer class performs the serialization. The data members are grouped into different categories, and then serialized as data type and data value pair.

The category contains:
* Int8
* Int16
* Int32
* Int64
* Hash128
* Hash160
* hash256
* Amount
* VL (string)
* Account (string)
* PathSet
* Object
* Array

# Dcuments
Usage for jingtum-lib-csharp. All classes are under the namespace JingTum.Lib. 

For the detail documents for each api, please refer to the jingtum-lib.chm.

## Wallet class
### Genreate()
Genereates a new wallet.

#### sample
```
var wallet = Wallet.Genereate();
```

### FromSecret(secret)
Creates a wallet from existing secret. The secret is the private secret of jingtum wallet.

#### sample
```
var wallet = Wallet.FromSecret("shJBucfMWjirJv4DiDe1DNmeRSub3");
```

## Remote class
Main function class in jingtum-lib-csharp. It creates a handle with jingtum, makes request to jingtum, subscribs event to jingtum, and gets info from jingtum.

* Remote(url, localSign)
* Connect(callback)
* Disconnect()
* RequestServerInfo()
* RequestLedgerClosed()
* RequestLedger(options)
* RequestTx(options)
* RequestAccountInfo(options)
* RequestAccountTums(options)
* RequestAccountRelations(options)
* RequestAccountOffers(options)
* RequestAccountTx(options)
* RequestOrderBook(options)
* RequestPathFind(options)
* CreateAccountStub()
* CreateOrderBookStub()
* BuildPaymentTx(options)
* BuildRelationTx(options)
* BuildAccountSetTx(options)
* BuildOfferCreateTx(options)
* BuildOfferCancelTx(options)
* DeployContractTx(options)
* CallContractTx(options)

### Remote(url, localSign)
#### options
* url: The jingtum websocket server url.
* localSign: Whether sign transaction in local.

#### sample
```
var remote = new remote("ws://123.57.219.57:5020", true);
```

### Connect(callback)
Each remote object should connect jingtum first. Now jingtum should connect manual, only then you can send request to backend.
Callback as MessageCallback&lt;ConnectResponse&gt;.

#### sample
```
remote.Connect(result =>
{
	if(result.Exception != null)	
	{	
		Console.Write(result.Exception.Message);		
	}	
	else	
	{	
		Console.Write(result.Message);		
	}	
});
```

### Disconnect()
Remote object can be disconnected manual, and no parameters are required.

#### sample
```
remote.Disconnect();
```

### RequestServerInfo()
Create request object and get server info from jingtum.
Callback as MessageCallback&lt;ServerInfoResponse&gt;.

#### sample
```
var req = remote.RequestServerInfo();
req.Submit(reqResult =>
{
	var info = reqResult.Result.Info;
	// Version: "0.29.60" //currenct jingtum version
	// Ledgers: "6753-14393" //complted ledgers in system
	// Node: "n9LxdTZbjjQnuPiM5SgwPYQndfb64YHbmCp1mhsoch7uw5HQJ3k6" //jingtum node id
	// State: "full   34:35:40" //currenct jingtum node state.
});
```

### RequestLedgerClosed()
Create request object and get last closed ledger in system.
Callback as MessageCallback&lt;LedgerClosedResponse&gt;.

#### sample
```
var req = remote.RequestLedgerClosed();
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	// LedgerIndex: 375596 //last closed ledger height info.
	// LedgerHash: "B5F6758A269778C7DD503109F711D4EE41653C0F78D908A4FCD5BB022D2E74D8" //last closed ledger height info.
});
```

### RequestLedger(options)
Create request object and get ledger in system.
Callback as MessageCallback&lt;LedgerResponse&gt;.

#### options
(If none is provided, then last closed ledger is returned.)
* LedgerIndex: The ledger index.
* LedgerHash: The ledger hash.
* Transactions: Whether include the transactions list in ledger.

#### sample
```
var req = remote.RequestLedger(new LedgerOptions{ LedgerIndex = 330784, Transactions = true});
req.Submit(reqResult =>
{
	var info = reqResult.Result.Ledger;
	//Accepted: True //marks the ledger is accepted
	//LedgerIndex: 330853 //ledger height informations
	//LedgerHash: "65048471FBC3DE4A6ECDA6CA4F9D09ED79313BFDA2007545EBD40FF010946400" //ledger height informations
	//CloseTime: 2018-05-27 18:36:40 //ledger closed time in UTC+8
	//TotalCoins: "600000000000000000" //total swt in system. 
	//Transactions: [array] //transactions list
});
```

### RequestTx(options)
Query one transaction information.
Callback as MessageCallback&lt;TxResponse&gt;.

#### options
* Hash: The transaction hash.

#### sample
```
var req = remote.RequestTx(new TxOptions { Hash = "BDE5FAA4F287353E65B3AC603F538DE091F1D8F4723A120BD7D930C5C4668FE2" });
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	//Account: "jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR"
	//Fee: "10000"
	//Flags: 0
	//Sequence: 1
	//SigningPubKey: "023CB40DE8C112AFC9815C1CF14A02D5EBD9E11604B4996109770E5D32CC57A31A"
	//Timestamp: 2018-05-27 18:36:32
	//TxnSignature: "3045022100E75441C048FA019C0BA1F79CF4724E726A915F7E444C97178FCE03ADE6B7B4B702202BA1D6B92EEF10027D72AA4DAFDF4AE74A6086BCB81A668B02DB272898478629"
	//Date: 2018-05-27 18:36:40
	//Hash: "BDE5FAA4F287353E65B3AC603F538DE091F1D8F4723A120BD7D930C5C4668FE2"
	//LedgerIndex: 330853
	//Memos: [Array]
	//TransactionType: Payment
	//Meta: [JingTum.Lib.Meta]
	//TxResult: [JingTum.Lib.SentTxResult] // diffrent transaction type has different type of TxResult
});
```

### RequestAccountInfo(options)
Get account info.
Callback as MessageCallback&lt;AccountInfoResponse&gt;.

#### options
* Account: The wallet address.
* LedgerIndex: (optional) 

#### sample
```
var req = remote.RequestAccountInfo(new AccountInfoOptions { Account = " j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1" });
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	//LedgerHash: "33505B81ABD10DC57BDB2607201BD0BAA2CB0921299F83431983EE2CCECBE3AD" //ledger height for the account.
	//LedgerIndex: 375693 //ledger height for the account.
	//Validated: True
	
	var data = info.AccountData; //account data information
	//Account: "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1"
	//Balance: [SWT]
	//OwnerCount: 17
	//PreviousTxnID: "6205D1B403AD307F666197919ADA90F1ECE1B7530E45AE6AC757F555B644DB49"
	//PreviousTxnLgrSeq: 374816
	//Sequence: 99
	//Index: "1DEF8374E9B8043F6B7306E9DCC039D513C8B48E8415BBC8467F24F0DCDB767E"
});
```

### RequestAccountTums(options)
Each account holds many jingtum tums, and the received and sent tums can be found by RequestAccountTums.
Callback as MessageCallback&lt;AccountTumsResponse&gt;.

#### options
* Account: The wallet address.
* LedgerIndex: (optional)

#### sample
```
var req = remote.RequestAccountTums(new AccountTumsOptions { Account = " j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1" });
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	//LedgerHash: "E21F13E341DFD6C78D0C768D95FD11AC6F35A8C49DADFE84D9063EA7FCEDE961"
	//LedgerIndex: 375725
	//ReceiveCurrencies: ["CNY", "EUR", "USD"] //tums that can be received by this account
	//SendCurrencies: ["CNY"] //tums that will be sent by this account.
	//Validated: True
});
```

### RequestAccountRelations(options)
Jingtum wallet is connected by many relations. Now jingtum supports `trust`, `authorize` and `freeze` relation, all can be queried by requestAccountRelations.
Callback as MessageCallback&lt;AccountRelationsResponse&gt;.

#### options
* Account: The wallet addres.
* Type: Trust, Ahthorize, Freeze
* Ledger: (optional)
* Limit: (optional) Limit the return relations count.
* Marker: (optional) Request from the marker position. It can be got from the response of previous request.

#### sample
```
var req = remote.RequestAccountRelations(new AccountRelationsOptions
{
	Account = " j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Type = RelationType.Trust
});
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	//Account: "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1"
	//LedgerHash: "55AD1ECF68DEEC13D623F02C88EBAADEE6FC0F93CFFC735CE18242120BD83915"
	//LedgerIndex: 375754 
	//Marker: "D831E0EA93BAFC3A6440761FB5A383CF00F958E9BE94ED33EF77A855E8FE3040"
	//Lines: [Array]
	//Validated: True
	
	var line = info.Lines[0];
	//Account: "jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS"
	//Balance: "1.381"
	//Currency: "CNY"
	//Limit: "100"
});
```

### RequestAccountOffers(options)
Query account's current offer that is suspended on jingtum system, and will be filled by other accounts.
Callback as MessageCallback&lt;AccountOffers&gt;.

#### options
* Account: The wallet address.
* LedgerIndex: (optional)
* Limit: (optional) Limit the return offers count.
* Marker: (optional) Request from the marker position. It can be got from the response of previous request.

#### sample
```
var req = remote.RequestAccountOffers(new AccountOffersOptions
{
	Account = " j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Limit = 1 
});
req.Submit(reqResult =>
{
	var info = reqResult.Result; 
	//Account: "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1"
	//LedgerHash: "D3988CD85D7CBEBC683E9D00563CF944312195F2B1DF9D5ECF8CD635E32EBF59"
	//LedgerIndex: 375782
	//Offers: [Array]
	//Marker: "41B91A63DCBCCA0EB01057A09373E14A38435A0BEC5D4A142DD05E02F783305A"
	//Validated: True
	
	var offer = info.Offers[0];
	//Flags: 131072
	//Seq: 7
	//TakerGets: [SWT]
	//TakerPays: [CNY:jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS]
	//IsSell: True
});
```

### RequestAccountTx(options)
Query account transactions.
Callback as MessageCallback&lt;AccountTxResponse&gt;.

#### options
* Account: The wallet address.
* LedgerIndex: (optional) 
* Limit: (optional) Limit the return trancations count.
* Marker: (optional) Request from the marker position. It can be got from the response of previous request.

#### sample
```
var req = remote.RequestAccountTx(new AccountTxOptions
{
	Account = " j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1"
});
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	//Account: "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1"
	//LedgerIndexMax: 375810
	//LedgerIndexMin: 3
	//Marker: [JingTum.Lib.Marker] 
	//Transactions: [Array]
	
	var tx = info.Transactions[0]; // diffent type of tx has diffrent data 
	//CounterParty: "jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR"
	//Amount: [SWT]
	//Type: Sent
	//Date: 2018-06-01 19:14:30
	//Hash: "62490FD35FD32B8FB9C4201DEF07BB5D2A0B5FA24B87384686B4077A9D362FF9"
	//Fee: "0.01"
	//Result: "tesSUCCESS"
	//Memos: [Array]
	//Effects: [Array]
	
	var effect = tx.Effects[0];
	// differenct type of transactions can have different type of effects.
});
```

### RequestOrderBook(options)
Query order book info.
Callback as MessageCallback&lt;OrderBookResponse&gt;.

Firstly, each order book has a currency pair, as AAA/BBB. When to query the bid orders, gets is AAA and pays is BBB. When to query the ask orders, gets is BBB and pays is AAA.
The result is array of orders.

#### options
* Gets: Amount object. (ignore the Value)
* Pays: Amount object. (ignore the Value)

#### sample
```
var req = remote.RequestOrderBook(new OrderBookOptions
{
	Gets = new Amount ("SWT", ""),
	Pays = new Amount ("CNY", " jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS")
});
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	//LedgerCurrentIndex: 375847
	//Offers: [Array]
	
	var offer = info.Offers[0];
	//Account: "jfVzX1Gcqb8KqvU93GRUZr6ycZnyhxVeBz"
	//Flags: 0
	//LedgerEntryType: "Offer"
	//PreviousTxnID: "880F416FAC41A73BC74286E3569AB28E7526EC8641C0869B327DA7AA6A26AE59"
	//PreviousTxnLgrSeq: 374793
	//Sequence: 1676
	//TakerGets: [CNY:jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS]
	//TakerPays: [SWT]
	//Index: "3699ADC5E51772884CBC548A92E9883BF29861692C6A4894548B2348983719EA"
	//OwnerFunds: "1394308.241700002"
	//Quality: "2325581.395348837"
	//IsSell: False
	//Price: "0.42999976299225661844123393"
});
```

### RequestPathFind(options)
Query path from one curreny to another.
Callback as MessageCallback&lt;PathFindResponse&gt;.

#### options
* Account: The payment source address.
* Destination: The payment target address.
* Amount: The payment amount.

#### sample
```
var req = remote.RequestOrderBook(new OrderBookOptions
{
	Account = "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT",
	Destination = "jB9eHCFeCaoxw6d9V9pBx5hiKUGW9K2fbs",
	Amount = new Amount ("CNY", " jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or", "0.5")
});
req.Submit(reqResult =>
{
	var info = reqResult.Result;
	//Source: "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT"
	//Destination: "jB9eHCFeCaoxw6d9V9pBx5hiKUGW9K2fbs"
	//Amount: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
	//Alternatives: [Array]
	
	var alt = info.Alternatives[0];
	//Key: "BC3B31D597947B3FEAFEE7D78A460DB16B37C939"
	//Choice: [SWT] (Value: "0.300752")
}
```

In this path find, the user wants to send CNY to another account. The system provides one choice which is to use SWT.

In each choice, one `Key` is presented. Key is used to "SetPath" in transaction parameter setting.

### CreateAccountStub()
AcccountStub is Account class, and is used to subscribe events of account. User can subscribe each account on account stub. 

### CreateOrderBookStub()
OrderBookStub is same as AccountStub. User can subscrible each currency pair on orderbook stub.

### BuildPaymentTx(options)
Normal payment transaction. 

More parameters can be set by Transaction members. The secret is requried, and others are optional.

#### options
* Account: The source address.
* To: The destination address.
* Amount: The payment amount.

#### sample
```
 var tx = remote.BuildPaymentTx(new PaymentTxOptions {
	Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	To = "jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR",
	Amount = new Amount
	{
		Value = "0.5",
		Currency = "SWT",
		Issuer = ""
	}
});
tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
tx.AddMemo("给jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR支付0.5swt.");
tx.Submit(txResult => {
	var info = txResult.Result; 
	//EngineResult: "tesSUCCESS"
	//EngineResultCode: 0
	//EngineResultMessage: "The transaction was applied. Only final in a validated ledger."
	//TxJson: [JingTum.Lib.PaymentTxJson]
});
```

### BuildRelationTx(options)
Build relation Transaction. Now Jingtum supports "trust", "authorize" and "freeze" relation setting.

Same as payment transaction parameter setting, secret is required and others are optional.

#### options
* Account: The source address.
* Target: The target address.
* Type: The relation type. "Trust", "Authorize", "Freeze".
* Limit: The limit amount.

#### sample
```
var tx = remote.BuildRelationTx(new RelationTxOptions {
	Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Target = "jBKaXuYemkAb5HytZgosAcWgWDZbBvz6KR",
	Limit = new Amount
	{
		Value = "0.01",
		Currency = "CNY",
		Issuer = " jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS"
	},
	Type = RelationType.Authorize
});
tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
tx.Submit(txResult => {
	var info = txResult.Result; 
	//EngineResult: "tesSUCCESS"
	//EngineResultCode: 0
	//EngineResultMessage: "The transaction was applied. Only final in a validated ledger."
	//TxJson: [JingTum.Lib.RelationTxJson]
});
```

### BuildAccountSetTx(options)
AccountSet Transaction is used to set account attribute. Now Jingtum supoorts three account attributes setting, as "property", "delegate" and "signer". "property" is used to set normal account info, "delegate" is used to set delegate account for this account, and "signer" is used to set signers for this acccount.

Same as payment transaction parameter setting, secret is required and others are optional.

#### options
* Account: The source address.
* Type: The property type. "Property", "Delegate", "Signer".
* SetFlag: (optional) The attribute to set for property type.
* ClearFlag: (optional) The attribute to remove for property type.
* DelegateKey: (optional) The regualar address for delegate type.

#### sample
```
var tx = remote.BuildAccountSetTx(new AccountSetTxOptions
{
	Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Type =  AccountSetType.Property，
	SetFlag = SetClearFlag.RequireDest
});
tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
tx.Submit(txResult => {
	var info = txResult.Result; 
	//EngineResult: "tesSUCCESS"
	//EngineResultCode: 0
	//EngineResultMessage: "The transaction was applied. Only final in a validated ledger."
	//TxJson: [JingTum.Lib.AccountSetTxJson]
});
```

### BuildOfferCreateTx(options)
Create one offer and submit to system. 

#### options
* Account: The source address.
* Type: "Sell" or "Buy".
* TakerGets: The amount to get by taker.
* TakerPays: The amount to pay by taker.

#### sample
```
var tx = remote.BuildOfferCreateTx(new OfferCreateTxOptions
{
	Account = " j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Type = OfferType.Sell,
	TakerGets = new Amount
	{
		Value = "0.01",
		Currency = " SWT ",
		Issuer = ""
	},
	TakerPays = new Amount
	{
		Value = "1",
		Currency = "CNY",
		Issuer = " jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS "
	}
});
tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
tx.Submit(txResult => {
	var info = txResult.Result; 
	//EngineResult: "tesSUCCESS"
	//EngineResultCode: 0
	//EngineResultMessage: "The transaction was applied. Only final in a validated ledger."
	//TxJson: [JingTum.Lib.OfferCreateTxJson]
});
```

### BuildOfferCancelTx(options)
Order can be canceled by order sequence. The sequence can be get when order is submitted or from offer query operation.

#### options
* Account: The account address.
* Sequence: The order sequence. It can be get from RequestAccountOffers operation.

#### sample
```
var tx = remote.BuildOfferCancelTx(new OfferCancelTxOptions
{
	Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Sequence = 8
});
tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
tx.Submit(txResult => {
	var info = txResult.Result; 
	//EngineResult: "tesSUCCESS"
	//EngineResultCode: 0
	//EngineResultMessage: "The transaction was applied. Only final in a validated ledger."
	//TxJson: [JingTum.Lib.OfferCancelTxJson]
});
```

### DeployContractTx(options)
Deploy contract to the system. The contract address is returned in the ContractState property.

#### options
* Account: The source address.
* Amount: The swt to active the contract address.
* Paylaod: The lua scripts.
* Params: (optional) The parameters.

#### sample
```
var tx = remote.DeployContractTx(new DeployContractTxOptions
{
	Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Amount = 35,
	Payload = "result={}; function Init(t) result=scGetAccountInfo(t) return result end; function foo(t) a={} result=scGetAccountInfo(t) return result end;",
	Params = new string[]{"j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1"}
});
tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
tx.Submit(txResult => {
	var info = txResult.Result; 
	//ContractState: "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn"
	//EngineResult: "tesSUCCESS"
	//EngineResultCode: 0
	//EngineResultMessage: "The transaction was applied. Only final in a validated ledger."
	//TxJson: [JingTum.Lib.DeployContractTxJson]
});
```

### CallContractTx(options)
Call the contract. The call result is returned in the ContractState property.

#### options
* Account: The source address.
* Destination: The contract address.
* Foo: The function name to call.
* Params: (optional) The parameters.

#### sample
```
var tx = remote.CallContractTx(new CallContractTxOptions
{
	Account = "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1",
	Destination = "jaVDaozkmFzCGwuBYL5wQ3SvhnUrySuofn",
	Foo = "foo",
	Params = new string[]{"j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1"}
});
tx.SetSecret("ssGkkAMnKCBkhGVQd9CNzSQv5zdNi");
tx.Submit(txResult => {
	var info = txResult.Result; 
	//ContractState: "{"Account" : "j9FGhAW9dSzL3RjbXkyW6Z6bHGxFk8cmB1", "Balance" : "853871574", ......}"
	//EngineResult: "tesSUCCESS"
	//EngineResultCode: 0
	//EngineResultMessage: "The transaction was applied. Only final in a validated ledger."
	//TxJson: [JingTum.Lib.CallContractTxJson]
});
```

### Events

#### Transactions
* Listening all transactions occur in the system.

#### LedgerClosed
* Listening all last closed ledger event.

#### ServerStatusChanged
* Listening all server status change event.

## Request&lt;T&gt; class

Request is used to get server, account, orderbook and path info. Request is not secret required, and will be public to every one. All requests are asynchronized and should provide a callback. Each callback returns the raw json message, exception and parsed result.

* SelectLedger(ledger)
* Submit(callback)

### SelectLedger(ledger)

Select one ledger for current request, ledger can be follow options,

* ledger index: The ledger index.
* ledger hash: The ledger hash.
* ledger state: The ledger state. "Current", "Validated", "Closed". 

After ledger is selected, the result is for the specified ledger.

### Submit(callback)

Callback entry for request. Each callback returns the raw json message, exception and parsed result.

* Message: The raw json message received from the jingtum system.
* Exception: The exception for local argument validation or error message from the jingtum system.
* Result: The parsed result object.


## Transaction&lt;T&gt; class

Transaction is used to make transaction and collect transaction parameter. Each transaction is secret required, and transaction can be signed local or remote. All transactions are asynchronized and should provide a callback. Each callback returns the raw json message, exception and parsed result.

* Account (get)
* TransactionType (get)
* SetSecret(secret)
* AddMemo(memo)
* SetPath(key)
* SetSendMax(amount)
* SetTransferRate(rate)
* SetFlags(flags)
* Submit(callback)

### Account property
Each transaction has source address, and its secret should be set.

Account can be master account, delegate account or operation account.

### TransactionType property

Get transaction type. Now Jingtum supports `Payment`, `OfferCreate`, `OfferCancel`, `AccountSet` and so on. 

### SetSecret(secret)

Set Transaction secret, this method is required before transaction submit.

### AddMemo(memo)

Add one memo to transaction, memo is string and is limited to 2k.

### SetPath(key)

Set path for one transaction. The key parameter is request by RequestPathFind method. When the key is set, "SendMax" parameter is also set.

### SetSendMax(amount)

Set payment transaction max amount when needed. It is set by "SetPath" default.

### SetTransferRate(rate)

Set transaction transfer rate. It should be check with fee. 

### SetFlags(flags)

Set transaction flags. It is used to set Offer type mainly. As follows

```
SetFlags((UInt32)OfferCreateFlags.Sell)
```
    
### Submit(callback)

Submit entry for transaction. Each callback returns the raw json message, exception and parsed result.

* Message: The raw json message received from the jingtum system.
* Exception: The exception for local argument validation or error message from the jingtum system.
* Result: The parsed result object.

## Account class

Account is account stub for account events. One Account stub can subscribe many account events.

### Subscribe(account, callback)

Subscribe account event.

### Unsubscribe(account)

Unsubscribe account event.

## OrderBook class
OrderBook is order book stub for order book events. One OrderBook stub can subscribe many order book events. 

### RegisterListener(gets, pays, callback)

Subscribe orderbook event.

### UnregisterListener(gets, pays)

Unsubscribe orderbook event.

## TxResult class
In the result of RequestAccountOffers and RequestTx, the transaction item contains lots of info. The Type property indicates different type of transaction. Different transaction has different result. The following transaction types are listed.

### Sent
The payment operation to other address. It has following info:

```
//Type: Sent
//CounterParty: "jJ3KZo6Zr3BVLiXBBKqMfQoQZHiYFZKNFT"
//Amount: [SWT]
//Date: 2018-04-28 22:04:30
//Hash: "66B1D54953B277CD4FC438ACF198BCB1E456E70D4260CDECA2020AB0E36893B9"
//Fee: "0.01"
//Result: "tesSUCCESS"
//Memos: [Array]
//Effects: [Array]
```

### Received
The payment operation from other address. It has following info:

```
//Type: Received
//CounterParty: "jpGnxQzw4KX1r6C9rbygDNdPqn843thpea"
//Amount: [SWT]
//Date: 2018-04-27 0:39:30
//Hash: "5C73414C742388348B7DC3F915A627A69912E4E63F5D5A56D03AACCDEFD7C8FD"
//Fee: "0.01"
//Result: "tesSUCCESS"
//Memos: [Array]
//Effects: [Array]
```

### Convert
User processed convert operation. It has following info. (I have not submitted the convert operation, here just list the info properties.)

```
//Type: Received
//Spent: [SWT]
//Amount: [CNY:jBciDE8Q3uJjf111VeiUNM775AMKHEbBLS]
//Date: ...
//Hash: "..."
//Fee: "0.01"
//Result: "tesSUCCESS"
//Memos: [Array]
//Effects: [Array]
```

### OfferNew
User creates a new offer. It has following info.

```
//Type: OfferNew
//OfferType: Sell
//Gets: [SWT]
//Pays: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
//Seq: 12
//Date: 2018-05-02 23:57:00
//Hash: "2F235C6C5F7839DC16E8896338FA4AB202538BD4415B55688F8B2DBC47269E0E"
//Fee: "0.01"
//Result: "tesSUCCESS"
//Memos: [Array]
//Effects: [Array]
```

### OfferCancel
User cancels the previous created offer. It has following info.

```
//Type: OfferCancel
//OfferSeq: 1
//Gets: [SWT]
//Pays: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
//Date: 2018-04-19 0:36:20
//Hash: "ABFD3C2AC5B97156FB5246C504CBBC071147B666151260756A5FBEC6FCD82A9F"
//Fee: "0.01"
//Result: "tesSUCCESS"
//Memos: [Array]
//Effects: [Array]
```

### OfferEffect
The offer is bought by or sold to others after the offer was created. It has following info.

```
//Type: OfferEffect
//Date: 2018-05-04 23:52:30
//Hash: "1FBD88D0AA001BBECB9C567F0D7128502ECE50BE5379CC22E2E8A496A91EC16C"
//Fee: "0.01"
//Result: "tesSUCCESS"
//Memos: [Array]
//Effects: [Array]
```

## NodeEffect class
Each transaction can have many affect nodes. And different node has different effect. The Effect property indicates the type of the effect. The following transaction effects are listed.

### OfferFunded
The offer is actually funded. The suggest prompt message could be: "Offer funded, you use XXX bought/sold XXX with price XXX" . It has following info.

```
//Effect: OfferFunded
//CounterParty: [JingTum.Lib.CounterParty]
//Got: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
//Paid: [SWT]
//Seq: 0
//Type: Bought
//Price: "0.041"
//Deleted: True
```

### OfferPartiallyFunded
The offer is partially funded. Suggest prompt message: "Offer partially funded, you use XXX bought/sold XXX with price XXX, the offer is cancel since the remained amount is not enough (optional, based on Cancelled property), the remained amount is XXX (optional, based on Remaining property)". It has following info. (I have no partially funded offer now, so just list the properties.)

```
//Effect: OfferPartiallyFunded
//Type: Bought
//Seq: 0
//CounterParty: [JingTum.Lib.CounterParty]
//Paid: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or:3001.96998]
//Got: [SWT:9999]
//Price: "0.03002"
//Gets:  [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
//Pays:  [SWT:1]
//Cancelled: false
//Remaining: true
```

The above contains the following key info.

* The offer is partially funded, you got SWT 9999, paid CNY amount is 3001.96998.
* The remaining is true, means have remain offer, the remain amount is 1, price is 0.03002.

### OfferCancelled
The offer is cancelled by BuildOfferCancelTx operation. Suggest prompt message: "The offer is cancelled, offer sequence is XXX". It has following info.

```
//Effect: OfferCancelled
//Type: Sell
//Gets: [SWT]
//Pays: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
//Seq: 1
//Price: "0.042"
//Deleted: True
```

### OfferCreated
A new offer is created. Suggest prompt message: "You create a buy/sell offer, use XXX transfer XXX". It has following info.

```
//Effect: OfferCreated
//Type: Sell
//Gets: [SWT]
//Pays: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
//Seq: 36
//Price: "2"
//Deleted: False
```

### OfferBought
The orderbook is sold/bought by other's buy/sell offer. Suggest prompt message: "You use XXX bought/sold XXX". It has following info.

```
//Effect: OfferBought
//Type: Sold
//CounterParty: [JingTum.Lib.CounterParty]
//Paid: [SWT]
//Got: [CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or]
//Price: "0.03336"
//Deleted: False
```
