using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents options for payment transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PaymentTxOptions
    {
        /// <summary>
        /// Required. The source account address.
        /// </summary>
        [Category("Required")]
        public string From { get; set; }
        /// <summary>
        /// Required. The destination account address.
        /// </summary>
        [Category("Required")]
        public string To { get; set; }
        /// <summary>
        /// Required. The payment amount.
        /// </summary>
        [Category("Required")]
        public AmountSettings Amount { get; set; }
    }

    public abstract class ContractTxOptions
    {
        /// <summary>
        /// Required. The account address.
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        public string[] Params { get; set; }
    }

    /// <summary>
    /// Represents the options for contract transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DeployContractTxOptions : ContractTxOptions
    {
        /// <summary>
        /// Required. The amount.
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Required.
        /// </summary>
        public string Payload { get; set; }
    }

    /// <summary>
    /// Represents the options for call contract transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CallContractTxOptions : ContractTxOptions
    {
        /// <summary>
        /// Required. The destination address.
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// Required. The function name.
        /// </summary>
        public string Foo { get; set; }
    }

    /// <summary>
    /// Represents the options for sign transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SignTxOptions
    {
        /// <summary>
        /// Required.
        /// </summary>
        public string Blob { get; set; }
    }

    /// <summary>
    /// Represents the options for relation transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RelationTxOptions
    {
        /// <summary>
        /// Required. The account address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Required. Optional for <see cref="RelationType.Trust"/>.
        /// The destination address.
        /// </summary>
        [Category("Optional")]
        public string Target { get; set; }
        /// <summary>
        /// Required. The type of the relation.
        /// </summary>
        [Category("Required")]
        public RelationType Type { get; set; }
        /// <summary>
        /// Required.The limit amount.
        /// </summary>
        [Category("Required")]
        public AmountSettings Limit { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        [Category("Optional")]
        public UInt32? QualityIn { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        [Category("Optional")]
        public UInt32? QualityOut { get; set; }
    }

    /// <summary>
    /// Represents the options for set account atribute transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AccountSetTxOptions
    {
        /// <summary>
        /// Required. The type of the account attribute.
        /// </summary>
        [Category("Required")]
        public AccountSetType Type { get; set; }
        /// <summary>
        /// Requird. The account address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        [Category("Optional")]
        public SetClearFlags? SetFlag { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        [Category("Optional")]
        public SetClearFlags? ClearFlag { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        [Category("Optional")]
        public string DelegateKey { get; set; }
    }

    /// <summary>
    /// Represents the options for offer create transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OfferCreateTxOptions
    {
        /// <summary>
        /// Required. The type of the order.
        /// </summary>
        [Category("Required")]
        public OfferType Type { get; set; }
        /// <summary>
        /// Required. The accound address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Required. Same as <see cref="Pays"/>. The amount to get by taker.
        /// </summary>
        [Category("Required")]
        public AmountSettings TakerGets { get; set; }
        /// <summary>
        /// Required. Same as <see cref="Gets"/>. The amount can to exchanged out by taker.
        /// </summary>
        [Category("Required")]
        public AmountSettings TakerPays { get; set; }
    }

    /// <summary>
    /// Represents the options for offer cancel transaction.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OfferCancelTxOptions
    {
        /// <summary>
        /// Required. The accound address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Required. The order sequence.
        /// </summary>
        [Category("Required")]
        public UInt32 Sequence { get; set; }
    }
}
