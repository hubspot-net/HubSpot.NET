namespace HubSpot.NET.Core.Requests
{
    /// <summary>
    /// Represents a property value instance that can be sent to HubSpot
    /// </summary>
    public class HubspotDataEntityProp
    {
        /// <summary>
        /// Gets or sets the property that has a value.
        /// </summary>
        /// <remarks>
        /// Data will only be set here if the converetd DTO returns a v1 route!
        /// </remarks>
        /// <value>
        /// The property value
        /// </value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the actual value of the given property.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the property that has a value.
        /// </summary>
        /// <remarks>
        /// Data will only be set here if the converted DTO returns a v2 route!
        /// </remarks>
        /// <value>
        /// The name.
        /// </value>
        public object Name { get; set; }
    }
}