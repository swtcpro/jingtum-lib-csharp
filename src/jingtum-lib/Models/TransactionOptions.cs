using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the options for payment transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class PaymentTxOptions
    {
        /// <summary>
        /// The source account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string From { get; set; }
        /// <summary>
        /// The destination account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string To { get; set; }
        /// <summary>
        /// The payment amount.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public Amount Amount { get; set; }
    }

    /// <summary>
    /// The options for contract transaction.
    /// </summary>
    public abstract class ContractTxOptions
    {
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The array of parameters.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        public string[] Params { get; set; }
    }

    /// <summary>
    /// Represents the options for deploy contract transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class DeployContractTxOptions : ContractTxOptions
    {
        /// <summary>
        /// The amount to active the contract address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public double Amount { get; set; }
        /// <summary>
        /// The content of the contract.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Payload { get; set; }
    }

    /// <summary>
    /// Represents the options for call contract transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class CallContractTxOptions : ContractTxOptions
    {
        /// <summary>
        /// The contract address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Destination { get; set; }
        /// <summary>
        /// The function name to call.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Foo { get; set; }
    }

    /// <summary>
    /// Represents the options for sign transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class SignTxOptions
    {
        /// <summary>
        /// The blob of the sign result.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Blob { get; set; }
    }

    /// <summary>
    /// Represents the options for relation transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class RelationTxOptions
    {
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The destination address.
        /// </summary>
        /// <remarks>
        /// Required for the relation types except <see cref="RelationType.Trust"/>.
        /// </remarks>
        public string Target { get; set; }
        /// <summary>
        /// The type of the relation.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public RelationType Type { get; set; }
        /// <summary>
        /// The limit amount.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public Amount Limit { get; set; }
        /// <summary>
        /// Reserved.
        /// </summary>
        /// <remarks>
        /// Optional. Default is 0.
        /// </remarks>
        public UInt32? QualityIn { get; set; }
        /// <summary>
        /// Reserved.
        /// </summary>
        /// <remarks>
        /// Optional. Default is 0.
        /// </remarks>
        public UInt32? QualityOut { get; set; }
    }

    /// <summary>
    /// Represents the options for set account atribute transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class AccountSetTxOptions
    {
        /// <summary>
        /// The type of the account attribute.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public AccountSetType Type { get; set; }
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The flags to set.
        /// </summary>
        /// <remarks>
        /// Optional for type of <see cref="AccountSetType.Property"/>.
        /// </remarks>
        public SetClearFlags? SetFlag { get; set; }
        /// <summary>
        /// The flags to clear.
        /// </summary>
        /// <remarks>
        /// Optional for type of <see cref="AccountSetType.Property"/>.
        /// </remarks>
        public SetClearFlags? ClearFlag { get; set; }
        /// <summary>
        /// The regular key.
        /// </summary>
        /// <remarks>
        /// Required for type of <see cref="AccountSetType.Delegate"/>.
        /// </remarks>
        public string DelegateKey { get; set; }
    }

    /// <summary>
    /// Represents the options for offer create transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class OfferCreateTxOptions
    {
        /// <summary>
        ///  The type of the order.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public OfferType Type { get; set; }
        /// <summary>
        ///  The account address.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The amount to get by taker.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public Amount TakerGets { get; set; }
        /// <summary>
        /// The amount can to exchanged out by taker.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public Amount TakerPays { get; set; }
    }

    /// <summary>
    /// Represents the options for offer cancel transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class OfferCancelTxOptions
    {
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The order sequence.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public UInt32 Sequence { get; set; }
    }
}
