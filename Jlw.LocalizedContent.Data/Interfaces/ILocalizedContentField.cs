// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="ILocalizedContentField.cs" company="Jason L. Walker">
//     Copyright �2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.LocalizedContent 
{
    /// <summary>
    /// Class to encapsulate a row from the LocalizedContentFields database table
    /// </summary>
    public interface ILocalizedContentField 
    {
        /// <summary>
        /// Returns the unique Id of the record. Matches the [Id] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The identifier.</value>
        /// <returns>If the instance was created from a database record, then the autogenerated Id column is returned. Otherwise the default value is 0.</returns>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<long>))] 
        long Id { get; }

        /// <summary>
        /// Matches the [GroupKey] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The group key.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string GroupKey { get; }

        /// <summary>
        /// Matches the [FieldKey] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The field key.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string FieldKey { get; }

        /// <summary>
        /// Matches the [FieldType] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The type of the field.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string FieldType { get; }

        /// <summary>
        /// Matches the [FieldData] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The field data.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string FieldData { get; }

        /// <summary>
        /// Matches the [FieldClass] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The field class.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string FieldClass { get; }

        /// <summary>
        /// Matches the [ParentKey] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The parent key.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string ParentKey { get; }

        /// <summary>
        /// Matches the [DefaultLabel] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The default label.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string DefaultLabel { get; }

        /// <summary>
        /// Matches the [WrapperClass] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The wrapper class.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string WrapperClass { get; }

        /// <summary>
        /// Matches the [WrapperHtmlStart] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The wrapper HTML start.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string WrapperHtmlStart { get; set; }

        /// <summary>
        /// Matches the [WrapperHtmlEnd] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The wrapper HTML end.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string WrapperHtmlEnd { get; set; }

        /// <summary>
        /// Matches the [AuditChangeType] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The type of the audit change.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonIgnore] 
        string AuditChangeType { get; }

        /// <summary>
        /// Matches the [AuditChangeBy] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The audit change by.</value>
        //[JsonIgnore] 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        string AuditChangeBy { get; }

        /// <summary>
        /// Matches the [AuditChangeDate] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The audit change date.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonIgnore] 
        DateTime AuditChangeDate { get; }

        /// <summary>
        /// The group filter that is used for the particular instance of the object
        /// </summary>
        /// <value>The group filter.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonIgnore]
        string GroupFilter { get; set; }

        /// <summary>
        /// Matches the [Order] column of the [LocalizedContentFields] table in the database.
        /// </summary>
        /// <value>The order.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<int>))] 
        int Order { get; }

        /// <summary>
        /// Replace placeholders in sourceString with data from the replacementObject. Placeholders match the members of the replacementObject.
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="replacementObject"></param>
        /// <returns>String</returns>
        string ResolvePlaceholders(string sourceString, object replacementObject);

    }
} 