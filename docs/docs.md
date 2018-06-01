# Dcuments
Usage for jingtum-lib-csharp. All classes are under the namespace JingTum.Lib.

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
url: The jingtum websocket server url.
localSign: Whether sign transaction in local.

#### sample
```
var remote = new remote("ws://123.57.219.57:5020", true);
```

### Connect(callback)
Each remote object should connect jingtum first. Now jingtum should connect manual, only then you can send request to backend.
Callback as MessageCallback<ConnectResponse>.

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
Callback as MessageCallback<ServerInfoResponse>.

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
Callback as MessageCallback<LedgerClosedResponse>.

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
Callback as MessageCallback<LedgerResponse>.

#### options
(If none is provided, then last closed ledger is returned.)
LedgerIndex: The ledger index.
LedgerHash: The ledger hash.
Transactions: Whether include the transactions list in ledger.

#### sample
```
var req = remote.RequestLedger(newLedgerOptions{ LedgerIndex = 330784, Transactions = true});
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
Callback as MessageCallback<TxResponse>.

#### options
Hash: The transaction hash.

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
Callback as MessageCallback<AccountInfoResponse>.

#### options
Account: The wallet address.
LedgerIndex: (optional) 

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
Each account helds many jingtum tums, and the received and sent tums can be found by RequestAccountTums.
Callback as MessageCallback<AccountTumsResponse>.

#### options
Account: The wallet address.
LedgerIndex: (optional)

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
Jingtum wallet is connected by many relations. Now jingtum support `trust`, `authorize` and `freeze` relation, all can be query by requestAccountRelations.
Callback as MessageCallback<AccountRelationsResponse>.

#### options
Account: The wallet addres.
Type: Trust, Ahthorize, Freeze
Ledger: (Optional)
Limit: (options) Limit the return relations count.

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
Callback as MessageCallback<AccountOffers>.

#### options
Account: The wallet address.
LedgerIndex: (optional)
Limit: (options) Limit the return offers count.

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
Callback as MessageCallback<AccountTxResponse>.

#### options
Account: The wallet address.
LedgerIndex: (optional) 
Limit: (optional) Limit the return tx count.

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
});
```

### RequestOrderBook(options)
Query order book info.
Callback as MessageCallback<OrderBookReponse>.

Firstly , each order book has a currency pair, as AAA/BBB. When to quer the bid orders, gets is AAA and pays is BBB. When to query the ask orders, gets is BBB and pays is AAA.
The result is array of orders.

#### options
Gets: Amount object. (ignore the Value)
Pays: Amount object. (ignore the Value)

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
Callback as MessageCallback<PathFindResponse>.

#### options
Account: The payment source address.
Destination: The payment target address.
Amount: The payment amount.

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

In this path find, the user want to send CNY to another account. The system provides one choice which is to use SWT.

In each choice, one `Key` is presented. Key is used to "SetPath" in transaction parameter setting.

### CreateAccountStub()

### CreateOrderBookStub()

### BuildPaymentTx(options)

### BuildRelationTx(options)

### BuildAccountSetTx(options)

### BuildOfferCreateTx(options)

### BuildOfferCancelTx(options)

### DeployContractTx(options)

### CallContractTx(options)



